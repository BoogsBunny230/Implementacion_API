using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Implementación_API_RESTful.Data;
using Microsoft.EntityFrameworkCore;
using TaskEntity = Implementación_API_RESTful.Models.Task;

namespace Implementación_API_RESTful.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskContext _context;

        public TaskService(TaskContext context)
        {
            _context = context;
        }

        // Obtener todas las tareas
        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        // Obtener una tarea por su ID
        public async Task<TaskEntity> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        // Crear una nueva tarea
        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        // Actualizar una tarea existente
        public async Task UpdateTaskAsync(TaskEntity task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var existingTask = await _context.Tasks.FindAsync(task.Id);
            if (existingTask == null)
            {
                throw new InvalidOperationException("Task not found.");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.UpdatedAt = DateTime.UtcNow;

            _context.Entry(existingTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Eliminar una tarea por su ID
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found.");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        // Marcar una tarea como completada
        public async Task CompleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found.");
            }

            task.IsCompleted = true;
            task.UpdatedAt = DateTime.UtcNow;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
