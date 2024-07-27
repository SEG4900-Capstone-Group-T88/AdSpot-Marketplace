namespace AdSpot.Test;

public static class TestServices
{
    static TestServices()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var keyManager = new KeyManager();

        ServiceProvider = new ServiceCollection()
            .AddLogging() // Required for auth stuff
            .AddScoped<IConfiguration>(sp => config)
            .AddHttpContextAccessor()
            .AddAndValidateOptions<JwtOptions>(config, out var jwtOptions)
            // Options
            .AddAndValidateOptions<JwtOptions>()
            .AddAndValidateOptions<EndpointsOptions>()
            .AddAndValidateOptions<OAuthOptions>()
            .AddDbContext<AdSpotDbContext>(o => o.UseInMemoryDatabase("adspot-inmemory-db"))
            // Services
            .AddSingleton(keyManager)
            .AddScoped<InstagramService>()
            // Repositories
            .AddScoped<ConnectionRepository>()
            .AddScoped<ListingRepository>()
            .AddScoped<ListingTypeRepository>()
            .AddScoped<OrderRepository>()
            .AddScoped<PlatformRepository>()
            .AddScoped<UserRepository>()
            // Validators
            .AddScoped<AddUserInputValidator>()
            .AddScoped<SubmitDeliverableInputValidator>()
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddGraphQLServer()
            .AddAuthorization()
            .AddFluentValidation()
            .AddMutationConventions(applyToAllMutations: true)
            .AddInMemorySubscriptions()
            .AddAdSpotTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .RegisterDbContext<AdSpotDbContext>()
            .RegisterService<IConfiguration>()
            .RegisterService<IHttpContextAccessor>()
            .RegisterService<InstagramService>()
            .RegisterService<ConnectionRepository>()
            .RegisterService<ListingRepository>()
            .RegisterService<ListingTypeRepository>()
            .RegisterService<OrderRepository>(ServiceKind.Resolver)
            .RegisterService<PlatformRepository>()
            .RegisterService<UserRepository>()
            .SetPagingOptions(new HotChocolate.Types.Pagination.PagingOptions { IncludeTotalCount = true, })
            .ModifyRequestOptions(options =>
            {
                options.IncludeExceptionDetails = true;
            })
            .Services.AddSingleton(sp => new RequestExecutorProxy(
                sp.GetRequiredService<IRequestExecutorResolver>(),
                Schema.DefaultName
            ))
            .BuildServiceProvider();

        Executor = ServiceProvider.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider ServiceProvider { get; }

    public static RequestExecutorProxy Executor { get; }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, $"{TestDatabase.TestUser.FirstName} {TestDatabase.TestUser.LastName}"),
            new(ClaimTypes.Email, TestDatabase.TestUser.Email),
        };

        var identity = new ClaimsIdentity(claims, "TestAuth");
        return new ClaimsPrincipal(identity);
    }

    public static async Task<IExecutionResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default
    )
    {
        var scope = ServiceProvider.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        requestBuilder.AddGlobalState(nameof(ClaimsPrincipal), CreateClaimsPrincipal());
        var request = requestBuilder.Create();

        var context = ServiceProvider.GetRequiredService<AdSpotDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SeedTestDatabase();

        var result = await Executor.ExecuteAsync(request, cancellationToken);
        result.RegisterForCleanup(scope.DisposeAsync);
        return result;
    }

    public static async Task<IExecutionResult> ExecuteRequestAsync(
        Action<AsyncServiceScope> arrangeData,
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default
    )
    {
        var scope = ServiceProvider.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        requestBuilder.AddGlobalState(nameof(ClaimsPrincipal), CreateClaimsPrincipal());
        var request = requestBuilder.Create();

        var context = ServiceProvider.GetRequiredService<AdSpotDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SeedTestDatabase();
        arrangeData.Invoke(scope);

        var result = await Executor.ExecuteAsync(request, cancellationToken);
        result.RegisterForCleanup(scope.DisposeAsync);
        return result;
    }
}
