using Microsoft.EntityFrameworkCore;
using TaskEntity = Implementación_API_RESTful.Models.Task; // Alias para la clase Task

namespace Implementación_API_RESTful.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; } // Usa el alias TaskEntity
    }
}
