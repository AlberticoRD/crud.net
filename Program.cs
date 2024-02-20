using definitiva.Context;
using definitiva.Interfaces;
using definitiva.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<Ipersonas, ConexionPersonas>();
builder.Services.AddDbContext<DbContextPersonas>(Options =>
{
   Options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
}, ServiceLifetime.Scoped);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});

app.Run();
