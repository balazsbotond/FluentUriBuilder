using Xunit;
using FluentAssertions;
using System;

namespace FluentUriBuilder.Test
{
    public class BuildUriTest
    {
        private static readonly string fullTestUri = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

        #region General

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

        #endregion

        #region Fragment

        [Fact]
        public void FragmentCannotBeNull()
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

        #endregion

        #region Host

        [Fact]
        public void HostCannotBeNullOrWhiteSpace()
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

        #endregion

        #region Password

        [Fact]
        public void UserNamenCannotBeNullOrWhiteSpace()
        {
            BuildUri.Create().Invoking(b => b.WithCredentials(null, "password")).ShouldThrow<ArgumentException>();
            BuildUri.Create().Invoking(b => b.WithCredentials(string.Empty, "password")).ShouldThrow<ArgumentException>();
            BuildUri.Create().Invoking(b => b.WithCredentials(" ", "password")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void PasswordCannotBeNullOrWhiteSpace()
        {
            BuildUri.Create().Invoking(b => b.WithCredentials("user", null)).ShouldThrow<ArgumentException>();
            BuildUri.Create().Invoking(b => b.WithCredentials(string.Empty, "user")).ShouldThrow<ArgumentException>();
            BuildUri.Create().Invoking(b => b.WithCredentials(" ", "user")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CredentialsAreUsedIfSpecified()
        {
            var user = "user";
            var password = "password";
            var expectedUserInfo = "user:password";
            var exampleUriWithoutCredentials = "http://example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            BuildUri.From(exampleUriWithoutCredentials)
                .WithCredentials(user, password)
                .ToUri()
                .UserInfo
                .Should().Be(expectedUserInfo);
        }

        [Fact]
        public void ExistingCredentialsAreUpdated()
        {
            var user = "new-user";
            var password = "new-password";
            var expectedUserInfo = "new-user:new-password";

            BuildUri.From(fullTestUri)
                .WithCredentials(user, password)
                .ToUri()
                .UserInfo
                .Should().Be(expectedUserInfo);
        }

        //[Fact]
        //public void ExistingPasswordIsDeletedIfEmptyPasswordSpecified()
        //{
        //    BuildUri.From(fullTestUri)
        //        .WithUserInfo(string.Empty)
        //        .ToUri()
        //        .UserInfo
        //        .Should().Be("user");
        //}

        #endregion
    }
}
