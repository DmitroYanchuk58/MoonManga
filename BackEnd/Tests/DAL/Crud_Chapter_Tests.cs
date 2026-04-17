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
        [InlineData("Valid Title", "Manga", true)]
        [InlineData(null, "Manga", false)]
        [InlineData("Valid Title", null, false)]
        [InlineData("", "Manga", false)]
        [InlineData("Valid Title", "", false)]
        [InlineData("   ", "Manga", false)]
        [InlineData("Valid Title", "   ", false)]
        public async Task CreateAsync_Success(string? title, string? type, bool shouldSucceed)
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
    }
}
