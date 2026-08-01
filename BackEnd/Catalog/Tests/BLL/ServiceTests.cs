using DatabaseAccessLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Tests.BLL
{
    public class ServiceTests
    {
        protected CatalogDBContext GetDbContext()
        {
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<CatalogDBContext>()
                .UseSqlite(connection)
                .Options;
            var context = new CatalogDBContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
