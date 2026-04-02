using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Tests.DAL
{
    public class CrudTests
    {
        private CatalogDBContext GetDbContext()
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

        [Theory]
        [InlineData("Valid Title", "Manga", true)]  
        [InlineData(null, "Manga", false)]     
        [InlineData("Valid Title", null, false)]     
        public async Task CreateAsync_ValidationScenario_WorksAsExpected(string? title, string? type, bool shouldSucceed)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<ReadItem>(context);
            var readItem = new ReadItem()   
            { 
                Id = Guid.NewGuid(), 
                Title = title!, 
                Type = type! 
            };

            // Act & Assert
            if (shouldSucceed)
            {
                await repository.CreateAsync(readItem);
                var entity = await context.ReadItems.FirstOrDefaultAsync(x => x.Title == title);

                Assert.NotNull(entity);
                Assert.Equal(type, entity.Type);
            }
            else
            {
                await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(readItem));
            }
        }
    }
}
