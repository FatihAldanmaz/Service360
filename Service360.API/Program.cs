using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Service360.Application.DependencyInjection;
using Service360.Infrastructure.DependencyInjection;
using Service360.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Application Katmanı
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

// Authentication şimdilik kapalı
// builder.Services.AddAuthentication(...);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();