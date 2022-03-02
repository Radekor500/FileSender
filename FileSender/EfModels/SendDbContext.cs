using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FileSender.EfModels
{
    public partial class SendDbContext : DbContext
    {
        public SendDbContext()
        {
        }

        public SendDbContext(DbContextOptions<SendDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FileContent> FileContents { get; set; } = null!;
        public virtual DbSet<FileUpload> FileUploads { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BJHILMQ;Database=SendDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileContent>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FileContent1).HasColumnName("FileContent");

                entity.Property(e => e.FileId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FileName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.FileUpload)
                    .WithMany()
                    .HasForeignKey(d => d.FileUploadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FileConte__FileU__5CD6CB2B");
            });

            modelBuilder.Entity<FileUpload>(entity =>
            {
                entity.ToTable("FileUpload");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.IsViewed).HasDefaultValueSql("((0))");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
