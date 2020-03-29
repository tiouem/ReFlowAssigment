using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
                {
        }
        protected  override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Company>().ToTable("Companies")
            .HasMany(o => o.Owners);
            modelBuilder.Entity<Owner>().ToTable("Owners");
        }
        public DbSet<Company> Companies {get;set;}
        public DbSet<Owner> Owners { get; set; }

    }
}