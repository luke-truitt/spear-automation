using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GCSS.Models
{
    public partial class SPEARGCSSContext : DbContext
    {
        public SPEARGCSSContext()
        {
        }

        public SPEARGCSSContext(DbContextOptions<SPEARGCSSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vehicle> Vehicle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-R5S6QI2;Database=SPEAR.GCSS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Tam);

                entity.Property(e => e.Tam)
                    .HasColumnName("TAM")
                    .HasDefaultValueSql("(newid())");
            });
        }
    }
}
