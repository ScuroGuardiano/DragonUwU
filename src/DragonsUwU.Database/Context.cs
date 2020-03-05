using Microsoft.EntityFrameworkCore;

namespace DragonsUwU.Database {
    class DragonContext : DbContext
    {
        public DbSet<Dragon> Dragons { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=dragon.sqlite"); // TODO: Replace it with value from config

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