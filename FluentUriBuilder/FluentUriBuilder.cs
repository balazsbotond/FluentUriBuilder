using System;

namespace FluentUriBuilder
{
    public class FluentUriBuilder
    {
        private string baseUri;
        private string fragment;
        private string host;

        public FluentUriBuilder(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public FluentUriBuilder WithFragment(string fragment)
        {
            this.fragment = fragment;

            return this;
        }

        public FluentUriBuilder WithHost(string host)
        {
            host.ThrowIfNullOrWhiteSpace(nameof(host));

            this.host = host;

            return this;
        }

        public Uri ToUri()
        {
            var uriBuilder = string.IsNullOrWhiteSpace(baseUri)
                ? new UriBuilder()
                : new UriBuilder(baseUri);

            uriBuilder.Fragment = fragment;

            if (!string.IsNullOrWhiteSpace(host))
            {
                uriBuilder.Host = host;
            }

            return uriBuilder.Uri;
        }
    }
}
