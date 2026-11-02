using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Q2.Entities;

namespace Q2;

public partial class Prn2123Context : DbContext
{
    public Prn2123Context()
    {
    }

    public Prn2123Context(DbContextOptions<Prn2123Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123456;database=PRN212_3;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E154A62BD");

            entity.Property(e => e.GenreName).HasMaxLength(100);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941AAB08B073");

            entity.Property(e => e.RatingCode).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(150);

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movies_Genres");

            entity.HasOne(d => d.Studio).WithMany(p => p.Movies)
                .HasForeignKey(d => d.StudioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movies_Studios");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("PK__Studios__4ACC3B708C54F92E");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.StudioName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
