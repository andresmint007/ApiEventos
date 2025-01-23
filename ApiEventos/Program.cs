using Datos;
using Entidades.Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Negocio;
using Servicios.Eventos;
using Servicios.Usuarios;
using System.Text;

 var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<IRepositoryComun<Usuario>, UsuarioContextDB>();
builder.Services.AddScoped<IRepositoryComun<Evento>, EventoContextoDB>();
builder.Services.AddScoped<InscripcionesContextoDB>();

builder.Services.AddScoped<GestionUsuarios>();
builder.Services.AddScoped<GestionEventos>();

builder.Services.AddScoped<IUsuarioServices, UsuarioServicio>();
builder.Services.AddScoped<IEventosServices, EventosService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
