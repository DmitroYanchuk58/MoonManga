using Bogus;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL
{
    public class Crud_Chapter_Tests : CrudTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task CreateAsync_Success(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            var chapter = new Chapter()
            {
                Id = id,
                Order = order,
            };

            //Act
            await repository.CreateAsync(chapter);
            var entity = await context.Chapters.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(order, entity.Order);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public async Task CreateAsync_IncorrectOrder(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            var chapter = new Chapter()
            {
                Id = id,
                Order = order,
            };
            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(chapter));
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            var chapter = new Chapter()
            {
                Id = id,
                Order = 5,
            };
            await repository.CreateAsync(chapter);
            // Act
            var result = await repository.GetByIdAsync(id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(chapter.Order, result.Order);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            for (int i = 1; i <= 50; i++)
            {
                var chapter = new Chapter()
                {
                    Id = Guid.NewGuid(),
                    Order = i + 1,
                };
                await repository.CreateAsync(chapter);
            }
            // Act
            var result = await repository.GetAllAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(50, result.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task UpdateAsync_Success(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            var chapter = new Chapter()
            {
                Id = id,
                Order = 1,
            };

            //Act
            await repository.CreateAsync(chapter);

            chapter.Order = order;
            await repository.UpdateAsync(chapter);

            var entity = await context.Chapters.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(order, entity.Order);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public async Task Update_IncorrectOrder(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            var chapter = new Chapter()
            {
                Id = id,
                Order = 1,
            };
            //Act
            await repository.CreateAsync(chapter);

            chapter.Order = order;
            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.UpdateAsync(chapter));
        }

        [Fact]
        public async Task Delete_Success()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            var chapter = new Chapter()
            {
                Id = id,
                Order = 1,
            };
            await repository.CreateAsync(chapter);
            // Act
            await repository.DeleteAsync(id);
            var entity = await context.Chapters.FirstOrDefaultAsync(x => x.Id == id);
            var exception = await Record.ExceptionAsync(async () => await repository.DeleteAsync(id));
            // Assert
            Assert.Null(exception);
            Assert.Null(entity);
        }

        [Fact]
        public async Task Delete_NonExisting()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Chapter>(context);
            var id = Guid.NewGuid();
            // Act
            await repository.DeleteAsync(id);
            var entity = await context.Chapters.FirstOrDefaultAsync(x => x.Id == id);
            var exception = await Record.ExceptionAsync(async () => await repository.DeleteAsync(id));
            // Assert
            Assert.Null(exception);
            Assert.Null(entity);
        }
    }
}
