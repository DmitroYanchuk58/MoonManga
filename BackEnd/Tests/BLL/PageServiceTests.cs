using Bogus;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ValidationException = FluentValidation.ValidationException;

namespace Tests.BLL
{
    public class PageServiceTests : ServiceTests
    {
        private readonly IPageService _service;
        private readonly Faker _faker;

        public PageServiceTests()
        {
            _service = new PageService(GetDbContext());
            _faker = new Faker();
        }

        [Fact]
        public async Task CreatePageAsync_ValidDto_SavesToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);
            var dto = new PageDTO
            {
                Order = 1,
                Image = _faker.Random.Bytes(100)
            };

            // Act
            await service.CreatePageAsync(dto);

            // Assert
            var savedPage = await context.Pages.FirstOrDefaultAsync(p => p.Order == dto.Order);

            Assert.NotNull(savedPage);
            Assert.Equal(dto.Image, savedPage.Image);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public async Task CreatePageAsync_InvalidOrder_ThrowsValidationException(int invalidOrder)
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);
            var dto = new PageDTO { Order = invalidOrder, Image = _faker.Random.Bytes(10) };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.CreatePageAsync(dto));

            var count = await context.Pages.CountAsync();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task CreatePageAsync_NullImage_ThrowsValidationException()
        {
            // Arrange
            var service = new PageService(GetDbContext());
            var dto = new PageDTO { Order = 1, Image = null! };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.CreatePageAsync(dto));
        }

        [Fact]
        public async Task CreatePageAsync_NullDto_ThrowsArgumentNullException()
        {
            // Arrange
            var service = new PageService(GetDbContext());
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.CreatePageAsync(null!));
        }

        [Fact]
        public async Task CreatePageAsync_EmptyImage_ThrowsValidationException()
        {
            // Arrange
            var service = new PageService(GetDbContext());
            var dto = new PageDTO { Order = 1, Image = Array.Empty<byte>() };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.CreatePageAsync(dto));
        }

        [Fact]
        public async Task CreatePageAsync_DuplicateId_ThrowsInvalidOperationException()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);
            var dto1 = new PageDTO { Id = Guid.NewGuid(), Order = 1, Image = _faker.Random.Bytes(10) };
            var dto2 = new PageDTO { Id = dto1.Id, Order = 2, Image = _faker.Random.Bytes(10) };
            await service.CreatePageAsync(dto1);
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreatePageAsync(dto2));
        }

        [Fact]
        public async Task CreatePageAsync_EmptyDTO_ThrowsValidationException()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.CreatePageAsync(new PageDTO()));
        }

        [Fact]
        public async Task GetPageByIdAsync_ExistingId_ReturnsCorrectDto()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            var pageId = Guid.NewGuid();
            var testImage = _faker.Random.Bytes(50);
            var testOrder = 5;

            var entity = new PageDTO
            {
                Id = pageId,
                Order = testOrder,
                Image = testImage
            };
            context.Pages.Add(entity.ConvertToEntity());
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetPageByIdAsync(pageId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testOrder, result.Order);
            Assert.Equal(testImage, result.Image);
        }

        [Fact]
        public async Task GetPageByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);
            var randomId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.GetPageByIdAsync(randomId));
        }

        [Fact]
        public async Task GetPageByIdAsync_EmptyGuid_ThrowsArgumentNullException()
        {
            // Arrange
            var service = new PageService(GetDbContext());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.GetPageByIdAsync(Guid.Empty));
        }

        [Fact]
        public async Task GetAllPagesAsync_WithExistingData_ReturnsAllDtos()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            var pagesToCreate = new List<PageDTO>
            {
                new PageDTO { Id = Guid.NewGuid(), Order = 1, Image = _faker.Random.Bytes(20) },
                new PageDTO { Id = Guid.NewGuid(), Order = 2, Image = _faker.Random.Bytes(20) },
                new PageDTO { Id = Guid.NewGuid(), Order = 3, Image = _faker.Random.Bytes(20) }
            };

            context.Pages.AddRange(pagesToCreate.Select(dto => dto.ConvertToEntity()));
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllPagesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            Assert.Contains(result, dto => dto.Order == 1);
            Assert.Contains(result, dto => dto.Order == 2);
            Assert.Contains(result, dto => dto.Order == 3);
        }

        [Fact]
        public async Task GetAllPagesAsync_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            // Act
            var result = await service.GetAllPagesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllPagesAsync_ReturnsCorrectDataTypes()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            context.Pages.Add((new PageDTO { Id = Guid.NewGuid(), Order = 1, Image = _faker.Random.Bytes(10) }).ConvertToEntity());
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllPagesAsync();

            // Assert
            var firstDto = result.First();
            Assert.IsType<PageDTO>(firstDto);
            Assert.IsType<byte[]>(firstDto.Image);
        }

        [Fact]
        public async Task UpdatePageAsync_ValidDto_UpdatesExistingRecord()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            var originalId = Guid.NewGuid();
            var originalPage = new PageDTO { Id = originalId, Order = 1, Image = _faker.Random.Bytes(10) };
            await service.CreatePageAsync(originalPage);
            context.ChangeTracker.Clear(); 

            var updateDto = new PageDTO
            {
                Id = originalId,
                Order = 99,
                Image = _faker.Random.Bytes(200)
            };

            // Act
            await service.UpdatePageAsync(updateDto);

            // Assert
            var updatedEntity = await context.Pages.FindAsync(originalId);
            Assert.NotNull(updatedEntity);
            Assert.Equal(updateDto.Order, updatedEntity.Order);
            Assert.Equal(updateDto.Image, updatedEntity.Image);
        }

        [Fact]
        public async Task UpdatePageAsync_NonExistingId_ThrowsValidationException()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);
            var dto = new PageDTO { Id = Guid.NewGuid(), Order = 1, Image = _faker.Random.Bytes(10) };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.UpdatePageAsync(dto));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public async Task UpdatePageAsync_InvalidOrder_ThrowsValidationException(int invalidOrder)
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            var id = Guid.NewGuid();
            context.Pages.Add((new PageDTO { Id = id, Order = 1, Image = _faker.Random.Bytes(10) }).ConvertToEntity());
            await context.SaveChangesAsync();

            var dto = new PageDTO { Id = id, Order = invalidOrder, Image = _faker.Random.Bytes(10) };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.UpdatePageAsync(dto));
        }

        [Fact]
        public async Task UpdatePageAsync_NullImage_ThrowsValidationException()
        {
            // Arrange
            var service = new PageService(GetDbContext());
            var dto = new PageDTO { Id = Guid.NewGuid(), Order = 1, Image = null! };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.UpdatePageAsync(dto));
        }

        [Fact]
        public async Task UpdatePageAsync_NullDto_ThrowsArgumentNullException()
        {
            // Arrange
            var service = new PageService(GetDbContext());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.UpdatePageAsync(null!));
        }

        [Fact]
        public async Task UpdatePageAsync_EmptyImage_ThrowsValidationException()
        {
            // Arrange
            var service = new PageService(GetDbContext());
            var dto = new PageDTO { Id = Guid.NewGuid(), Order = 1, Image = Array.Empty<byte>() };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                service.UpdatePageAsync(dto));
        }

        [Fact]
        public async Task DeletePageAsync_ExistingId_RemovesFromDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            var id = Guid.NewGuid();
            var page = (new PageDTO { Id = id, Order = 1, Image = _faker.Random.Bytes(10) }).ConvertToEntity();

            context.Pages.Add(page);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            // Act
            await service.DeletePageAsync(id);

            // Assert
            var deletedPage = await context.Pages.FindAsync(id);
            Assert.Null(deletedPage);

            var count = await context.Pages.CountAsync();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task DeletePageAsync_EmptyGuid_ThrowsValidationException()
        {
            // Arrange
            var service = new PageService(GetDbContext());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.DeletePageAsync(Guid.Empty));
        }

        [Fact]
        public async Task DeletePageAsync_MultipleRecords_OnlyDeletesTarget()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PageService(context);

            var idToDelete = Guid.NewGuid();
            var idToKeep = Guid.NewGuid();

            await service.CreatePageAsync(new PageDTO { Id = idToDelete, Order = 1, Image = _faker.Random.Bytes(5) });
            await context.SaveChangesAsync();
            await service.CreatePageAsync(new PageDTO { Id = idToKeep, Order = 2, Image = _faker.Random.Bytes(5) });
            await context.SaveChangesAsync();

            // Act
            await service.DeletePageAsync(idToDelete);

            await context.SaveChangesAsync();

            // Assert
            var remainingPage = await context.Pages.FindAsync(idToKeep);
            await context.SaveChangesAsync();
            var deletedPage = await context.Pages.FindAsync(idToDelete);

            Assert.Null(deletedPage);
            Assert.NotNull(remainingPage);
            Assert.Equal(1, await context.Pages.CountAsync());
        }
    }
}
