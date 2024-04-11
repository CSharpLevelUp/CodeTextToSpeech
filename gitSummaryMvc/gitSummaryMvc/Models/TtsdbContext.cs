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

    public virtual DbSet<Databasechangelog> Databasechangelogs { get; set; }

    public virtual DbSet<Databasechangeloglock> Databasechangeloglocks { get; set; }

    public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistories { get; set; }

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

        modelBuilder.Entity<Databasechangelog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("databasechangelog");

            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .HasColumnName("author");
            entity.Property(e => e.Comments)
                .HasMaxLength(255)
                .HasColumnName("comments");
            entity.Property(e => e.Contexts)
                .HasMaxLength(255)
                .HasColumnName("contexts");
            entity.Property(e => e.Dateexecuted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateexecuted");
            entity.Property(e => e.DeploymentId)
                .HasMaxLength(10)
                .HasColumnName("deployment_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Exectype)
                .HasMaxLength(10)
                .HasColumnName("exectype");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasColumnName("filename");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Labels)
                .HasMaxLength(255)
                .HasColumnName("labels");
            entity.Property(e => e.Liquibase)
                .HasMaxLength(20)
                .HasColumnName("liquibase");
            entity.Property(e => e.Md5sum)
                .HasMaxLength(35)
                .HasColumnName("md5sum");
            entity.Property(e => e.Orderexecuted).HasColumnName("orderexecuted");
            entity.Property(e => e.Tag)
                .HasMaxLength(255)
                .HasColumnName("tag");
        });

        modelBuilder.Entity<Databasechangeloglock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("databasechangeloglock_pkey");

            entity.ToTable("databasechangeloglock");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Locked).HasColumnName("locked");
            entity.Property(e => e.Lockedby)
                .HasMaxLength(255)
                .HasColumnName("lockedby");
            entity.Property(e => e.Lockgranted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lockgranted");
        });

        modelBuilder.Entity<FlywaySchemaHistory>(entity =>
        {
            entity.HasKey(e => e.InstalledRank).HasName("flyway_schema_history_pk");

            entity.ToTable("flyway_schema_history");

            entity.HasIndex(e => e.Success, "flyway_schema_history_s_idx");

            entity.Property(e => e.InstalledRank)
                .ValueGeneratedNever()
                .HasColumnName("installed_rank");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.InstalledBy)
                .HasMaxLength(100)
                .HasColumnName("installed_by");
            entity.Property(e => e.InstalledOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("installed_on");
            entity.Property(e => e.Script)
                .HasMaxLength(1000)
                .HasColumnName("script");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
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
