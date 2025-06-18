var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/shirts", () => "Reading all the shirts");
app.MapGet("/shirts/{id}", (int id) => $"Reading the shirt with id: {id}");
app.MapPost("/shirts", () => "Creating a new shirt");
app.MapPut("/shirts/{id}", (int id) => $"Updating the shirt with id: {id}");
app.MapDelete("/shirts/{id}", (int id) => $"Deleting the shirt with id: {id}");

app.UseHttpsRedirection();

app.Run();
