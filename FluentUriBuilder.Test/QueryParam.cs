using FluentAssertions;
using System;
using Xunit;

namespace FluentUri.Test
{
    public class QueryParam
    {
        [Fact]
        public void QueryParamKeyCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParam(null, "a"))
                .ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParam(string.Empty, "a"))
                .ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParam(" ", "a"))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void QueryParamValueCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParam("a", (string)null))
                .ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParam("a", string.Empty))
                .ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParam("a", " "))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void SingleQueryParamIsUsed()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", "testvalue")
                .ToUri()
                .Query
                .Should().Be("?testkey=testvalue");
        }

        [Fact]
        public void MultipleQueryParamsAreUsed()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", "testvalue")
                .QueryParam("anotherkey", "anothervalue")
                .ToUri()
                .Query
                .Should().Be("?testkey=testvalue&anotherkey=anothervalue");
        }

        [Fact]
        public void QueryParamsAreUrlEncoded()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", ":?&=#/@")
                .ToUri()
                .Query
                .Should().Be("?testkey=%3a%3f%26%3d%23%2f%40");
        }

        [Fact]
        public void AddingAQueryParamClearsExistingQueryParams()
        {
            var uri = "http://example.com?oldparam=oldvalue";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", "testvalue")
                .ToUri()
                .Query
                .Should().Be("?testkey=testvalue");
        }

        [Fact]
        public void QueryParamValuesCanBeOfAnyType()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", 1)
                .QueryParam("anotherkey", 2.5)
                .ToUri()
                .Query
                .Should().Be("?testkey=1&anotherkey=2%2c5");
        }
    }
}
