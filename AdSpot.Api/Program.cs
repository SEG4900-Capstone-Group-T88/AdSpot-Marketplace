var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<AdSpotDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("Postgres"))
);

builder.Services.AddHttpClient();

builder
    .Services
    // Options
    .AddAndValidateOptions<JwtOptions>(config, out var jwtOptions)
    .AddAndValidateOptions<EndpointsOptions>(config, out var endpointsOptions)
    .AddAndValidateOptions<OAuthOptions>()
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
    .AddScoped<AddUserInputValidator>();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder
    .Services.AddGraphQLServer()
    .AddAuthorization()
    .AddFluentValidation()
    .UsePersistedQueryPipeline()
    .AddReadOnlyFileSystemQueryStorage("./PersistedQueries")
    .AddMutationConventions(applyToAllMutations: true)
    .AddInMemorySubscriptions()
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
    .SetPagingOptions(
        new HotChocolate.Types.Pagination.PagingOptions { IncludeTotalCount = true, }
    );

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = jwtOptions.Issuer;
        options.Audience = jwtOptions.Audience;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "all",
        policy =>
        {
            policy.WithOrigins(endpointsOptions.AdSpotClient);
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.SeedDatabase();
}

app.UseCors("all");

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQL("/");

app.Run();
