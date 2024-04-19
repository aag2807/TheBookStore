using System.Diagnostics;
using BookApi.Extensions;
using Boundaries.Persistance.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCORS();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureDatabase();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (IServiceScope? serviceScope = app.Services.CreateScope())
{
    IServiceProvider? services = serviceScope.ServiceProvider;

    app.ConfigureExceptionHandler();
    app.UseMiddleware<LoggerMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();