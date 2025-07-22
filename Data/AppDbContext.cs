using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// Configures the database context for the application and registers the Todo model,
    /// allowing Entity Framework Core to create and manage the corresponding table.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }


}