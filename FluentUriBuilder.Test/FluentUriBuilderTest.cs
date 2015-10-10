using Xunit;
using FluentAssertions;
using System;

namespace FluentUriBuilder.Test
{
    public class FluentUriBuilderTest
    {
        private static readonly string fullTestUri = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

        #region General

        [Fact]
        public void FromReturnsFluentUriBuilderInstance()
        {
            FluentUriBuilder.From(string.Empty).Should().NotBeNull();
        }

        [Fact]
        public void CreateReturnsFluentUriBuilderInstance()
        {
            FluentUriBuilder.Create().Should().NotBeNull();
        }

        [Fact]
        public void UrlPartsNotUpdatedArePreserved()
        {
            FluentUriBuilder.From(fullTestUri).ToUri().AbsoluteUri.Should().Be(fullTestUri);
        }

        #endregion

        #region Fragment

        [Fact]
        public void FragmentCannotBeNull()
        {
            FluentUriBuilder.Create().Invoking(b => b.WithFragment(null)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FragmentIsUsedIfSpecified()
        {
            var fragment = "fragment";
            var exampleUriWithoutFragment = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue";

            FluentUriBuilder.From(exampleUriWithoutFragment)
                .WithFragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void ExistingFragmentIsUpdated()
        {
            var fragment = "test";

            FluentUriBuilder.From(fullTestUri)
                .WithFragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void ExistingFragmentIsDeletedIfEmptyFragmentSpecified()
        {
            FluentUriBuilder.From(fullTestUri)
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
            FluentUriBuilder.Create().Invoking(b => b.WithHost(null)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.WithHost(string.Empty)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.WithHost(" ")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void HostIsUsedIfSpecified()
        {
            var host = "test.example.com";

            FluentUriBuilder.Create()
                .WithHost(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }

        [Fact]
        public void ExistingHostIsUpdated()
        {
            var host = "subdomain.domain.hu";

            FluentUriBuilder.From(fullTestUri)
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
            FluentUriBuilder.Create().Invoking(b => b.WithCredentials(null, "password")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.WithCredentials(string.Empty, "password")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.WithCredentials(" ", "password")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void PasswordCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create().Invoking(b => b.WithCredentials("user", null)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.WithCredentials(string.Empty, "user")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.WithCredentials(" ", "user")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CredentialsAreUsedIfSpecified()
        {
            var user = "user";
            var password = "password";
            var expectedUserInfo = "user:password";
            var exampleUriWithoutCredentials = "http://example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutCredentials)
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

            FluentUriBuilder.From(fullTestUri)
                .WithCredentials(user, password)
                .ToUri()
                .UserInfo
                .Should().Be(expectedUserInfo);
        }

        #endregion

        #region Path

        [Fact]
        public void PathCannotBeNull()
        {
            FluentUriBuilder.Create().Invoking(b => b.WithPath(null)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void PathIsUsedIfSpecified()
        {
            var path = "/just/a/path.extension";
            var exampleUriWithoutPath = "http://user:password@example.com:888?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutPath)
                .WithPath(path)
                .ToUri()
                .LocalPath
                .Should().Be(path);
        }

        [Fact]
        public void ExistingPathIsUpdated()
        {
            var path = "/just/a/path.extension";

            FluentUriBuilder.From(fullTestUri)
                .WithPath(path)
                .ToUri()
                .LocalPath
                .Should().Be(path);
        }

        [Fact]
        public void ExistingPathIsDeletedIfEmptyFragmentSpecified()
        {
            FluentUriBuilder.From(fullTestUri)
                .WithPath(string.Empty)
                .ToUri()
                .LocalPath
                .Should().Be("/");
        }

        #endregion

        #region Port

        [Fact]
        public void PortCannotBeLessThanMinus1()
        {
            FluentUriBuilder.Create().Invoking(b => b.WithPort(-2)).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCannotBeGreaterThan65535()
        {
            FluentUriBuilder.Create().Invoking(b => b.WithPort(65536)).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortIsUsedIfSpecified()
        {
            var port = 1337;
            var exampleUriWithoutPort = "http://user:password@example.com/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutPort)
                .WithPort(port)
                .ToUri()
                .Port
                .Should().Be(port);
        }

        [Fact]
        public void ExistingPortIsUpdated()
        {
            var port = 1337;

            FluentUriBuilder.From(fullTestUri)
                .WithPort(port)
                .ToUri()
                .Port
                .Should().Be(port);
        }

        [Fact]
        public void ExistingPortIsDeletedIfProtocolDefaultPortSpecified()
        {
            FluentUriBuilder.From(fullTestUri)
                .WithPort(-1)
                .ToUri()
                .Port
                .Should().Be(80);
        }

        #endregion

        #region Scheme

        [Theory]
        [InlineData(UriScheme.File, "file")]
        [InlineData(UriScheme.Ftp, "ftp")]
        [InlineData(UriScheme.Gopher, "gopher")]
        [InlineData(UriScheme.Http, "http")]
        [InlineData(UriScheme.Https, "https")]
        [InlineData(UriScheme.Mailto, "mailto")]
        [InlineData(UriScheme.News, "news")]
        public void SchemeIsUsedIfSpecified(UriScheme scheme, string expectedScheme)
        {
            FluentUriBuilder.Create()
                .WithScheme(scheme)
                .WithHost("example.com")
                .ToUri()
                .Scheme
                .Should().Be(expectedScheme);
        }

        [Theory]
        [InlineData(UriScheme.Ftp, "ftp")]
        [InlineData(UriScheme.Gopher, "gopher")]
        [InlineData(UriScheme.Http, "http")]
        [InlineData(UriScheme.Https, "https")]
        [InlineData(UriScheme.Mailto, "mailto")]
        [InlineData(UriScheme.News, "news")]
        public void ExistingSchemeIsUpdated(UriScheme scheme, string expectedScheme)
        {
            FluentUriBuilder.From(fullTestUri)
                .WithScheme(scheme)
                .ToUri()
                .Scheme
                .Should().Be(expectedScheme);
        }

        #endregion
    }
}
