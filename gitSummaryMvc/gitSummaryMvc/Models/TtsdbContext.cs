using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gitSummaryMvc.Models;

public partial class TtsdbContext : DbContext
{
    public TtsdbContext()
    {
    }

    public TtsdbContext(DbContextOptions<TtsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Commit> Commits { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=texttospeechdb.cltqgxhlp0db.eu-west-1.rds.amazonaws.com;Database=ttsdb;Username=dbadmin;Password=RDS_password_123_");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Commit>(entity =>
        {
            entity.HasKey(e => e.Commitid).HasName("commits_pkey");

            entity.ToTable("commits");

            entity.Property(e => e.Commitid).HasColumnName("commitid");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Diff).HasColumnName("diff");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Commits)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("commits_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
