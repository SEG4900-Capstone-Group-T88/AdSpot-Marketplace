var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdSpotDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

var localReactEndpoint = "http://localhost:3000";
var localReactCors = "local-react-app";
builder.Services.AddCors(options =>
{
    options.AddPolicy(localReactCors, policy =>
    {
        policy.WithOrigins(localReactEndpoint);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Issuer"];
        options.Audience = builder.Configuration["Jwt:Audience"];
        //options.TokenValidationParameters =
        //    new TokenValidationParameters
        //    {
        //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //        ValidAudience = builder.Configuration["Jwt:Audience"],
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(
        //            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        //    };
    });
builder.Services.AddAuthorization();

builder.Services
    .AddScoped<ConnectionRepository>()
    .AddScoped<ListingRepository>()
    .AddScoped<ListingTypeRepository>()
    .AddScoped<OrderRepository>()
    .AddScoped<PlatformRepository>()
    .AddScoped<UserRepository>();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType()
    .UsePersistedQueryPipeline()
    .AddReadOnlyFileSystemQueryStorage("./PersistedQueries")
    .AddMutationConventions(applyToAllMutations: true)
    .AddAdSpotTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterService<ConnectionRepository>()
    .RegisterService<ListingRepository>()
    .RegisterService<ListingTypeRepository>()
    .RegisterService<OrderRepository>()
    .RegisterService<PlatformRepository>()
    .RegisterService<UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.SeedDatabase();
}

app.UseCors(localReactCors);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.MapGraphQL("/");
app.Run();
