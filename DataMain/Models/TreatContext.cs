using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models
{
    public class TreatContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Treat> Treats { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<FlavorTreat> FlavorTreats { get; set; }


        public TreatContext(DbContextOptions options) : base(options) { }
    }
}