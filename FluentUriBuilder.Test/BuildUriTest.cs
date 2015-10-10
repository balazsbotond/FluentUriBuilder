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
            var exampleUri = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";
            BuildUri.From(exampleUri).ToUri().Should().Be(exampleUri);
        }

        [Fact]
        public void FragmentIsUsedIfSpecified()
        {
            var fragment = "fragment";
            var exampleUriWithoutFragment = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue";

            BuildUri.From(exampleUriWithoutFragment)
                .WithFragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void ExistingFragmentIsUpdated()
        {
            var fragment = "test";
            var exampleUriWithFragment = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            BuildUri.From(exampleUriWithFragment)
                .WithFragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }
   } 
}
