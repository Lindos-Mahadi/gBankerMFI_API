using GC.MFI.Models.DbModels;
using GC.MFI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GC.MFI.DataAccess
{
    public partial class GBankerDbContext : DbContext
    {       
        public GBankerDbContext(DbContextOptions<GBankerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
       
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        // Portal Member
        public DbSet<PortalMember> PortalMember { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<Member> Member { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Country> Country { get; set; }
       
        public virtual DbSet<Upozilla> Upozillas { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Union> Union { get; set; }
        public virtual DbSet<NIDLogging> NIDLogging { get; set; }
        public virtual DbSet<Center> Center { get; set; }
        public virtual DbSet<MainProduct> MainProduct { get; set; }
        public virtual DbSet<PortalLoanSummary> PortalLoanSummary { get; set; }
        public virtual DbSet<PortalSavingSummary> PortalSavingSummary { get; set; }
        public virtual DbSet<Purpose> Purpose { get; set; }
        public virtual DbSet<SubMainProduct> SubMainProduct { get; set; }
        public virtual DbSet<ProductList> ProductList { get; set; }

        public virtual DbSet<MemberPassBookRegister> MemberPassBookRegister { get; set; }
        public virtual DbSet<NomineeXPortalSavingSummary> NomineeXPortalSavingSummary { get; set; }

        public virtual DbSet<Investor> Investor { get; set; }

        public virtual DbSet<RepaymentScheduleReportAE> RepaymentScheduleReportAE { get; set; }
        public virtual DbSet<RepaymentScheduleReportD> RepaymentScheduleReportD { get; set; }
        public virtual DbSet<DistrictList> DistrictList { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });
 

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BillNo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.PaymentRef).HasMaxLength(50);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderNo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ReceivedAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ReturnedAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ServiceTypesId).HasColumnName("ServiceTypesID");

                entity.Property(e => e.SiteId).HasColumnName("SiteID");
                entity.Property(e => e.DailyShiftId).HasColumnName("DailyShiftID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(O => O.order)
                .WithMany(M => M.orderDetails);
            //.HasForeignKey(FK => FK.);
            modelBuilder.Entity<NomineeXPortalSavingSummary>()
                .HasOne(O => O.PortalSavingSummary)
                .WithMany(M => M.MemberNomines);

           

            modelBuilder.Entity<Upozilla>()
                .Property(e => e.CreateUser)
                .IsUnicode(false);

            modelBuilder.Entity<District>()
                .Property(e => e.CreateUser)
                .IsUnicode(false);

            modelBuilder.Entity<Division>().HasNoKey();
            modelBuilder.Entity<MainProduct>().HasNoKey();
            modelBuilder.Entity<SubMainProduct>().HasNoKey();
            modelBuilder.Entity<ProductList>().HasNoKey();
            modelBuilder.Entity<RepaymentScheduleReportAE>().HasNoKey();
            modelBuilder.Entity<RepaymentScheduleReportD>().HasNoKey();
            modelBuilder.Entity<DistrictList>().HasNoKey();
          // modelBuilder.Entity<NIDLogging>().0();

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        // STORE PROCEDURE
        //public static List<Division>? FindDivisionFromSql(GBankerDbContext context, string searchFor)
        //{
        //    return context?.Division?.FromSqlRaw($"Proc_GetLocationList {searchFor}").ToList();
        //}

    }
}
