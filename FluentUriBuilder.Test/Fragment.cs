using FluentAssertions;
using System;
using Xunit;

namespace FluentUri.Test
{
    public class Fragment
    {
        [Fact]
        public void FragmentCannotBeNull()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.Fragment(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FragmentIsUsedIfSpecified()
        {
            var fragment = "fragment";
            var exampleUriWithoutFragment = 
                "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue";

            FluentUriBuilder.From(exampleUriWithoutFragment)
                .Fragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void ExistingFragmentIsUpdated()
        {
            var fragment = "test";

            FluentUriBuilder.From(TestData.Uri)
                .Fragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void ExistingFragmentIsDeletedIfEmptyFragmentSpecified()
        {
            FluentUriBuilder.From(TestData.Uri)
                .Fragment(string.Empty)
                .ToUri()
                .Fragment
                .Should().Be(string.Empty);
        }

        [Fact]
        public void ExistingFragmentIsDeletedIfRemoveFragmentIsCalled()
        {
            FluentUriBuilder.From(TestData.Uri)
                .RemoveFragment()
                .ToUri()
                .Fragment
                .Should().Be(string.Empty);
        }
    }
}
