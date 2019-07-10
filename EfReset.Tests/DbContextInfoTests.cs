using EfReset.Tests.TestingHelpers;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace EfReset.Tests
{
    public class DbContextInfoTests
    {
        [Theory, AutoData]
        public void GetInfo_ValidDbContext_ReturnsDbInfo(string dbInfo)
        {            
            var dbContext = new FakeDbContextInfo(dbInfo);

            var actual = dbContext.GetInfo("");

            actual.Should().BeEquivalentTo(dbInfo);            
        }
    }
}
