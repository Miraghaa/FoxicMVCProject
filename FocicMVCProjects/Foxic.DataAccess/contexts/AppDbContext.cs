﻿using Foxic.Core.Entities;
using Foxic.Core.Entities.AreasEntitycontroller;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foxic.DataAccess.contexts;

public class AppDbContext: IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
    }
    public DbSet<Slider> Sliders { get; set; } = null!;
    public DbSet<Color> Colors { get; set; } = null!;
	public DbSet<Brand> Brands { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Collection> Collections { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Image> Images { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductColor> ProductColors { get; set; }
	public DbSet<ProductDetail> productDetails { get; set; }
	public DbSet<OrderItem> ProductOrders { get; set; }
	public DbSet<ProductSize> ProductSizes { get; set; }
	public DbSet<Size> Sizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Brand>()
            .HasIndex(p => p.BrandName)
            .IsUnique();
        modelBuilder.Entity<Category>()
            .HasIndex(p => p.CategoryName)
            .IsUnique();
        modelBuilder.Entity<Collection>()
            .HasIndex(p => p.CollectionName)
            .IsUnique();
        modelBuilder.Entity<Size>()
            .HasIndex(p => p.Name)
            .IsUnique();
        modelBuilder.Entity<Color>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
    }
}
