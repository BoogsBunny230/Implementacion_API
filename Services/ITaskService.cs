using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntity = Implementación_API_RESTful.Models.Task;

namespace Implementación_API_RESTful.Services
{
    public interface ITaskService
    {
        Task<List<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity> GetTaskByIdAsync(int id);
        Task<TaskEntity> CreateTaskAsync(TaskEntity task);
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(int id);
        Task CompleteTaskAsync(int id);
    }
}
