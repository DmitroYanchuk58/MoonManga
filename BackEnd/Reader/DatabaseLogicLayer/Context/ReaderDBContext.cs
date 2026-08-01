using Microsoft.EntityFrameworkCore;
using DatabaseLogicLayer.Entities;

namespace DatabaseAccessLayer.DatabaseContext
{
    public class ReaderDBContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public ReaderDBContext(DbContextOptions<ReaderDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(b => b.Id);

                // Configure table name AND check constraints together
                entity.ToTable("Pages", t =>
                {
                    t.HasCheckConstraint("CK_Page_Order_Min", "[Order] > 0");
                    t.HasCheckConstraint("CK_Page_Image_NotEmpty", "Image IS NOT NULL AND DATALENGTH(Image) > 0");
                });
            });
        }
    }
}