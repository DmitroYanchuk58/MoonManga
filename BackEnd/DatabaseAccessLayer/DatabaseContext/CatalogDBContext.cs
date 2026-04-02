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
            modelBuilder.Entity<ReadItem>(entity =>
            {
                entity.ToTable("ReadItems");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(b => b.Type)
                    .IsRequired();

                entity.ToTable(t => t.HasCheckConstraint("CK_ReadItem_Title_NotEmpty", "LEN(TRIM(Title)) > 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_ReadItem_Type_NotEmpty", "LEN(TRIM(Type)) > 0"));
            });
        }
    }
}
