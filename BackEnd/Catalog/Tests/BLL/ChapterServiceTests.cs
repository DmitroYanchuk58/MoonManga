using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using FluentValidation;

namespace Tests.BLL
{
    public class ChapterServiceTests : ServiceTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        public async Task CreateChapter_ShouldAddChapterToDatabase(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = order
            };
            // Act
            await service.CreateChapterAsync(chapterDto);
            // Assert
            var chapterInDb = await context.Chapters.FindAsync(chapterDto.Id);
            Assert.NotNull(chapterInDb);
            Assert.Equal(chapterDto.Order, chapterInDb.Order);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public async Task CreateChapter_ShouldThrowValidationException_WhenOrderIsInvalid(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = order
            };
            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.CreateChapterAsync(chapterDto));
        }

        [Fact]
        public async Task CreateChapter_ShouldThrowArgumentException_WhenChapterAlreadyExists()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = 1
            };
            await service.CreateChapterAsync(chapterDto);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateChapterAsync(chapterDto));
        }

        [Fact]
        public async Task CreateChapter_ShouldThrowNullArgumentException_WhenChapterIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterEmptyId = new ChapterDTO
            {
                Id = Guid.Empty,
                Order = 1
            };
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateChapterAsync(null!));
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateChapterAsync(chapterEmptyId));
        }

        [Fact]
        public async Task GetChapterById_ShouldReturnChapter_WhenChapterExists()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = 1
            };
            await service.CreateChapterAsync(chapterDto);
            context.ChangeTracker.Clear();
            // Act
            var retrievedChapter = await service.GetChapterByIdAsync(chapterDto.Id);
            // Assert
            Assert.NotNull(retrievedChapter);
            Assert.Equal(chapterDto.Id, retrievedChapter.Id);
            Assert.Equal(chapterDto.Order, retrievedChapter.Order);
        }

        [Fact]
        public async Task GetChapterById_ShouldThrowKeyNotFoundException_WhenChapterDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var nonExistentId = Guid.NewGuid();
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetChapterByIdAsync(nonExistentId));
        }

        [Fact]
        public async Task GetChapterById_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetChapterByIdAsync(Guid.Empty));
        }

        [Fact]
        public async Task GetAllChapters_ShouldReturnAllChapters()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapter1 = new ChapterDTO { Id = Guid.NewGuid(), Order = 1 };
            var chapter2 = new ChapterDTO { Id = Guid.NewGuid(), Order = 2 };
            await service.CreateChapterAsync(chapter1);
            await service.CreateChapterAsync(chapter2);
            context.ChangeTracker.Clear();
            // Act
            var chapters = await service.GetAllChaptersAsync();
            // Assert
            Assert.NotNull(chapters);
            Assert.Equal(2, chapters.Count);
            Assert.Contains(chapters, c => c.Id == chapter1.Id && c.Order == chapter1.Order);
            Assert.Contains(chapters, c => c.Id == chapter2.Id && c.Order == chapter2.Order);
        }

        [Fact]
        public async Task GetAllChapters_200Chapters_ShouldReturnAllChapters()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            for (int order = 1; order <= 200; order++)
            {
                var chapterDto = new ChapterDTO
                {
                    Id = Guid.NewGuid(),
                    Order = order
                };
                await service.CreateChapterAsync(chapterDto);
                context.ChangeTracker.Clear();
            }
            // Act
            var chapters = await service.GetAllChaptersAsync();
            // Assert
            Assert.NotNull(chapters);
            Assert.All(chapters, c => Assert.InRange(c.Order, 1, 200));
        }

        [Fact]
        public async Task GetAllChapters_NoChapters_ShouldReturnEmptyList()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            // Act
            var chapters = await service.GetAllChaptersAsync();
            // Assert
            Assert.NotNull(chapters);
            Assert.Empty(chapters);
        }

        [Fact]
        public async Task DeleteChapter_ShouldRemoveChapterFromDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = 1
            };
            await service.CreateChapterAsync(chapterDto);
            context.ChangeTracker.Clear();
            // Act
            await service.DeleteChapterAsync(chapterDto.Id);
            // Assert
            var chapterInDb = await context.Chapters.FindAsync(chapterDto.Id);
            Assert.Null(chapterInDb);
        }

        [Fact]
        public async Task DeleteChapter_ShouldNotThrowException_WhenKeyDoesntExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var nonExistentId = Guid.NewGuid();
            var exception = await Record.ExceptionAsync(async () => await service.DeleteChapterAsync(nonExistentId));
            // Act & Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteChapter_ShouldNotThrowException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            // Act & Assert
            var exception = await Record.ExceptionAsync(async () => await service.DeleteChapterAsync(Guid.Empty));
            Assert.Null(exception);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        public async Task UpdateChapter_ShouldUpdateChapterInDatabase(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = 1
            };
            await service.CreateChapterAsync(chapterDto);
            context.ChangeTracker.Clear();
            // Act
            chapterDto.Order = order;
            await service.UpdateChapterAsync(chapterDto);
            context.ChangeTracker.Clear();
            var updatedChapter = await context.Chapters.FindAsync(chapterDto.Id);

            // Assert
            Assert.NotNull(updatedChapter);
            Assert.Equal(order, updatedChapter.Order);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public async Task UpdateChapter_ShouldThrowValidationException_WhenOrderIsInvalid(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = 1
            };
            await service.CreateChapterAsync(chapterDto);
            context.ChangeTracker.Clear();
            // Act
            chapterDto.Order = order;
            // Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.UpdateChapterAsync(chapterDto));
        }

        [Fact]
        public async Task UpdateChapter_ShouldThrowKeyNotFoundException_WhenChapterDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.NewGuid(),
                Order = 1
            };
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateChapterAsync(chapterDto));
        }

        [Fact]
        public async Task UpdateChapter_ShouldThrowArgumentNullException_WhenChapterIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateChapterAsync(null!));
        }

        [Fact]
        public async Task UpdateChapter_ShouldThrowArgumentNullException_WhenChapterIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new ChapterService(context);
            var chapterDto = new ChapterDTO
            {
                Id = Guid.Empty,
                Order = 1
            };
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateChapterAsync(chapterDto));
        }
    }
}
