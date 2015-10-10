using System;
using Xunit;
using FluentAssertions;
using FluentUriBuilder;

namespace FluentUriBuilder.Test
{
    public class BuildUriTest
    {
        [Fact]
        public void FromReturnsFluentUriBuilderInstance()
        {
            BuildUri.From(string.Empty).Should().NotBeNull();
        }

        [Fact]
        public void ToUriReturnsUriInstanceInitializedWithTheBaseUri()
        {
            var exampleUri = "http://example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";
            BuildUri.From(exampleUri).ToUri().Should().Be(exampleUri);
        }
   } 
}
