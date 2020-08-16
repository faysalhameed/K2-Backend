using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CustomerDAL.Models
{
    public partial class SilaeeContext : DbContext
    {
        public SilaeeContext()
        {
        }

        public SilaeeContext(DbContextOptions<SilaeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerProfile> CustomerProfile { get; set; }
        public virtual DbSet<Customerloginactivities> Customerloginactivities { get; set; }
        public virtual DbSet<Devicetypeinfo> Devicetypeinfo { get; set; }
        public virtual DbSet<Usertypeinfo> Usertypeinfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=sql5052.site4now.net;Database=DB_A6503A_CustomerDB;user id=DB_A6503A_CustomerDB_admin;password=K@2Te@m123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerProfile>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CreationDateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerCity).HasMaxLength(100);

                entity.Property(e => e.CustomerCnic)
                    .HasColumnName("CustomerCNIC")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerCountry).HasMaxLength(50);

                entity.Property(e => e.CustomerFirstName).HasMaxLength(200);

                entity.Property(e => e.CustomerGender).HasMaxLength(50);

                entity.Property(e => e.CustomerLastName).HasMaxLength(200);

                entity.Property(e => e.CustomerMobileNumber).HasMaxLength(50);

                entity.Property(e => e.CustomerOtherContactNumber).HasMaxLength(50);

                entity.Property(e => e.CustomerPassword).HasMaxLength(100);

                entity.Property(e => e.CustomerProvince).HasMaxLength(100);

                entity.Property(e => e.CustomerRating).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CustomerWalletAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CustomerZipCode).HasMaxLength(100);

                entity.Property(e => e.CustomereMailAddress).HasMaxLength(100);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Uniquedeviceid).HasMaxLength(500);
            });

            modelBuilder.Entity<Customerloginactivities>(entity =>
            {
                entity.HasKey(e => e.Activityid);

                entity.Property(e => e.Authenticationmedium).HasMaxLength(50);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Logindatetime).HasColumnType("datetime");

                entity.Property(e => e.Logoutdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.SessionToken).HasMaxLength(200);
            });

            modelBuilder.Entity<Devicetypeinfo>(entity =>
            {
                entity.HasKey(e => e.Devicetypeid);

                entity.Property(e => e.Devicetypename).HasMaxLength(200);
            });

            modelBuilder.Entity<Usertypeinfo>(entity =>
            {
                entity.HasKey(e => e.Usertypeid);

                entity.Property(e => e.Usertype).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
