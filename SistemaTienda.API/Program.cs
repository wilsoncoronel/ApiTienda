using SistemaTienda.API.Middleware;
using SistemaTienda.IOC;
using SistemaTienda.DAL.DBContext;
using Scalar.AspNetCore;
using SistemaTienda.BLL.Servicios.Contrato;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient<ISriService, SriService>(client =>
{
    client.BaseAddress = new Uri("https://srienlinea.sri.gob.ec/"); // Replace with the actual base URL of the external API
    client.Timeout = TimeSpan.FromSeconds(10); // Set a timeout for the request
    client.DefaultRequestHeaders.Add(
        "User-Agent", "Mozilla/5.0");
});
builder.Services.InyectarDependecias(builder.Configuration);
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
