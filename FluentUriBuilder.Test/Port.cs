using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentUri.Test
{
    public class Port
    {
        [Fact]
        public void PortCannotBeLessThanMinus1()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Port(-2))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCanBeMinus1()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Port(-1))
                .ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCannotBeGreaterThan65535()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Port(65536))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCanBe65535()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Port(65535))
                .ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortIsUsedIfSpecified()
        {
            var port = 1337;
            var exampleUriWithoutPort = 
                "http://user:password@example.com/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutPort)
                .Port(port)
                .ToUri()
                .Port
                .Should().Be(port);
        }

        [Fact]
        public void ExistingPortIsUpdated()
        {
            var port = 1337;

            FluentUriBuilder.From(TestData.Uri)
                .Port(port)
                .ToUri()
                .Port
                .Should().Be(port);
        }

        [Fact]
        public void ProtocolDefaultPortIsUsedIfPortIsMinus1()
        {
            FluentUriBuilder.From(TestData.Uri)
                .Port(-1)
                .ToUri()
                .Port
                .Should().Be(80);
        }

        [Fact]
        public void ExistingPortIsDeletedIfProtocolDefaultPortSpecified()
        {
            FluentUriBuilder.From("ftp://example.com:9999")
                .DefaultPort()
                .ToUri()
                .Port
                .Should().Be(21);
        }

        [Fact]
        public void ExistingPortIsDeletedIfRemovePortCalled()
        {
            FluentUriBuilder.From("ftp://example.com:9999")
                .RemovePort()
                .ToUri()
                .Port
                .Should().Be(21);
        }
    }
}
