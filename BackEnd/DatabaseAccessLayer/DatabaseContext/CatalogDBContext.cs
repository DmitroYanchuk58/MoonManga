using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccessLayer.DatabaseContext
{
    public class CatalogDBContext : DbContext
    {
        public DbSet<ReadItem> ReadItems { get; set; }

        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReadItem>().ToTable("ReadItems");

            modelBuilder.Entity<ReadItem>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<ReadItem>()
            .Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

            modelBuilder.Entity<ReadItem>()
                .Property(b => b.Type)
                .IsRequired();
        }
    }
}
