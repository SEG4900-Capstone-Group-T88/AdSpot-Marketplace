var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdSpotDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

var localReactEndpoint = "http://localhost:5173";
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
    .AddScoped<ConnectionRepository>()
    .AddScoped<ListingRepository>()
    .AddScoped<ListingTypeRepository>()
    .AddScoped<PlatformRepository>()
    .AddScoped<UserRepository>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType()
    .AddMutationConventions(applyToAllMutations: true)
    .AddAdSpotTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterService<ConnectionRepository>()
    .RegisterService<ListingRepository>()
    .RegisterService<ListingTypeRepository>()
    .RegisterService<PlatformRepository>()
    .RegisterService<UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.SeedDatabase();
}

app.UseCors(localReactCors);

app.MapGraphQL("/");
app.Run();
