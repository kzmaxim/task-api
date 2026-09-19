using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;

namespace TaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static List<TaskItem> _tasks = new List<TaskItem>();
    
    [HttpGet]
    public IActionResult GetAllTasks()
    {
        return Ok(_tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public IActionResult AddTask(TaskItem task)
    {
        if (task == null || string.IsNullOrEmpty(task.Title))
        {
            return BadRequest();
        }
        task.Id = _tasks.Count + 1;
        _tasks.Add(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskItem task)
    {
        if (id <= 0 || task == null || string.IsNullOrEmpty(task.Title))
        {
            return BadRequest();
        }
        var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
        if (existingTask == null)
        {
            return NotFound();
        }
        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;
        
        return Ok(existingTask);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        _tasks.Remove(task);
        return NoContent();
    }
}