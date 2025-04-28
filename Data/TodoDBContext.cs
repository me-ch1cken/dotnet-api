using Microsoft.EntityFrameworkCore;

public class TodoDBContext : DbContext
{
    public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> Todos => Set<TodoItem>();
}