using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpearAutomation.Models.GCSS
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
                optionsBuilder.UseSqlServer("Server=.;Database=GCSS;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Tam);

                entity.ToTable("Vehicle");

                entity.Property(e => e.Tam).ValueGeneratedNever();
            });
        }
    }
}
