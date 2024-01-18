using CarsMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarsMVC.Context
{
    public class CarsDBContext:IdentityDbContext
    {
        public CarsDBContext(DbContextOptions<CarsDBContext> options) : base(options) { }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
