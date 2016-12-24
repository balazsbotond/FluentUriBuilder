using FluentAssertions;
using Xunit;

namespace FluentUri.Test
{
    public class RemoveQueryParams
    {
        [Fact]
        public void RemoveQueryParamsClearsExistingQueryParams()
        {
            var uri = "http://example.com?param=value&otherparam=othervalue";
            var expectedUri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .RemoveQueryParams()
                .ToString()
                .Should().Be(expectedUri);
        }
    }
}
