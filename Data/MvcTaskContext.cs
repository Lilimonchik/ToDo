using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace MvcTask.Data
{
    // DbContext class representing the database context
    public class MvcTaskContext : DbContext
    {
        // Constructor taking DbContextOptions as a parameter
        public MvcTaskContext(DbContextOptions<MvcTaskContext> options)
            : base(options)
        {
        }

        // DbSet representing the TaskModel entity, which corresponds to a database table
        public DbSet<TaskModel> TaskModel { get; set; }
    }
}