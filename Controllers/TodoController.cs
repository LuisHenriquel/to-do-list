using API.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    /// <summary>
    /// This class defines the controller responsible for handling HTTP requests. 
    /// It receives input data, performs validations, and returns appropriate HTTP status codes.
    /// </summary>

    [ApiController]
    [Route(template: "Controller")]
    public class TodoController : ControllerBase
    {

        /// <summary>
        /// Returns all tasks from the database.
        /// </summary>
        [HttpGet]
        [Route("GetList")]
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context)
        {
            var todos = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();
            return Ok(todos);
        }

        /// <summary>
        /// Returns a specific task by its ID.
        /// </summary>
        [HttpGet]
        [Route("GetListId/{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return todo == null ? NotFound() : Ok(todo);
        }

        /// <summary>
        /// Creates a new task using the data provided in the request body.
        /// </summary>
        [HttpPost("CreateTask")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTodoViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };
            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"Controller/GetListId/{todo.Id}", todo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates the title of an existing task by its ID.
        /// </summary>
        [HttpPut("EditTask/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTodoViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context
            .Todos
            .FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();
            try
            {
                todo.Title = model.Title;
                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a task by its ID. 
        /// </summary>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}