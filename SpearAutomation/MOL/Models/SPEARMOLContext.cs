using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MOL.Models
{
    public partial class SPEARMOLContext : DbContext
    {
        public SPEARMOLContext()
        {
        }

        public SPEARMOLContext(DbContextOptions<SPEARMOLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Personnel> Personnel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=MOL;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.HasKey(e => e.MarineId);
                entity.ToTable("Personnel");
                entity.Property(e => e.MarineId).ValueGeneratedNever();
            });
        }
    }
}
