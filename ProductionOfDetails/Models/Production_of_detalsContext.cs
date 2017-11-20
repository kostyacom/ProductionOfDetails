using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductionOfDetails
{
    public partial class Production_of_detalsContext : DbContext
    {
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<MaterialTypes> MaterialTypes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }

        public Production_of_detalsContext(DbContextOptions<Production_of_detalsContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\v11.0;Database=Production_of_detals;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.IdClient);

                entity.Property(e => e.Adress).HasMaxLength(30);

                entity.Property(e => e.FirmName).HasMaxLength(50);

                entity.Property(e => e.Representative).HasMaxLength(40);

                entity.Property(e => e.Telephone).HasMaxLength(10);
            });

            modelBuilder.Entity<Details>(entity =>
            {
                entity.HasKey(e => e.IdDetail);

                entity.Property(e => e.Colour).HasMaxLength(1);

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.IdMaterial)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MaterialTypes_Detail");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Detaisls_Order");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.FirmName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasKey(e => e.IdInvoice);

                entity.Property(e => e.DeliveryDate).HasColumnType("date");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EMPLOYEE_INVOCES");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.IdMaterial)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MaterialTypes_Invoice");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SUPPLIER_INVOICE");
            });

            modelBuilder.Entity<MaterialTypes>(entity =>
            {
                entity.HasKey(e => e.IdMaterial);

                entity.Property(e => e.TypeMaterial).HasMaxLength(30);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_Order");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_EMPLOYEE_ORDERS");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.IdSupplier);

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}
