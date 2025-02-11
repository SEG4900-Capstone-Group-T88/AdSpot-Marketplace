﻿var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();
var config = builder.Configuration;

var keyManager = new KeyManager();
builder.Services.AddSingleton(keyManager);

builder.Services.AddDbContext<AdSpotDbContext>(options => options.UseNpgsql(config.GetConnectionString("Postgres")));

builder.Services.AddHttpClient().AddHttpContextAccessor();

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
    .AddScoped<FlairRepository>()
    // Validators
    .AddScoped<AddUserInputValidator>()
    .AddScoped<SubmitDeliverableInputValidator>();

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
    .AddConvention<IFilterConvention>(
        new FilterConventionExtension(x =>
            x.AddProviderExtension(
                new QueryableFilterProviderExtension(y => y.AddFieldHandler<QueryableStringInvariantEqualsHandler>())
            )
        )
    )
    .AddConvention<IFilterConvention>(
        new FilterConventionExtension(x =>
            x.AddProviderExtension(
                new QueryableFilterProviderExtension(y =>
                    y.AddFieldHandler<QueryableStringInvariantStartsWithHandler>()
                )
            )
        )
    )
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
    .RegisterService<FlairRepository>()
    .SetPagingOptions(new HotChocolate.Types.Pagination.PagingOptions { IncludeTotalCount = true });

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = !isDevelopment,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(keyManager.RsaKey),
            ClockSkew = TimeSpan.Zero,
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
