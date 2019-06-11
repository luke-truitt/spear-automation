using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SpearAutomation.Models.MOL
{
    public partial class SPEARMOLContext : DbContext
    {
        private readonly ILogger _logger;
        public SPEARMOLContext()
        {
        }

        public SPEARMOLContext(DbContextOptions<SPEARMOLContext> options, ILogger<SPEARMOLContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public virtual DbSet<Personnel> Personnel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=MOL;Trusted_Connection=True;MultipleActiveResultSets=true");
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
