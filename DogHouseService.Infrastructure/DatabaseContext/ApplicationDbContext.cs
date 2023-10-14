using DogHouseService.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DogHouseService.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dog>()
                .HasIndex(dog => dog.Name)
                .IsUnique();

            modelBuilder.Entity<Dog>().HasData(
                new List<Dog>
                {
                    new Dog
                    {
                        Id = Guid.NewGuid(),
                        Name = "Neo",
                        Color = "red&amber",
                        TailLength = 22,
                        Weight = 32
                    },
                    new Dog
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jessy",
                        Color = "black&white",
                        TailLength = 7,
                        Weight = 14
                    },
                    new Dog
                    {
                        Id = Guid.NewGuid(),
                        Name = "Spot",
                        Color = "White",
                        TailLength = 2,
                        Weight = 20
                    }
                }
            );
        }

    }
}
