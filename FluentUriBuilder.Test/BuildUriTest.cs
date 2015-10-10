using Xunit;
using FluentAssertions;
using System;

namespace FluentUriBuilder.Test
{
    public class BuildUriTest
    {
        private static readonly string fullTestUri = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

        [Fact]
        public void FromReturnsFluentUriBuilderInstance()
        {
            BuildUri.From(string.Empty).Should().NotBeNull();
        }

        [Fact]
        public void CreateReturnsFluentUriBuilderInstance()
        {
            BuildUri.Create().Should().NotBeNull();
        }

        [Fact]
        public void ToUriReturnsUriInstanceInitializedWithTheBaseUri()
        {
            BuildUri.From(fullTestUri).ToUri().Should().Be(fullTestUri);
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

            BuildUri.From(fullTestUri)
                .WithFragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void WithHostDoesNotAcceptNullOrWhiteSpace()
        {
            Assert.Throws<ArgumentException>(() =>
                BuildUri.Create().WithHost(null)
            );
            Assert.Throws<ArgumentException>(() =>
                BuildUri.Create().WithHost(string.Empty)
            );
            Assert.Throws<ArgumentException>(() =>
                BuildUri.Create().WithHost(" ")
            );
        }

        [Fact]
        public void HostIsUsedIfSpecified()
        {
            var host = "test.example.com";

            BuildUri.Create()
                .WithHost(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }

        [Fact]
        public void ExistingHostIsUpdated()
        {
            var host = "subdomain.domain.hu";

            BuildUri.From(fullTestUri)
                .WithHost(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }
   } 
}
