using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RideOnBulgaria.Models;

namespace RideOnBulgaria.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CoverPhotoRoad> CoverPhotoRoads { get; set; }

        public DbSet<Road> Roads { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Road>()
                .HasOne(x => x.CoverPhoto)
                .WithOne(x => x.Road)
                .HasForeignKey<CoverPhotoRoad>(x => x.Id);

            builder.Entity<Product>()
                .HasOne(x => x.Image)
                .WithOne(x => x.Product)
                .HasForeignKey<ProductImage>(x => x.Id);
        }
    }
}
