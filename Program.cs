using Microsoft.EntityFrameworkCore;
using Implementación_API_RESTful.Data;
using Implementación_API_RESTful.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración del DbContext
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el servicio de tareas para inyección de dependencias
builder.Services.AddScoped<ITaskService, TaskService>();

// Añadir controladores al contenedor de servicios
builder.Services.AddControllers();

// Configurar Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del middleware de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
