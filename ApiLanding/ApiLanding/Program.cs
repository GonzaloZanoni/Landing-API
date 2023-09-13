
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
using LandingAPI.AD.Services.Galeria;
using LandingAPI.AD.Services.GaleriaImagenes;
using LandingAPI.AD.Services.Footer;
using LandingAPI.AD.Services.RedSocial;
using LandingAPI.AD.Services.Empresas;

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
builder.Services.AddTransient<IGaleriaServices, GaleriaServices>();
builder.Services.AddTransient<IGaleriaImagenServices, GaleriaImagenServices>();
builder.Services.AddTransient<IFooterServices, FooterServices>();
builder.Services.AddTransient<IRedSocialServices, RedSocialServices>();
builder.Services.AddTransient<IEmpresaServices, EmpresaServices>();
builder.Services.AddControllers();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});



//var app = builder.Build();
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

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
