using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* if (!optionsBuilder.IsConfigured)
             {
                 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                // optionsBuilder.UseSqlServer("Server=localhost;Database=CustomerDB;user id=sa;password=123;");
             } */

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("SilaeeConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //.AddJsonFile("appsettings.json")
            //.Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
