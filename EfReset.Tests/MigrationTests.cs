using AutoFixture.Xunit2;
using FluentAssertions;
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
        public void Remove_DirectoryNotExist_ThrowsDirectoryNotFoundException(Migration sut)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> {
                { @"C:\temp", new MockFileData("Test")}
            });
            
            Action act = () => sut.Remove(@"C:\demo");

            act.Should().Throw<DirectoryNotFoundException>();
        }

        [Theory, AutoData]
        public void Remove_DirectoryExist_NotThrowsDirectoryNotFoundException(Migration sut)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> {
                { @"C:\temp", new MockFileData("Test")}
            });

            Action act = () => sut.Remove(@"C:\temp");

            act.Should().NotThrow<DirectoryNotFoundException>();
        }        
    }
}
