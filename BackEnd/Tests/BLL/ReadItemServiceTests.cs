using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DatabaseAccessLayer.DatabaseContext;
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
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            var readItemDto = new ReadItemDTO
            {
                Id = Guid.NewGuid(),
                Title = title!,
                Type = Enum.Parse<ReadItemType>(type!)  
            };
            // Act & Assert
            await Assert.ThrowsAsync(expectedException, () => service.CreateReadItemAsync(readItemDto));
        }
    }
}
