var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdSpotDbContext>(
    options => options.UseInMemoryDatabase("adspot"));

var localReactEndpoint = "http://localhost:5173";
var localReactCors = "local-react-app";
builder.Services.AddCors(options =>
{
    options.AddPolicy(localReactCors, policy => {
        policy.WithOrigins(localReactEndpoint);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services
    .AddScoped<AuthorRepository>()
    .AddScoped<BookRepository>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType()
    .AddMutationConventions(applyToAllMutations: true)
    .AddAdSpotTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterService<AuthorRepository>()
    .RegisterService<BookRepository>();

var app = builder.Build();

app.UseCors(localReactCors);

app.MapGraphQL("/");
app.Run();
