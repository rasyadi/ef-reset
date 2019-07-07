using EfReset.Tests.TestingHelpers;
using FluentAssertions;
using Xunit;

namespace EfReset.Tests
{
    public class DbContextTests
    {
        [Fact]
        public void GetInfo_ValidDbContext_ReturnsDbInfo()
        {
            string dbInfo = @"Provider name: Microsoft.EntityFrameworkCore.Sqlite
                            Database name: main
                            Data source: people.db
                            Options: None";

            var dbContext = new FakeDbContext(dbInfo);

            var actual = dbContext.GetInfo();

            actual.Should().BeEquivalentTo(dbInfo);            
        }
    }
}
