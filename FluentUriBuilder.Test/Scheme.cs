using FluentAssertions;
using Xunit;

namespace FluentUri.Test
{
    public class Scheme
    {
        [Theory]
        [InlineData(UriScheme.File, "file")]
        [InlineData(UriScheme.Ftp, "ftp")]
        [InlineData(UriScheme.Gopher, "gopher")]
        [InlineData(UriScheme.Http, "http")]
        [InlineData(UriScheme.Https, "https")]
        [InlineData(UriScheme.Mailto, "mailto")]
        [InlineData(UriScheme.News, "news")]
        public void SchemeIsUsedIfSpecified(
            UriScheme scheme, string expectedScheme)
        {
            FluentUriBuilder.Create()
                .Scheme(scheme)
                .Host("example.com")
                .ToUri()
                .Scheme
                .Should().Be(expectedScheme);
        }

        [Theory]
        [InlineData(UriScheme.Ftp, "ftp")]
        [InlineData(UriScheme.Gopher, "gopher")]
        [InlineData(UriScheme.Http, "http")]
        [InlineData(UriScheme.Https, "https")]
        [InlineData(UriScheme.Mailto, "mailto")]
        [InlineData(UriScheme.News, "news")]
        public void ExistingSchemeIsUpdated(
            UriScheme scheme, string expectedScheme)
        {
            FluentUriBuilder.From(TestData.Uri)
                .Scheme(scheme)
                .ToUri()
                .Scheme
                .Should().Be(expectedScheme);
        }
    }
}
