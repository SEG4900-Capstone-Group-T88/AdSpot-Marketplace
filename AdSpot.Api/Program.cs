var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<AdSpotDbContext>(
    options => options.UseNpgsql(config.GetConnectionString("Postgres")));

builder.Services
    .AddScoped<ConnectionRepository>()
    .AddScoped<ListingRepository>()
    .AddScoped<ListingTypeRepository>()
    .AddScoped<OrderRepository>()
    .AddScoped<PlatformRepository>()
    .AddScoped<UserRepository>()
    .AddScoped<AddUserInputValidator>();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddFluentValidation()
    .AddQueryType()
    .UsePersistedQueryPipeline()
    .AddReadOnlyFileSystemQueryStorage("./PersistedQueries")
    .AddMutationConventions(applyToAllMutations: true)
    .AddAdSpotTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterDbContext<AdSpotDbContext>()
    .RegisterService<IConfiguration>()
    .RegisterService<ConnectionRepository>()
    .RegisterService<ListingRepository>()
    .RegisterService<ListingTypeRepository>()
    .RegisterService<OrderRepository>(ServiceKind.Resolver)
    .RegisterService<PlatformRepository>()
    .RegisterService<UserRepository>()
    .SetPagingOptions(new HotChocolate.Types.Pagination.PagingOptions
    {
        IncludeTotalCount = true,
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = config["Jwt:Issuer"];
        options.Audience = config["Jwt:Audience"];

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["Jwt:Key"]))
            };
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policy =>
    {
        policy.WithOrigins(config["Endpoints:AdSpotClient"]);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.SeedDatabase();
}

app.UseCors("all");

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL("/");

app.Run();
