using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TCPT.Models
{
    public partial class SPEARTCPTContext : DbContext
    {
        public SPEARTCPTContext()
        {
        }

        public SPEARTCPTContext(DbContextOptions<SPEARTCPTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Resource> Resource { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TCPT;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(e => e.ResourceId).ValueGeneratedNever();
            });
        }
    }
}
