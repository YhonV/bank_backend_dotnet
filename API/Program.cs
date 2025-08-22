using Application.Interfaces;
using Application.Services;
using Infra.ConnectionDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region Services

// mapeamos los controladores que tenemos
builder.Services.AddControllers();

// se debe previamente instalar el package: dotnet add API.csproj package Swashbuckle.AspNetCore -v 6.6.2
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Registro del DbContext usando la conexión del appsettings.json
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// servicios 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();

#endregion

var app = builder.Build();

// habilitar el swagger cuando la app ejecuta (solo en dev)
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();
