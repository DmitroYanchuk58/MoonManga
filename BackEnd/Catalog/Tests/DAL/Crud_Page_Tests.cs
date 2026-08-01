using Bogus;
using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.DAL
{
    public class Crud_Page_Tests : CrudTests
    {
        private readonly Faker _faker = new Faker();

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task CreateAsync_Success(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            var id = Guid.NewGuid();
            var imageData = _faker.Random.Bytes(1024); 

            var page = new Page()
            {
                Id = id,
                Order = order,
                Image = imageData
            };

            // Act
            await repository.CreateAsync(page);
            var entity = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(order, entity.Order);
            Assert.Equal(imageData, entity.Image);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task CreateAsync_IncorrectOrder(int order)
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            var page = new Page()
            {
                Id = Guid.NewGuid(),
                Order = order,
                Image = _faker.Random.Bytes(512)
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(page));
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            var id = Guid.NewGuid();
            var page = new Page()
            {
                Id = id,
                Order = 1,
                Image = _faker.Random.Bytes(128)
            };
            await repository.CreateAsync(page);

            // Act
            var result = await repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(page.Image, result.Image);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            int count = 10;
            for (int i = 0; i < count; i++)
            {
                await repository.CreateAsync(new Page
                {
                    Id = Guid.NewGuid(),
                    Order = i + 1,
                    Image = _faker.Random.Bytes(10)
                });
            }

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(count, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            var id = Guid.NewGuid();
            var page = new Page()
            {
                Id = id,
                Order = 1,
                Image = _faker.Random.Bytes(100)
            };
            await repository.CreateAsync(page);

            var newImage = _faker.Random.Bytes(200);
            page.Order = 2;
            page.Image = newImage;

            // Act
            await repository.UpdateAsync(page);
            var entity = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(2, entity.Order);
            Assert.Equal(newImage, entity.Image);
        }

        [Fact]
        public async Task Delete_Success()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            var id = Guid.NewGuid();
            var page = new Page()
            {
                Id = id,
                Order = 1,
                Image = _faker.Random.Bytes(10)
            };
            await repository.CreateAsync(page);
            context.ChangeTracker.Clear();

            // Act
            await repository.DeleteAsync(id);
            var entity = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            // Assert
            Assert.Null(entity);
        }

        [Fact]
        public async Task Create_NullImage_ShouldFail()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            var page = new Page()
            {
                Id = Guid.NewGuid(),
                Order = 1,
                Image = null!
            };

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(page));
        }
    }
}