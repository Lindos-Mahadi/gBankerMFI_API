﻿using GC.MFI.Models.DbModels;
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
        public virtual DbSet<MemberToPHCMapping> MemberToPHCMapping { get; set; }
        public virtual DbSet<Upozilla> Upozillas { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Union> Union { get; set; }
        public virtual DbSet<NIDLogging> NIDLogging { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<Center> Center { get; set; }
        public virtual DbSet<MainProduct> MainProduct { get; set; }
        public virtual DbSet<PortalLoanSummary> PortalLoanSummary { get; set; }
        public virtual DbSet<LoanSummary> LoanSummary { get; set; }
        public virtual DbSet<PortalSavingSummary> PortalSavingSummary { get; set; }
        public virtual DbSet<Purpose> Purpose { get; set; }
        public virtual DbSet<SubMainProduct> SubMainProduct { get; set; }
        public virtual DbSet<ProductList> ProductList { get; set; }

        public virtual DbSet<MemberPassBookRegister> MemberPassBookRegister { get; set; }
        public virtual DbSet<NomineeXPortalSavingSummary> NomineeXPortalSavingSummary { get; set; }

        public virtual DbSet<SavingSummary> SavingSummary { get; set; }
        public virtual DbSet<NomineeXSavingSummary> NomineeXSavingSummary { get; set; }
        public virtual DbSet<Investor> Investor { get; set; }

        public virtual DbSet<RepaymentScheduleReportAE> RepaymentScheduleReportAE { get; set; }
        public virtual DbSet<RepaymentScheduleReportD> RepaymentScheduleReportD { get; set; }
        public virtual DbSet<RepaymentScheduleReportF> RepaymentScheduleReportF { get; set; }
        public virtual DbSet<DistrictList> DistrictList { get; set; }
        public virtual DbSet<UpozillaList> UpozillaList { get; set; }
        public virtual DbSet<ProductListForSavingSummary> ProductListForSavingSummary { get; set; }
        public virtual DbSet<FileUploadTable> FileUploadTable { get; set; }
        public virtual DbSet<VillageList> VillageList { get; set; }
        public virtual DbSet<UnionList> UnionList { get; set; }
        public virtual DbSet<SavingsAccClose> SavingsAccClose { get; set; }
        public virtual DbSet<LoanAccReschedule> LoanAccReschedule { get; set; }
        public virtual DbSet<SMSLogTable> SMSLogTable { get; set; }
        public virtual DbSet<LoanLedger> LoanLedger { get; set; }
        public virtual DbSet<SavingLedger> SavingLedger { get; set; }
        public virtual DbSet<EmailLogTable> EmailLogTable { get; set; }
        public virtual DbSet<NotificationTable> NotificationTable { get; set; }
        public virtual DbSet<SignalRConnectionTable> SignalRConnectionTable { get; set; }
        public virtual DbSet<FcmConnectionTable> FcmConnectionTable { get; set; }


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


            modelBuilder.Entity<NomineeXSavingSummary>().HasNoKey();
            //modelBuilder.Entity<NomineeXSavingSummary>()
            //    .HasOne(O => O.SavingSummary)
            //    .WithMany(O => O.MemberNomines);


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
            modelBuilder.Entity<RepaymentScheduleReportF>().HasNoKey();
            modelBuilder.Entity<DistrictList>().HasNoKey();
            modelBuilder.Entity<UpozillaList>().HasNoKey();
            modelBuilder.Entity<VillageList>().HasNoKey();
            modelBuilder.Entity<UnionList>().HasNoKey();
            modelBuilder.Entity<ProductListForSavingSummary>().HasNoKey();
            modelBuilder.Entity<LoanLedger>().HasNoKey();
            modelBuilder.Entity<SavingLedger>().HasNoKey();
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