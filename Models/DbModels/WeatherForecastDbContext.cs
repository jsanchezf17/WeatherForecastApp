using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeatherForecastApp.Models;

public partial class WeatherForecastDbContext : DbContext
{
    public WeatherForecastDbContext()
    {
    }

    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coordinate> Coordinates { get; set; }

    public virtual DbSet<WeatherForecastByCoordinatesDb> WeatherForecastByCoordinatesDbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=WeatherForecastDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coordinate>(entity =>
        {
            entity.HasKey(e => e.CoordinateId).HasName("PK__Coordina__05E88A83336EC4E8");

            entity.Property(e => e.CoordinateId).HasColumnName("CoordinateID");
            entity.Property(e => e.Latitude).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<WeatherForecastByCoordinatesDb>(entity =>
        {
            entity.HasKey(e => e.ForecastId).HasName("PK__WeatherF__7F27445878EA4AB7");

            entity.ToTable("WeatherForecastByCoordinatesDB");

            entity.Property(e => e.ForecastId).HasColumnName("ForecastID");
            entity.Property(e => e.CoordinateId).HasColumnName("CoordinateID");
            entity.Property(e => e.CurrentPrecipitation).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CurrentTemperature)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CurrentTime).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.HumidityUnit)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PrecipitationUnit)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.TemperatureUnit)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.TimeZone)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.Coordinate).WithMany(p => p.WeatherForecastByCoordinatesDbs)
                .HasForeignKey(d => d.CoordinateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WeatherFo__Coord__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
