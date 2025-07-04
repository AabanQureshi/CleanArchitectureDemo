using Application;
using CleanArchitectureDemo.Utils.CustomMiddleWares;
using Infrastructure;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddProblemDetails();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
    {
        o.Title = "Clean Architecture Demo";
        o.Theme = ScalarTheme.DeepSpace;
    });
}
 
app.UseMiddleware<MiddlewareExceptionHandler>(); // For Registring Global Exception Handler
app.UseExceptionHandler(); // for Registring Global Exception Handler through IExceptionHandler Interface
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
