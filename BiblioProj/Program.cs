using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BiblioProj.Data;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BiblioProjContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioProjContext") ?? throw new InvalidOperationException("Connection string 'BiblioProjContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("BiblioProjContext");
builder.Services.AddDbContext<BiblioProjContext>(options =>
    options.UseSqlServer(connectionString));

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
