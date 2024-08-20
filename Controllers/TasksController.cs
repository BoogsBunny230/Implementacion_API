using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskEntity = Implementación_API_RESTful.Models.Task;
using Implementación_API_RESTful.Data;
using Microsoft.EntityFrameworkCore;

namespace Implementación_API_RESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TaskEntity>> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public ActionResult<TaskEntity> GetTask(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<TaskEntity> PostTask(TaskEntity task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult PutTask(int id, TaskEntity task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }

        // PATCH: api/tasks/{id}/complete
        [HttpPatch("{id}/complete")]
        public IActionResult CompleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = true;
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
