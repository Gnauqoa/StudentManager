using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<Student> Students { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
            options.UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=1;Database=studentmanagement");
        }
    }
}
