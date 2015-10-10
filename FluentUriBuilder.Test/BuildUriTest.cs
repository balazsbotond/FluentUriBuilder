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
   } 
}
