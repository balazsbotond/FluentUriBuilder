using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;

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
            FluentUriBuilder.Create().Invoking(b => b.Fragment(null)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void FragmentIsUsedIfSpecified()
        {
            var fragment = "fragment";
            var exampleUriWithoutFragment = "http://user:password@example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue";

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

            FluentUriBuilder.From(fullTestUri)
                .Fragment(fragment)
                .ToUri()
                .Fragment
                .Should().Be("#" + fragment);
        }

        [Fact]
        public void ExistingFragmentIsDeletedIfEmptyFragmentSpecified()
        {
            FluentUriBuilder.From(fullTestUri)
                .Fragment(string.Empty)
                .ToUri()
                .Fragment
                .Should().Be(string.Empty);
        }

        [Fact]
        public void ExistingFragmentIsDeletedIfRemoveFragmentIsCalled()
        {
            FluentUriBuilder.From(fullTestUri)
                .RemoveFragment()
                .ToUri()
                .Fragment
                .Should().Be(string.Empty);
        }

        #endregion

        #region Host

        [Fact]
        public void HostCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create().Invoking(b => b.Host(null)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.Host(string.Empty)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.Host(" ")).ShouldThrow<ArgumentException>();
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

            FluentUriBuilder.From(fullTestUri)
                .Host(host)
                .ToUri()
                .Host
                .Should().Be(host);
        }

        #endregion

        #region Password

        [Fact]
        public void UserNameCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create().Invoking(b => b.Credentials(null, "password")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.Credentials(string.Empty, "password")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.Credentials(" ", "password")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void PasswordCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create().Invoking(b => b.Credentials("user", null)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.Credentials(string.Empty, "user")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.Credentials(" ", "user")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CredentialsAreUsedIfSpecified()
        {
            var user = "user";
            var password = "password";
            var expectedUserInfo = "user:password";
            var exampleUriWithoutCredentials = "http://example.com:888/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutCredentials)
                .Credentials(user, password)
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
                .Credentials(user, password)
                .ToUri()
                .UserInfo
                .Should().Be(expectedUserInfo);
        }

        [Fact]
        public void ExistingCredentialsAreDeletedIfRemoveCredentialsIsCalled()
        {
            FluentUriBuilder.From(fullTestUri)
                .RemoveCredentials()
                .ToUri()
                .UserInfo
                .Should().Be(string.Empty);
        }

        #endregion

        #region Path

        [Fact]
        public void PathCannotBeNull()
        {
            FluentUriBuilder.Create().Invoking(b => b.Path(null)).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void PathIsUsedIfSpecified()
        {
            var path = "/just/a/path.extension";
            var exampleUriWithoutPath = "http://user:password@example.com:888?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

            FluentUriBuilder.From(exampleUriWithoutPath)
                .Path(path)
                .ToUri()
                .LocalPath
                .Should().Be(path);
        }

        [Fact]
        public void ExistingPathIsUpdated()
        {
            var path = "/just/a/path.extension";

            FluentUriBuilder.From(fullTestUri)
                .Path(path)
                .ToUri()
                .LocalPath
                .Should().Be(path);
        }

        [Fact]
        public void ExistingPathIsDeletedIfEmptyFragmentSpecified()
        {
            FluentUriBuilder.From(fullTestUri)
                .Path(string.Empty)
                .ToUri()
                .LocalPath
                .Should().Be("/");
        }

        [Fact]
        public void ExistingPathIsDeletedIfRemovePathCalled()
        {
            FluentUriBuilder.From(fullTestUri)
                .RemovePath()
                .ToUri()
                .LocalPath
                .Should().Be("/");
        }

        #endregion

        #region Port

        [Fact]
        public void PortCannotBeLessThanMinus1()
        {
            FluentUriBuilder.Create().Invoking(b => b.Port(-2)).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCanBeMinus1()
        {
            FluentUriBuilder.Create().Invoking(b => b.Port(-1)).ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCannotBeGreaterThan65535()
        {
            FluentUriBuilder.Create().Invoking(b => b.Port(65536)).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortCanBe65535()
        {
            FluentUriBuilder.Create().Invoking(b => b.Port(65535)).ShouldNotThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void PortIsUsedIfSpecified()
        {
            var port = 1337;
            var exampleUriWithoutPort = "http://user:password@example.com/path/to?somekey=some%2bvalue&otherkey=some%2bvalue#fragment";

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

            FluentUriBuilder.From(fullTestUri)
                .Port(port)
                .ToUri()
                .Port
                .Should().Be(port);
        }

        [Fact]
        public void ProtocolDefaultPortIsUsedIfPortIsMinus1()
        {
            FluentUriBuilder.From(fullTestUri)
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
                .Scheme(scheme)
                .Host("example.com")
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
                .Scheme(scheme)
                .ToUri()
                .Scheme
                .Should().Be(expectedScheme);
        }

        #endregion

        #region ToString

        [Fact]
        public void ToStringReturnsTheSameStringAsUriAbsoluteUri()
        {
            var builder = FluentUriBuilder.From(fullTestUri);
            var uri = new Uri(fullTestUri);

            builder.ToString().Should().Be(uri.AbsoluteUri);
        }

        #endregion

        #region QueryParam

        [Fact]
        public void QueryParamKeyCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create().Invoking(b => b.QueryParam(null, "a")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.QueryParam(string.Empty, "a")).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.QueryParam(" ", "a")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void QueryParamValueCannotBeNullOrWhiteSpace()
        {
            FluentUriBuilder.Create().Invoking(b => b.QueryParam("a", null)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.QueryParam("a", string.Empty)).ShouldThrow<ArgumentException>();
            FluentUriBuilder.Create().Invoking(b => b.QueryParam("a", " ")).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void SingleQueryParamIsUsed()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", "testvalue")
                .ToUri()
                .Query
                .Should().Be("?testkey=testvalue");
        }

        [Fact]
        public void MultipleQueryParamsAreUsed()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", "testvalue")
                .QueryParam("anotherkey", "anothervalue")
                .ToUri()
                .Query
                .Should().Be("?testkey=testvalue&anotherkey=anothervalue");
        }

        [Fact]
        public void QueryParamsAreUrlEncoded()
        {
            var uri = "http://example.com";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", ":?&=#/@")
                .ToUri()
                .Query
                .Should().Be("?testkey=%3a%3f%26%3d%23%2f%40");
        }

        [Fact]
        public void AddingAQueryParamClearsExistingQueryParams()
        {
            var uri = "http://example.com?oldparam=oldvalue";

            FluentUriBuilder.From(uri)
                .QueryParam("testkey", "testvalue")
                .ToUri()
                .Query
                .Should().Be("?testkey=testvalue");
        }

        #endregion

        #region QueryParams Dictionary syntax

        [Fact]
        public void QueryParamsDictionaryCannotBeNull()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParams(null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void EmptyQueryParamsDictionaryAddsNoQueryParams()
        {
            var uri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object>())
                .ToString()
                .Should().Be(uri);
        }

        [Fact]
        public void EmptyQueryParamsDictionaryClearsExistingQueryParams()
        {
            var uri = "http://example.com?param=value";
            var expectedUri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object>())
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void QueryParamUsedFromSingleElementDictionary()
        {
            var uri = "http://example.com";
            var expectedUri = "http://example.com/?param=value";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object> { { "param", "value" } })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void MultipleQueryParamsAreUsedFromMultiElementDictionary()
        {
            var uri = "http://example.com";
            var expectedUri = "http://example.com/?param=value&otherparam=othervalue";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object>
                {
                    { "param", "value" },
                    { "otherparam", "othervalue" },
                })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void ExistingQueryParamsAreDeletedWhenQueryParamsDictionaryIsUsed()
        {
            var uri = "http://example.com/?oldparam=oldvalue";
            var expectedUri = "http://example.com/?param=value";

            FluentUriBuilder.From(uri)
                .QueryParams(new Dictionary<string, object> { { "param", "value" } })
                .ToString()
                .Should().Be(expectedUri);
        }

        #endregion

        #region QueryParams object syntax

        [Fact]
        public void QueryParamsObjectCannotBeNull()
        {
            FluentUriBuilder.Create()
                .Invoking(b => b.QueryParams((object)null))
                .ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void EmptyQueryParamsObjectAddsNoQueryParams()
        {
            var uri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new { })
                .ToString()
                .Should().Be(uri);
        }

        [Fact]
        public void EmptyQueryParamsObjectClearsExistingQueryParams()
        {
            var uri = "http://example.com?param=value";
            var expectedUri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .QueryParams(new { })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void QueryParamUsedFromSinglePropertyObject()
        {
            var uri = "http://example.com";
            var expectedUri = "http://example.com/?param=value";

            FluentUriBuilder.From(uri)
                .QueryParams(new { param = "value" })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void MultipleQueryParamsAreUsedFromMultiPropertyObject()
        {
            var uri = "http://example.com";
            var expectedUri = "http://example.com/?param=value&otherparam=othervalue";

            FluentUriBuilder.From(uri)
                .QueryParams(new { param = "value", otherparam = "othervalue" })
                .ToString()
                .Should().Be(expectedUri);
        }

        [Fact]
        public void ExistingQueryParamsAreDeletedWhenQueryParamsObjectIsUsed()
        {
            var uri = "http://example.com?oldparam=oldvalue";
            var expectedUri = "http://example.com/?param=value";

            FluentUriBuilder.From(uri)
                .QueryParams(new { param = "value" })
                .ToString()
                .Should().Be(expectedUri);
        }

        #endregion

        #region RemoveQueryParams

        [Fact]
        public void RemoveQueryParamsClearsExistingQueryParams()
        {
            var uri = "http://example.com?param=value&otherparam=othervalue";
            var expectedUri = "http://example.com/";

            FluentUriBuilder.From(uri)
                .RemoveQueryParams()
                .ToString()
                .Should().Be(expectedUri);
        }

        #endregion
    }
}
