using DatabaseAccessLayer.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DLL
{
    public abstract class Repo_Tests
    {
        protected ReaderDBContext GetDbContext()
        {
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("Filename=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ReaderDBContext>()
                .UseSqlite(connection)
                .Options;
            var context = new ReaderDBContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
