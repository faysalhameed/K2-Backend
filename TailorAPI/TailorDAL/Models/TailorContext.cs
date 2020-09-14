using System;
using logginglibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TailorDAL.Models
{
    public partial class TailorContext : DbContext
    {
        private ILog logger;
        public TailorContext(ILog templogger)
        {
            this.logger = templogger;
        }

        public TailorContext(DbContextOptions<TailorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Deviceinfo> Deviceinfo { get; set; }
        public virtual DbSet<Reviewfortailors> Reviewfortailors { get; set; }
        public virtual DbSet<Tailorloginactivities> Tailorloginactivities { get; set; }
        public virtual DbSet<Tailorotheraddressdetails> Tailorotheraddressdetails { get; set; }
        public virtual DbSet<Tailorproductdetails> Tailorproductdetails { get; set; }
        public virtual DbSet<Tailorprofile> Tailorprofile { get; set; }
        public virtual DbSet<Tailorpromotiondetails> Tailorpromotiondetails { get; set; }
        public virtual DbSet<Tailorpromotionproductdetails> Tailorpromotionproductdetails { get; set; }
        public virtual DbSet<Tailorstitchingdetails> Tailorstitchingdetails { get; set; }
        public virtual DbSet<Usertype> Usertype { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                    optionsBuilder.UseSqlServer("Server=SQL5080.site4now.net;Database=DB_A6503A_TailorDB;user id=DB_A6503A_TailorDB_admin;password=K@2Te@m123;");

                    logger.Information("Opening connection to TailorDB.");

                }
            }
            catch(Exception ex)
            {
                logger.Error("Error occurred in opening TailorDB connection. Error =" + ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deviceinfo>(entity =>
            {
                entity.HasKey(e => e.Deviceid);

                entity.Property(e => e.Devicetype)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reviewfortailors>(entity =>
            {
                entity.HasKey(e => e.Reviewid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.Reviewcomments)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Reviewrating)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Useremailaddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Userfirstname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Userlastname)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UsertypeId).HasColumnName("UsertypeID");
            });

            modelBuilder.Entity<Tailorloginactivities>(entity =>
            {
                entity.HasKey(e => e.Activityid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Logindatetime).HasColumnType("datetime");

                entity.Property(e => e.Logoutdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tailorotheraddressdetails>(entity =>
            {
                entity.HasKey(e => e.Addressid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.Tailoraddress)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Tailorcity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tailorcountry)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tailorprovince)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tailorzipcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tailorproductdetails>(entity =>
            {
                entity.HasKey(e => e.Productid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.Productcharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Productdescription)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Producttitle)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tailorprofile>(entity =>
            {
                entity.HasKey(e => e.Tailorid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.Tailoraddress).HasMaxLength(300);

                entity.Property(e => e.Tailorcity).HasMaxLength(100);

                entity.Property(e => e.Tailorcnic).HasMaxLength(50);

                entity.Property(e => e.Tailorcompanytitle).HasMaxLength(500);

                entity.Property(e => e.Tailorcountry).HasMaxLength(100);

                entity.Property(e => e.Tailoremailaddress).HasMaxLength(100);

                entity.Property(e => e.Tailorfirstname).HasMaxLength(200);

                entity.Property(e => e.Tailorlastname).HasMaxLength(200);

                entity.Property(e => e.Tailorlatitude).HasMaxLength(20);

                entity.Property(e => e.Tailorlongitude).HasMaxLength(20);

                entity.Property(e => e.Tailormobilenumber).HasMaxLength(50);

                entity.Property(e => e.Tailorothercontactnumber).HasMaxLength(50);

                entity.Property(e => e.Tailorpassword).HasMaxLength(50);

                entity.Property(e => e.Tailorprofilestatus).HasMaxLength(100);

                entity.Property(e => e.Tailorprovince).HasMaxLength(50);

                entity.Property(e => e.Tailorrating).HasMaxLength(50);

                entity.Property(e => e.Tailorwalletamount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Tailorwebsite).HasMaxLength(100);

                entity.Property(e => e.Tailorzipcode).HasMaxLength(50);
            });

            modelBuilder.Entity<Tailorpromotiondetails>(entity =>
            {
                entity.HasKey(e => e.Promotionid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.Pormotionenddatetime).HasColumnType("datetime");

                entity.Property(e => e.Promotiongendertype).HasMaxLength(20);

                entity.Property(e => e.Promotionstartdatetime).HasColumnType("datetime");

                entity.Property(e => e.Publisheddatetime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tailorpromotionproductdetails>(entity =>
            {
                entity.HasKey(e => e.Promotionproductid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Tailorstitchingdetails>(entity =>
            {
                entity.HasKey(e => e.Stitchid);

                entity.Property(e => e.Creationdatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifieddatetime).HasColumnType("datetime");

                entity.Property(e => e.Stitchcharges).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Stitchtype)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usertype>(entity =>
            {
                entity.Property(e => e.Usertype1)
                    .HasColumnName("Usertype")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
