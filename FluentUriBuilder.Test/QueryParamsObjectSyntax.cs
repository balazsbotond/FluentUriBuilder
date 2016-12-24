using FluentAssertions;
using System;
using Xunit;

namespace FluentUri.Test
{
    public class QueryParamsObjectSyntax
    {
        [Fact]
        public void QueryParamsObjectCannotBeNull()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParams((object)null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void EmptyQueryParamsObjectAddsNoQueryParams()
        {
            var uri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new { })
                .ToString()
                .Should().Be(uri);
        }

        [Fact]
        public void EmptyQueryParamsObjectClearsExistingQueryParams()
        {
            var uri = "http://example.com?param=value";
            var expectedUri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new { })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void QueryParamUsedFromSinglePropertyObject()
        {
            var uri = "http://example.com";
            var expectedUri = "http://example.com/?param=value";

            FluentUriBuilder.From(uri)
                .QueryParams(new { param = "value" })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void MultipleQueryParamsAreUsedFromMultiPropertyObject()
        {
            var uri = "http://example.com";
            var expectedUri = 
                "http://example.com/?param=value&otherparam=othervalue";

            FluentUriBuilder.From(uri)
                .QueryParams(new { param = "value", otherparam = "othervalue" })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void ExistingQueryParamsAreDeletedWhenQueryParamsObjectIsUsed()
        {
            var uri = "http://example.com?oldparam=oldvalue";
            var expectedUri = "http://example.com/?param=value";

            FluentUriBuilder.From(uri)
                .QueryParams(new { param = "value" })
                .ToString()
                .Should().Be(expectedUri);
        }
    }
}
