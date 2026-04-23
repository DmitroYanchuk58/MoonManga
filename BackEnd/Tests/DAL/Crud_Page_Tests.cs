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
    public class Crud_Page_Tests : CrudTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public async Task CreateAsync(int order)
        {
            // Arrange
            var faker = new Faker();
            byte[] randomImage = faker.Random.Bytes(1024);

            var id = Guid.NewGuid();

            var page = new Page()
            {
                Id = id,
                Order = order,
                Image = randomImage
            };
            var context = GetDbContext();

            var repository = new CrudRepository<Page>(context);

            // Act 

            var exception = await Record.ExceptionAsync(async () => await repository.CreateAsync(page));
            var entity = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);

            // Assert

            Assert.Null(exception);
            Assert.NotNull(entity);
            Assert.Equal(order, entity.Order);
            Assert.Equal(randomImage, entity.Image);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public async Task CreateAsync_WrongOrder(int order)
        {
            // Arrange
            var faker = new Faker();
            byte[] randomImage = faker.Random.Bytes(1024);

            var id = Guid.NewGuid();

            var page = new Page()
            {
                Id = id,
                Order = order,
                Image = randomImage
            };
            var context = GetDbContext();

            var repository = new CrudRepository<Page>(context);

            // Act && Assert

            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(page));
        }

        [Fact]
        public async Task CreateAsync_NullImage_ShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var page = new Page()
            {
                Id = id,
                Order = 1,
                Image = null
            };
            var context = GetDbContext();
            var repository = new CrudRepository<Page>(context);
            // Act && Assert
            await Assert.ThrowsAsync<DbUpdateException>(async () => await repository.CreateAsync(page));
        }
    }
}
