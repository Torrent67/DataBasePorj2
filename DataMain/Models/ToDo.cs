using Microsoft.EntityFrameworkCore;

namespace DataBase.Models
{
  public class DataBaseContext : DbContext
  {
    public virtual DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    
    public DataBaseContext(DbContextOptions options) : base(options) { }
  }
}