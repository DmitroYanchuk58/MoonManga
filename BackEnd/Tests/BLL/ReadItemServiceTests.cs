using Bogus;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;
using BusinessLogicLayer.DTOs.Enums;

namespace Tests.BLL
{
    public class ReadItemServiceTests : ServiceTests
    {
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

        [InlineData(null, "Manga")]
        [InlineData("", "Manga")]
        [InlineData("  ", "Manga")]
        [Theory]
        public async Task CreateReadItemAsync_ShouldThrowException(string? title, string? type)
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
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task CreateReadItemAsync_ShouldThrowExceptionWithNullReadItem()
        {
            // Arrange
            var service = new ReadItemService(GetDbContext());
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateReadItemAsync(null!));
        }

        [Fact]
        public async Task CreateReadItemAsync_ShouldThrowExceptionWithInvalidType()
        {
            // Arrange
            var service = new ReadItemService(GetDbContext());
            var readItemDto = new ReadItemDTO
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Type = (ReadItemType)999 
            };
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateReadItemAsync(readItemDto));
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
        public async Task GetItemByIdAsync_GetWithNullId_ShouldThrowArgumentException()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.GetReadItemByIdAsync(Guid.Empty)
            );
        }

        [Fact]
        public async Task GetItemByIdAsync_GetWithNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await service.GetReadItemByIdAsync(Guid.Parse(null!))
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

        [Fact]
        public async Task DeleteAsync_ExistingItem_ShouldRemoveFromDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            var item = new ReadItem { Id = Guid.NewGuid(), Title = "To Delete", Type = "Manga" };
            context.ReadItems.Add(item);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();


            // Act
            await service.DeleteReadItemAsync(item.Id);
            var deletedItem = await context.ReadItems.FindAsync(item.Id);

            // Assert
            Assert.Null(deletedItem);
        }

        [Fact]
        public async Task DeleteAsync_NotEsixtingItem_ShouldNotThrow()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);

            // Act
            var exception = await Record.ExceptionAsync(async () =>
                await service.DeleteReadItemAsync(Guid.NewGuid())
            );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteAsync_NullId_ShouldThrow()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteReadItemAsync(Guid.Empty));
        }

        [Fact]
        public async Task UpdateReadItemAsync_ShouldUpdateReadItem()
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
            await context.ReadItems.AddAsync(readItemDto.ToReadItem());
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            readItemDto.Title = "Updated Title";
            // Act 
            await service.UpdateReadItemAsync(readItemDto);
            context.ChangeTracker.Clear();
            var updatedItem = await context.ReadItems
                .FirstOrDefaultAsync(x => x.Id == readItemDto.Id);

            // Assert
            Assert.NotNull(updatedItem);
            Assert.Equal("Updated Title", updatedItem.Title);
        }

        [Fact]
        public async Task UpdateReadItemAsync_UpdateNotExistedReadItem_ShouldThrowKeyNotFoundException()
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
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task UpdateReadItemAsync_UpdateWithInvalidData_ShouldThrowArgumentException()
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
            await service.CreateReadItemAsync(readItemDto);
            readItemDto.Title = "";
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.UpdateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task UpdateReadItemAsync_UpdateWithNullData_ShouldThrowArgumentNullException()
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
            await service.CreateReadItemAsync(readItemDto);
            readItemDto.Title = null!;
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.UpdateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task UpdateReadItemAsync_UpdateWithInvalidType_ShouldThrowArgumentException()
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
            await service.CreateReadItemAsync(readItemDto);
            context.ChangeTracker.Clear();
            readItemDto.Type = (ReadItemType)999; 
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.UpdateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task UpdateReadItemAsync_UpdateWithToLongTitle_ShouldThrowArgumentNullException()
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
            await service.CreateReadItemAsync(readItemDto);
            var faker = new Bogus.Faker();

            string tooLongTitle = faker.Random.String2(101);
            readItemDto.Title = tooLongTitle;
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.UpdateReadItemAsync(readItemDto));
        }

        [Fact]
        public async Task UpdateReadItemAsync_UpdateWithNullReadItem_ShouldThrowNullArgumentNullException()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ReadItemService(context);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateReadItemAsync(null!));
        }
    }
}
