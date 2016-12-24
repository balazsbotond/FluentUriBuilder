using FluentAssertions;
using System;
using Xunit;

namespace FluentUri.Test
{
    public class ToString
    {
        [Fact]
        public void ToStringReturnsTheSameStringAsUriAbsoluteUri()
        {
            var builder = FluentUriBuilder.From(TestData.Uri);
            var uri = new Uri(TestData.Uri);

            builder.ToString().Should().Be(uri.AbsoluteUri);
        }
    }
}
