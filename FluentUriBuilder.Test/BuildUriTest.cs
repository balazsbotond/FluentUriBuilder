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
        public void UrlPartsNotUpdatedArePreserved()
        {
            BuildUri.From(fullTestUri).ToUri().AbsoluteUri.Should().Be(fullTestUri);
        }

        [Fact]
        public void WithFragmentDoesNotAcceptNull()
        {
            BuildUri.Create().Invoking(b => b.WithFragment(null)).ShouldThrow<ArgumentNullException>();
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
        public void ExistingFragmentIsDeletedIfEmptyFragmentSpecified()
        {
            BuildUri.From(fullTestUri)
                .WithFragment(string.Empty)
                .ToUri()
                .Fragment
                .Should().Be(string.Empty);
        }

        [Fact]
        public void WithHostDoesNotAcceptNullOrWhiteSpace()
        {
            BuildUri.Create().Invoking(b => b.WithHost(null)).ShouldThrow<ArgumentException>();
            BuildUri.Create().Invoking(b => b.WithHost(string.Empty)).ShouldThrow<ArgumentException>();
            BuildUri.Create().Invoking(b => b.WithHost(" ")).ShouldThrow<ArgumentException>();
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
