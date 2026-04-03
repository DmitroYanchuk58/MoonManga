using Bogus;
using Bogus.DataSets;
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
        [InlineData("", "Manga", false)]
        [InlineData("Valid Title", "", false)]
        [InlineData("   ", "Manga", false)]
        [InlineData("Valid Title", "   ", false)]
        public async Task CreateAsync(string? title, string? type, bool shouldSucceed)
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

        [Fact]
        public async Task CreateAsync_CheckData_ShouldThrowException()
        {
            // Arrange
            using var context = GetDbContext();
            var faker = new Faker();
            var repository = new CrudRepository<ReadItem>(context);
            var tooLongTitleReadItem = new ReadItem() { Id = Guid.NewGuid(), Title = faker.Random.String2(100), Type = "Manga" };
            var tooLongTypeReadItem = new ReadItem() { Id = Guid.NewGuid(), Title = "Test Title", Type = faker.Random.String2(100) };
            await repository.CreateAsync(tooLongTitleReadItem);
            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(tooLongTitleReadItem));
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<ReadItem>(context);
            var readItem = new ReadItem() { Id = Guid.NewGuid(), Title = "Test Title", Type = "Manga" };
            await repository.CreateAsync(readItem);
            // Act
            var result = await repository.GetByIdAsync(readItem.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(readItem.Title, result.Title);
            Assert.Equal(readItem.Type, result.Type);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<ReadItem>(context);
            var mangaFaker = new Faker<ReadItem>()
                                    .RuleFor(m => m.Id, f => Guid.NewGuid())
                                    .RuleFor(m => m.Title, f => f.Commerce.ProductName())
                                    .RuleFor(m => m.Type, f => f.PickRandom("Manga", "Manhwa", "Manhua"));

            var readItems = mangaFaker.Generate(50);
            foreach (var item in readItems)
            {
                await repository.CreateAsync(item);
            }
            // Act
            var result = await repository.GetAllAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(50, result.Count());
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<ReadItem>(context);
            var readItem = new ReadItem() { Id = Guid.NewGuid(), Title = "Test Title", Type = "Manga" };
            await repository.CreateAsync(readItem);
            // Act
            readItem.Title = "Updated Title";
            readItem.Type = "Manhwa";
            await repository.UpdateAsync(readItem);
            var updatedEntity = await context.ReadItems.FirstOrDefaultAsync(x => x.Id == readItem.Id);
            // Assert
            Assert.NotNull(updatedEntity);
            Assert.Equal("Updated Title", updatedEntity.Title);
            Assert.Equal("Manhwa", updatedEntity.Type);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<ReadItem>(context);
            var readItem = new ReadItem() { Id = Guid.NewGuid(), Title = "Test Title", Type = "Manga" };
            await repository.CreateAsync(readItem);
            context.ChangeTracker.Clear();
            // Act
            await repository.DeleteAsync(readItem.Id);
            var deletedEntity = await context.ReadItems.FirstOrDefaultAsync(x => x.Id == readItem.Id);
            // Assert
            Assert.Null(deletedEntity);
        }
    }
}
