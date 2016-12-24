using FluentAssertions;
using System;
using Xunit;

namespace FluentUri.Test
{
    public class Path
    {
        [Fact]
        public void PathCannotBeNull()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Path(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void PathIsUsedIfSpecified()
        {
            var path = "/just/a/path.extension";
            var exampleUriWithoutPath = 
                "http://user:password@example.com:888?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutPath)
                .Path(path)
                .ToUri()
                .LocalPath
                .Should().Be(path);
        }

        [Fact]
        public void ExistingPathIsUpdated()
        {
            var path = "/just/a/path.extension";

            FluentUriBuilder.From(TestData.Uri)
                .Path(path)
                .ToUri()
                .LocalPath
                .Should().Be(path);
        }

        [Fact]
        public void ExistingPathIsDeletedIfEmptyFragmentSpecified()
        {
            FluentUriBuilder.From(TestData.Uri)
                .Path(string.Empty)
                .ToUri()
                .LocalPath
                .Should().Be("/");
        }

        [Fact]
        public void ExistingPathIsDeletedIfRemovePathCalled()
        {
            FluentUriBuilder.From(TestData.Uri)
                .RemovePath()
                .ToUri()
                .LocalPath
                .Should().Be("/");
        }
    }
}
