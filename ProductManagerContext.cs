using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductManager
{
    public partial class ProductManagerContext : DbContext
    {
        public ProductManagerContext()
        {
        }

        public ProductManagerContext(DbContextOptions<ProductManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsCategory> ProductsCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ProductManager;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Idparent).HasColumnName("IDParent");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(50)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.IdparentNavigation)
                    .WithMany(p => p.InverseIdparentNavigation)
                    .HasForeignKey(d => d.Idparent)
                    .HasConstraintName("FK__Categorie__IDPar__123EB7A3");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ArticleNumber, "AN_ArticleNumber")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArticleNumber)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(50)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ProductsCategory>(entity =>
            {
                entity.HasIndex(e => new { e.IdProduct, e.IdCategory }, "IDProductCategory")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdCategory).HasColumnName("IDCategory");

                entity.Property(e => e.IdProduct).HasColumnName("IDProduct");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.ProductsCategories)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ProductsC__IDCat__17036CC0");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductsCategories)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ProductsC__IDPro__160F4887");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(13);

                entity.Property(e => e.Password).HasMaxLength(25);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
