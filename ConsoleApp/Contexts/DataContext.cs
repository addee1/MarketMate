using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<CustomerProfileEntity> CustomerProfiles { get; set; }
    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<DetailEntity> Details { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DetailEntity>()
            .HasKey(x => x.Id);
   

        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<CustomerProfileEntity>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();

        
    }
}
