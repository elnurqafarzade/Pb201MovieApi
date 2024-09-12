using Microsoft.EntityFrameworkCore;
using PB201Initial.Configurations;
using PB201Initial.Entities;

namespace PB201Initial.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options){}

        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GroupCofiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
