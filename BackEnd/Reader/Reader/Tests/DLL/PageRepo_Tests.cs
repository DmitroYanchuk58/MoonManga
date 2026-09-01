using DatabaseAccessLayer.DatabaseContext;
using DatabaseAccessLayer.Repositories;
using DatabaseLogicLayer.Entities;
using DatabaseLogicLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DLL
{
    public class PageRepo_Tests : Repo_Tests
    {
        ICRUD_Repository<Page> _pageRepository;

        public PageRepo_Tests()
        {
            using var context = GetDbContext();
            var repository = new CRUD_Repository<Page>(context);
        }

        [Theory]
        [Category("Get")]
        [InlineData("1F75CA54-4807-42B1-921A-00401E89F040")]
        [InlineData("08205CCF-C862-4E27-B3FF-0187D1F19C6E")]
        [InlineData("DF83F12D-3980-4486-9FBC-032638281074")]
        public async Task GetById_NotNull_Success(Guid id)
        {
            var page = await _pageRepository.GetByIdAsync(id);
            Assert.NotNull(page);
        }
    }
}
