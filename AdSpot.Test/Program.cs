namespace AdSpot.Test;

public static class TestServices
{
    static TestServices()
    {
        Services = new ServiceCollection()
            // Services
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

            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()

            .AddGraphQLServer()
            .AddMutationConventions(applyToAllMutations: true)
            .AddAdSpotTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting()

            .RegisterDbContext<AdSpotDbContext>()
            .RegisterService<IConfiguration>()
            .RegisterService<InstagramService>()
            .RegisterService<ConnectionRepository>()
            .RegisterService<ListingRepository>()
            .RegisterService<ListingTypeRepository>()
            .RegisterService<OrderRepository>(ServiceKind.Resolver)
            .RegisterService<PlatformRepository>()
            .RegisterService<UserRepository>()

            .SetPagingOptions(new HotChocolate.Types.Pagination.PagingOptions
            {
                IncludeTotalCount = true,
            })

            .ModifyRequestOptions(
                options =>
                {
                    options.IncludeExceptionDetails = true;
                })
            .Services
            .AddSingleton(
                sp => new RequestExecutorProxy(
                    sp.GetRequiredService<IRequestExecutorResolver>(),
                    Schema.DefaultName))
            .BuildServiceProvider();

        Executor = Services.GetRequiredService<RequestExecutorProxy>();
    }

    public static IServiceProvider Services { get; }

    public static RequestExecutorProxy Executor { get; }

    public static async Task<IExecutionResult> ExecuteRequestAsync(
        Action<IQueryRequestBuilder> configureRequest,
        CancellationToken cancellationToken = default)
    {
        var scope = Services.CreateAsyncScope();

        var requestBuilder = new QueryRequestBuilder();
        requestBuilder.SetServices(scope.ServiceProvider);
        configureRequest(requestBuilder);
        var request = requestBuilder.Create();

        var result = await Executor.ExecuteAsync(request, cancellationToken);
        result.RegisterForCleanup(scope.DisposeAsync);
        return result;
    }
}
