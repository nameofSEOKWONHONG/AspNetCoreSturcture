using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Neon.Payment.Models;
using NeonCore.Library;

namespace Neon.Payment
{
    public partial class PaymentContext : DbContext
    {
        public String ConnectionString { get; }
        public PaymentContext(IOptions<AppSettings> appSettings)
        {
            ConnectionString = appSettings.Value.PaymentInstance;
        }

        public virtual DbSet<TaOrders> TaOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaOrders>(entity =>
            {
                entity.HasKey(e => e.OrdId);

                entity.ToTable("TA_ORDERS");

                entity.Property(e => e.OrdId).HasColumnName("ORD_ID");

                entity.Property(e => e.OrdCost)
                    .HasColumnName("ORD_COST")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OrdUserId).HasColumnName("ORD_USER_ID");

                entity.Property(e => e.RegDt)
                    .HasColumnName("REG_DT")
                    .HasColumnType("datetime");

                entity.Property(e => e.RegId)
                    .HasColumnName("REG_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RegIp)
                    .HasColumnName("REG_IP")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
