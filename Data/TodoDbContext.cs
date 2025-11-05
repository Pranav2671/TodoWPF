using Microsoft.EntityFrameworkCore;
using TodoWPF.Models;

namespace TodoWPF.Data
{
    // The DbContext is the main EF Core class that manages
    // the database connection and maps your C# classes to tables.
    public class TodoDbContext : DbContext
    {
        //This DbSet<TodoItem> represents the TodoItems table in the database
        public DbSet<TodoItem> TodoItems { get; set; }

        //OnConfiguring is where we tell EF Core how to connect to the database
        // Replace the placeholder connection string with your SQL Server details.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=PRANAV;Database=TodoWPFDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}
