using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpearAutomation.Models.TCPT
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
                optionsBuilder.UseSqlServer("Server=.;Database=TCPT;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.ResourceId);
                entity.ToTable("Resource");
                entity.Property(e => e.ResourceId).ValueGeneratedNever();
            });
        }
    }
}
