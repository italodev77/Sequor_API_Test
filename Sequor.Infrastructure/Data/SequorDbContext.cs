using Microsoft.EntityFrameworkCore;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Infrastructure.Data
{
    public class SequorDbContext : DbContext
    {
        public SequorDbContext(DbContextOptions<SequorDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Production> Productions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Product>().HasKey(p => p.ProductCode);
            modelBuilder.Entity<Material>().HasKey(m => m.MaterialCode);
            modelBuilder.Entity<User>().HasKey(u => u.Email);
            modelBuilder.Entity<Production>().HasKey(p => p.Id);

            modelBuilder.Entity<ProductMaterial>()
                .HasKey(pm => new { pm.ProductCode, pm.MaterialCode });

            modelBuilder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMaterials)
                .HasForeignKey(pm => pm.ProductCode);

            modelBuilder.Entity<ProductMaterial>()
                .HasOne(pm => pm.Material)
                .WithMany(m => m.ProductMaterials)
                .HasForeignKey(pm => pm.MaterialCode);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(p => p.Orders) 
                .HasForeignKey(o => o.ProductCode)
                .HasPrincipalKey(p => p.ProductCode);
        }
    }
}
