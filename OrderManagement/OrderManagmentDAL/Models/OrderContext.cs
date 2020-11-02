using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrderManagmentDAL.Models
{
    public partial class OrderContext : DbContext
    {
        public OrderContext()
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dresscategories> Dresscategories { get; set; }
        public virtual DbSet<Dresscategoryattributes> Dresscategoryattributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=sql5080.site4now.net;Database=DB_A6503A_ordermanagement;user id=DB_A6503A_ordermanagement_admin;password=K@2Te@m123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dresscategories>(entity =>
            {
                entity.HasKey(e => e.Dresscategoryid);

                entity.Property(e => e.Categoryname).HasMaxLength(100);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dresscategoryattributes>(entity =>
            {
                entity.HasKey(e => e.Measurementid);

                entity.Property(e => e.Attributename).HasMaxLength(100);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
