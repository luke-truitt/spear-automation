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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
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
