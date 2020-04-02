using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WeltenretterDemo.Models
{
    public partial class WeltenretterDemoContext : DbContext
    {
        public virtual DbSet<Agressor> Agressor { get; set; }
        public virtual DbSet<Held> Held { get; set; }
        public virtual DbSet<HeldAgressor> HeldAgressor { get; set; }

        public WeltenretterDemoContext(DbContextOptions<WeltenretterDemoContext> options) : base(options) { }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WeltenretterDemo;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agressor>(entity =>
            {
                entity.ToTable("agressor");

                entity.Property(e => e.AgressorId)
                    .HasColumnName("agressor_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Agressorname)
                    .HasColumnName("agressorname")
                    .HasColumnType("char(20)");
            });

            modelBuilder.Entity<Held>(entity =>
            {
                entity.Property(e => e.HeldId)
                    .HasColumnName("held_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Heldname)
                    .HasColumnName("heldname")
                    .HasColumnType("char(20)");
            });

            modelBuilder.Entity<HeldAgressor>(entity =>
            {
                entity.Property(e => e.HeldagressorId)
                    .HasColumnName("heldagressor_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AgressorId).HasColumnName("agressor_id");

                entity.Property(e => e.HeldId).HasColumnName("held_id");

                entity.HasOne(d => d.Agressor)
                    .WithMany(p => p.HeldAgressor)
                    .HasForeignKey(d => d.AgressorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("heldagressor_agressor");

                entity.HasOne(d => d.Held)
                    .WithMany(p => p.HeldAgressor)
                    .HasForeignKey(d => d.HeldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("heldagressor_held");
            });
        }
    }
}
