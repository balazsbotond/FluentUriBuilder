using FluentAssertions;
using System;
using Xunit;

namespace FluentUri.Test
{
    public class Host
    {
        [Fact]
        public void HostCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Host(null))
                .ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Host(string.Empty))
                .ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create()
                .Invoking(b => b.Host(" "))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void HostIsUsedIfSpecified()
        {
            var host = "test.example.com";

            FluentUriBuilder.Create()
                .Host(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }

        [Fact]
        public void ExistingHostIsUpdated()
        {
            var host = "subdomain.domain.hu";

            FluentUriBuilder.From(TestData.Uri)
                .Host(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }
    }
}
