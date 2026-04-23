using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccessLayer.DatabaseContext
{
    public class CatalogDBContext : DbContext
    {
        public DbSet<ReadItem> ReadItems { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Page> Pages { get; set; }

        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Project with test use sqlite, so we need to use LENGTH instead of LEN for check constraint
            //All other project use SQL Server, so we need use LEN for check constraint
            var lengthFunctionName = Database.IsSqlite() ? "LENGTH" : "LEN";

            modelBuilder.Entity<ReadItem>(entity =>
            {
                entity.ToTable("ReadItems");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(b => b.Type)
                    .IsRequired();

                entity.ToTable(t => t.HasCheckConstraint("CK_ReadItem_Title_NotEmpty", $"{lengthFunctionName}(TRIM(Title)) > 0"));
                entity.ToTable(t => t.HasCheckConstraint("CK_ReadItem_Type_NotEmpty", $"{lengthFunctionName}(TRIM(Type)) > 0"));
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.ToTable("Chapters");

                entity.HasKey(b => b.Id);

                entity.ToTable(t => t.HasCheckConstraint("CK_Chapter_Order_Min", "[Order] > 0"));
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Pages");
                entity.HasKey(b => b.Id);
                entity.ToTable(t => t.HasCheckConstraint("CK_Page_Image_NotEmpty", "Image IS NOT NULL AND LENGTH(Image) > 0"));
            });
        }
    }
}
