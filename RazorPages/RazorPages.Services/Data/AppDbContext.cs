using Microsoft.EntityFrameworkCore;
using RazorPages.Models;

namespace RazorPages.Services.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
