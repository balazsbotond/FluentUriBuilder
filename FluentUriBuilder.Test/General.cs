using FluentAssertions;
using Xunit;

namespace FluentUri.Test
{
    public class General
    {
        [Fact]
        public void FromReturnsFluentUriBuilderInstance()
        {
            FluentUriBuilder.From(string.Empty).Should().NotBeNull();
        }

        [Fact]
        public void CreateReturnsFluentUriBuilderInstance()
        {
            FluentUriBuilder.Create().Should().NotBeNull();
        }

        [Fact]
        public void UrlPartsNotUpdatedArePreserved()
        {
            FluentUriBuilder.From(TestData.Uri).ToUri().AbsoluteUri
                .Should().Be(TestData.Uri);
        }
    }
}
