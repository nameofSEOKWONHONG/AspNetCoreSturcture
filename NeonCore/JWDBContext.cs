using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using NeonCore.Library;
using NeonCore.Models;

namespace NeonCore
{
    public partial class JWDBContext : DbContext
    {
        public String ConnectionString { get; }
        public JWDBContext(IOptions<AppSettings> appSettings)
        {
            ConnectionString = appSettings.Value.JWDBInstance;
        }

        public virtual DbSet<ChangeLog> ChangeLog { get; set; }
        public virtual DbSet<TaUser> TaUser { get; set; }

        // Unable to generate entity type for table 'dbo.Log'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            ////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            ////                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=JWDB;Trusted_Connection=True;");
            //            }

            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChangeLog>(entity =>
            {
                entity.Property(e => e.ChangeLogId)
                    .HasColumnName("ChangeLogID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ChangeDtm).HasColumnType("datetime");

                entity.Property(e => e.ClassNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentValue)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OriginalValue)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserNm)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TaUser>(entity =>
            {
                entity.HasKey(e => e.UserIdx);

                entity.ToTable("TA_USER");

                entity.Property(e => e.UserIdx).HasColumnName("USER_IDX");

                entity.Property(e => e.Addr1)
                    .IsRequired()
                    .HasColumnName("ADDR1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Addr2)
                    .IsRequired()
                    .HasColumnName("ADDR2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Hp)
                    .IsRequired()
                    .HasColumnName("HP")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("PWD")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegId)
                    .HasColumnName("REG_ID")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RegIp)
                    .IsRequired()
                    .HasColumnName("REG_IP")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnName("TEL")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("USER_ID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserNm)
                    .IsRequired()
                    .HasColumnName("USER_NM")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
