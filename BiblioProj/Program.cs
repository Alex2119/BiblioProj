using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BiblioProj.Data;
using System;
using System.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BiblioProjContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioProjContext") ?? throw new InvalidOperationException("Connection string 'BiblioProjContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<BiblioProjContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioProjContext"));
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
    options.EnableSensitiveDataLogging(); // Pour afficher les données sensibles dans les requêtes
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
