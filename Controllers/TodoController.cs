using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoDBContext _context;

    public TodoController(TodoDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAll()
    {
        return await _context.Todos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> Get(int id)
    {
        var item = await _context.Todos.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item)
    {
        _context.Todos.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem item)
    {
        if (id != item.Id) return BadRequest();

        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("complete/{id}")]
    public async Task<IActionResult> CompleteItem(int id)
    {
        var item = await _context.Todos.FindAsync(id);
        if(item == null) return NotFound();

        item.IsCompleted = true;

        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Todos.FindAsync(id);
        if (item == null) return NotFound();

        _context.Todos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}