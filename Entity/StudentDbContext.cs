using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentRegistrationInCore.Entity;

public partial class StudentDbContext : DbContext
{
    public StudentDbContext()
    {
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sysdiagram> Sysdiagrams { get; set; }

    public virtual DbSet<TblClass> TblClasses { get; set; }

    public virtual DbSet<TblDocument> TblDocuments { get; set; }

    public virtual DbSet<TblGender> TblGenders { get; set; }

    public virtual DbSet<TblHobby> TblHobbies { get; set; }

    public virtual DbSet<TblImage> TblImages { get; set; }

    public virtual DbSet<TblMapping> TblMappings { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-3EKTNG4;Database=StudentDb;user=sa;password=1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sysdiagram>(entity =>
        {
            entity.HasKey(e => e.DiagramId);

            entity.ToTable("sysdiagrams");

            entity.Property(e => e.DiagramId).HasColumnName("diagram_id");
            entity.Property(e => e.Definition).HasColumnName("definition");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name");
            entity.Property(e => e.PrincipalId).HasColumnName("principal_id");
            entity.Property(e => e.Version).HasColumnName("version");
        });

        modelBuilder.Entity<TblClass>(entity =>
        {
            entity.HasKey(e => e.ClassId);

            entity.ToTable("tblClasses");

            entity.Property(e => e.ClassTeacher)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblDocument>(entity =>
        {
            entity.HasKey(e => e.DocId);

            entity.ToTable("tblDocuments");

            entity.Property(e => e.DocPath)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblGender>(entity =>
        {
            entity.HasKey(e => e.GenderId);

            entity.ToTable("tblGenders");

            entity.Property(e => e.GenderName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblHobby>(entity =>
        {
            entity.HasKey(e => e.HobbyId);

            entity.ToTable("tblHobby");

            entity.Property(e => e.HobbyName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("tblImages");

            entity.Property(e => e.ImagePath)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblMapping>(entity =>
        {
            entity.HasKey(e => e.MapId);

            entity.ToTable("tblMapping");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.ToTable("tblStudents");

            entity.HasIndex(e => e.ClassId, "IX_FK_tblStudent_tblClass");

            entity.HasIndex(e => e.GenderId, "IX_FK_tblStudent_tblGender");

            entity.HasIndex(e => e.ImageId, "IX_FK_tblStudent_tblImage");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Hobbies)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegisteredDate).HasColumnType("datetime");

            entity.HasOne(d => d.Class).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblStudent_tblClass");

            entity.HasOne(d => d.Doc).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.DocId)
                .HasConstraintName("FK_tblStudents_tblDocuments");

            entity.HasOne(d => d.Gender).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_tblStudent_tblGender");

            entity.HasOne(d => d.Image).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_tblStudent_tblImage");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
