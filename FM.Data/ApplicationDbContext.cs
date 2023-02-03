using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PM.Data.Entity;
using PM.Models;

namespace PM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Member> Member { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plane>().HasData(
                new Plane()
                {
                    Id = 1,
                    Name = "Bowing",
                    Seats = 100,
                    IsActive = true,
                },
                new Plane()
                {
                    Id = 2,
                    Name = "Airbus",
                    Seats = 300,
                    IsActive = true,
                });
        }
    }
}
