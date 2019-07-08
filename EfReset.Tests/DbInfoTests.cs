using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Text;
using Xunit;

namespace EfReset.Tests
{
    public class DbInfoTests
    {
        [Theory, AutoData]
        public void Parse_EmptyDbInfoText_ThrowsArgumentNullException(DbInfo sut)
        {
            Action act = () => sut.Parse(string.Empty);

            act.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoData]
        public void Parse_InvalidDbInfoText_ReturnsDbInfo(string argument, DbInfo sut)
        {
            Action act = () => sut.Parse(argument);

            act.Should().Throw<ArgumentException>().WithMessage("Invalid argument");
        }

        [Theory, AutoData]
        public void Parse_ValidDbInfoText_ReturnsDbInfo(DbInfo expected, DbInfo sut)
        {

            var output = new StringBuilder();
            output.Append("Provider name").Append(": ").Append(expected.ProviderName).Append("\r\n");
            output.Append("Database name").Append(": ").Append(expected.DatabaseName).Append("\r\n");
            output.Append("Data source").Append(": ").Append(expected.DataSource).Append("\r\n");
            output.Append("options").Append(": ").Append(expected.Options).Append("\r\n");

            var actual = sut.Parse(output.ToString());

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
