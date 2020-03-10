using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using DragonsUwU.Database.Models;

namespace DragonsUwU.Database {
    class DragonContext : DbContext
    {
        public DbSet<Dragon> Dragons { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public static readonly ILoggerFactory Logger
            = LoggerFactory.Create(builder => builder.AddConsole());

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            if(DragonConfiguration.Config == null)
            {
                DragonConfiguration.LoadConfiguration(); // Needed for Entity Framework to generate migrations and update db
            }
            options
                .UseSqlite(DragonConfiguration.Config.ConnectionString);
        }

                //.UseLoggerFactory(Logger)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DragonTag>()
                .HasKey(t => new { t.DragonId, t.TagId });
            
            modelBuilder.Entity<DragonTag>()
                .HasOne(dt => dt.Dragon)
                .WithMany(d => d.DragonTags)
                .HasForeignKey(dt => dt.DragonId);

            modelBuilder.Entity<DragonTag>()
                .HasOne(dt => dt.Tag)
                .WithMany(t => t.DragonTags)
                .HasForeignKey(dt => dt.TagId);
        }
    }
}