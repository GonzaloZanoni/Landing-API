
using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using LandingAPI.AD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LandingApi.AD.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using LandingAPI.AD.Services.Contactos;
using LandingAPI.AD.Services.Header;
using LandingAPI.AD.Services.PortadaImagen;
using LandingAPI.AD.Services.Testimonios;
using LandingAPI.AD.Services.SeccionServicios;
using LandingAPI.AD.Services.ServicioImagenes;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var connection = new M_ConnectionToSql(config.GetConnectionString("Conn"));

// Add services to the container.

builder.Services.AddSingleton(connection);
builder.Services.AddTransient<IContactoServices, ContactoService>();
builder.Services.AddTransient<IHeaderServices, HeaderServices>();
builder.Services.AddTransient<IPortadaImagenServices, PortadaImagenServices>();
builder.Services.AddTransient<ITestimonioServices, TestimonioServices>();
builder.Services.AddTransient<ISeccionServicioServices, SeccionServicioServices>();
builder.Services.AddTransient<IServicioImagenServices, ServicioImagenServices>();
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

app.Run();
