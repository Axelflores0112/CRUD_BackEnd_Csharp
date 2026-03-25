using CRUD_BackEnd.Context;
using Microsoft.EntityFrameworkCore;
using CRUD_BackEnd.Middlewares;
using CRUD_BackEnd.Security;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
//Conexion a DB usando .env
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

var connection = builder.Services.AddDbContext<AppDbContext>(options => 
options.UseNpgsql(connectionString));

if(connection != null)
{
    Console.WriteLine("succesfull connection with DB");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection(); Activar HTTPS

//app.UseAuthorization(); valida roles para ver quienes acceden a que endpoint. 

app.UseMiddleware<ExceptionMiddleware>();//Middleware personalizado para errores internos.

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.MapControllers();

app.Run();

/*CORREGIR LOS DE LAS MIGRACIONE SY VERIFICAR A DONDE ESTA MANDANDO LOS REGISTROS*/