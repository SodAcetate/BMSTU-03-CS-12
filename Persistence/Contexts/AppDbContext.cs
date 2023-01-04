using Microsoft.EntityFrameworkCore;
using Lab_12.Domain.Models;

namespace Lab_12.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Sight> Sights { get; set; }
        
        public AppDbContext() : base() { Database.EnsureCreated(); }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=sql2;User Id=sa;Password=<cewlpass69>;TrustServerCertificate=true;MultipleActiveResultSets=true");
            optionsBuilder.EnableDetailedErrors();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<City>().ToTable("Cities");
            builder.Entity<City>().HasKey(p => p.Id);
            builder.Entity<City>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<City>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<City>().HasMany(p => p.Hotels).WithOne(p => p.City).HasForeignKey(p => p.CityId);
            builder.Entity<City>().HasMany(p => p.Sights).WithOne(p => p.City).HasForeignKey(p => p.CityId);
            
            builder.Entity<City>().HasData
            (
                new City { Id = 100, Name = "Moscow" }, // Id set manually due to in-memory provider
                new City { Id = 101, Name = "Perm" }
            );
            
            builder.Entity<Hotel>().ToTable("Hotels");
            builder.Entity<Hotel>().HasKey(p => p.Id);
            builder.Entity<Hotel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Hotel>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Hotel>().Property(p => p.Address).IsRequired();

            builder.Entity<Sight>().ToTable("Sights");
            builder.Entity<Sight>().HasKey(p => p.Id);
            builder.Entity<Sight>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Sight>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Sight>().Property(p => p.Location).IsRequired();
        }
    }
}