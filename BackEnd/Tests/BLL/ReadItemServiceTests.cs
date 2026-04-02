using Bogus;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace Tests.BLL
{
    public class ReadItemServiceTests
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

        [Fact]
        public async Task CreateReadItemAsync_ShouldCreateReadItem()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            var readItemDto = new ReadItemDTO
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Type = ReadItemType.Manga
            };
            // Act
            await service.CreateReadItemAsync(readItemDto);
            // Assert
            var entity = await context.ReadItems.FirstOrDefaultAsync(x => x.Title == readItemDto.Title);
            Assert.NotNull(entity);
            Assert.Equal(readItemDto.Type.ToString(), entity.Type);
        }

        [InlineData(null, "Manga", typeof(ArgumentNullException))]
        [InlineData("", "Manga", typeof(ArgumentException))]
        [InlineData("  ", "Manga", typeof(ArgumentException))]
        [Theory]
        public async Task CreateReadItemAsync_ShouldThrowException(string? title, string? type, Type expectedException)
        {
            // Arrange
            var service = new ReadItemService(GetDbContext());
            var readItemDto = new ReadItemDTO
            {
                Id = Guid.NewGuid(),
                Title = title!,
                Type = Enum.Parse<ReadItemType>(type!)  
            };
            // Act & Assert
            await Assert.ThrowsAsync(expectedException, () => service.CreateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task GetItemByIdAsync_GetExistedItem_ShouldGetReadItem()
        {
            //Arrange
            var service = new ReadItemService(GetDbContext());
            var readItem = new ReadItemDTO()
            {
                Id = Guid.NewGuid(),
                Title = "Test title",
                Type = ReadItemType.Manga
            };
            //Act
            await service.CreateReadItemAsync(readItem);
            var item = await service.GetReadItemByIdAsync(readItem.Id);
            //Assert
            Assert.NotNull(item);
            Assert.Equal(readItem.Title, item.Title);
            Assert.Equal(readItem.Type, item.Type);
        }

        [Fact]
        public async Task GetItemByIdAsync_GetUnexistedItem_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await service.GetReadItemByIdAsync(nonExistentId)
            );
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsFromDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);

            context.ReadItems.AddRange(new List<ReadItem>
            {
                new ReadItem { Id = Guid.NewGuid(), Title = "Manga 1", Type = "Manga" },
                new ReadItem { Id = Guid.NewGuid(), Title = "Manga 2", Type = "Manhwa" }
            });
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllReadItemsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, x => x.Title == "Manga 1");
        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnManyItems()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            var mangaFaker = new Faker<ReadItem>()
                        .RuleFor(m => m.Id, f => Guid.NewGuid())
                        .RuleFor(m => m.Title, f => f.Commerce.ProductName())
                        .RuleFor(m => m.Type, f => f.PickRandom("Manga", "Manhwa", "Manhua"));

            // Act
            var readItems = mangaFaker.Generate(50);
            context.ReadItems.AddRange(readItems);
            await context.SaveChangesAsync();
            var result = await service.GetAllReadItemsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(50, result.Count());
        }


    }
}
