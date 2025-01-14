using Microsoft.EntityFrameworkCore;
using Villa_API.Models;

namespace Villa_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Pool View",
                    Details = "Pool View Details",
                    ImageUrl = "",
                    Rate = 100,
                    Amenity = "",
                    Occupancy = 4,
                    Sqft = 100,
                    CreateDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Lake View",
                    Details = "Lake View Details",
                    ImageUrl = "",
                    Rate = 200,
                    Amenity = "",
                    Occupancy = 8,
                    Sqft = 200,
                     CreateDate = DateTime.Now
                }
                );
        }
    }
}
