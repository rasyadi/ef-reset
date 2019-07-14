using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace EfReset.Tests
{
    public class MigrationTests
    {
        [Theory, AutoData]
        public void Remove_ProjectPathNotExist_ThrowsDirectoryNotFoundException(Migration sut)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> {
                { @"C:\temp", new MockFileData("Test")}
            });
            
            Action act = () => sut.Remove(@"C:\demo", "");

            act.Should().Throw<DirectoryNotFoundException>();
        }

        [Theory, AutoData]
        public void Remove_MigrationsFolderNotExist_ThrowsDirectoryNotFoundException(Migration sut)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> {
                { @"C:\temp", new MockFileData("Test")},
                { @"C:\temp\Migrate", new MockFileData("Test")},
            });

            Action act = () => sut.Remove(@"C:\temp", @"C:\temp\Migrations");

            act.Should().NotThrow<DirectoryNotFoundException>();
        }

        [Theory, AutoNSubstituteData]
        public void Remove_AllPathExist_PathDeleted(
            [Frozen] IDbInfo dbInfo,            
            Migration sut)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> {
                { @"C:\temp", new MockFileData("Test")},
                { @"C:\temp\Migrations", new MockFileData("Test")},
            });            
            _ = dbInfo.Parse("test").Returns(new DbInfo());

            Action act = () => sut.Remove(@"C:\temp", @"C:\temp\Migrations");

            fileSystem.Directory.Exists(@"C:\temp\Migrations").Should().BeFalse();
        }


    }
}
