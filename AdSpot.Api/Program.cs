var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdSpotDbContext>(
    options => options.UseInMemoryDatabase("adspot"));

builder.Services
    .AddScoped<AuthorRepository>()
    .AddScoped<BookRepository>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType()
    .AddAdSpotTypes()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .RegisterService<AuthorRepository>()
    .RegisterService<BookRepository>();

var app = builder.Build();

app.MapGraphQL("/");
app.Run();
