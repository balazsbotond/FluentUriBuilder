using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FluentUri.Test
{
    public class QueryParamsDictionarySyntax
    {
        [Fact]
        public void QueryParamsDictionaryCannotBeNull()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParams(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void EmptyQueryParamsDictionaryAddsNoQueryParams()
        {
            var uri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object>())
                .ToString()
                .Should().Be(uri);
        }

        [Fact]
        public void EmptyQueryParamsDictionaryClearsExistingQueryParams()
        {
            var uri = "http://example.com?param=value";
            var expectedUri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object>())
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void QueryParamUsedFromSingleElementDictionary()
        {
            var uri = "http://example.com";
            var expectedUri = "http://example.com/?param=value";
            var queryParams =
                new Dictionary<string, string> { { "param", "value" } };

            FluentUriBuilder.From(uri)
                .QueryParams(queryParams)
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void MultipleQueryParamsAreUsedFromMultiElementDictionary()
        {
            var uri = "http://example.com";
            var expectedUri = 
                "http://example.com/?param=value&otherparam=othervalue";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object>
                {
                    { "param", "value" },
                    { "otherparam", "othervalue" },
                })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void ExistingQueryParamsAreDeletedWhenQueryParamsDictionaryIsUsed()
        {
            var uri = "http://example.com/?oldparam=oldvalue";
            var expectedUri = "http://example.com/?param=value";
            var queryParams =
                new Dictionary<string, string> { { "param", "value" } };

            FluentUriBuilder.From(uri)
                .QueryParams(queryParams)
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void KeyTypeCanBeString()
        {
            var uri = "http://example.com/?oldparam=oldvalue";
            var expectedUri = "http://example.com/?param=value";
            var queryParams = 
                new Dictionary<string, string> { { "param", "value" } };

            FluentUriBuilder.From(uri)
                .QueryParams(queryParams)
                .ToString()
                .Should().Be(expectedUri);
        }
    }
}
