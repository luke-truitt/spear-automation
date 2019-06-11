using Microsoft.EntityFrameworkCore;
using SpearAutomation.Models.Logger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Logger.Data
{
    public class LoggerContext : DbContext
    {
        public virtual DbSet<EventLog> EventLog { get; set; }
        public static int MessageMaxLength;

        public LoggerContext()
        {

        }
        public LoggerContext(DbContextOptions<LoggerContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = SpearLogging.db");
            }
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.ToTable("EventLog");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.LogLevel).HasMaxLength(50);

                entity.Property(e => e.Message).HasMaxLength(4000);
            });
        }
    }
}
