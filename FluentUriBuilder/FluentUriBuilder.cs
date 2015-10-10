using System;

namespace FluentUriBuilder
{
    public class FluentUriBuilder
    {
        private string baseUri;
        private string fragment;
        private string host;

        internal FluentUriBuilder(string baseUri)
        {
            this.baseUri = baseUri;
        }

        /// <summary>
        ///     Sets or updates the fragment part of the URI.
        /// </summary>
        /// <param name="fragment">
        ///     The new value of the URI fragment. If empty, the base URI fragment is deleted.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If the fragment specified is <c>null</c>.
        /// </exception>
        public FluentUriBuilder WithFragment(string fragment)
        {
            fragment.ThrowIfNull(nameof(fragment));

            this.fragment = fragment;

            return this;
        }

        /// <summary>
        ///     Sets or updates the hostname of the URI.
        /// </summary>
        /// <param name="host">
        ///     The new value of the hostname. Cannot be null or white space.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     If the hostname specified is a <c>null</c> or empty <see cref="string"/>,
        ///     or it only contains white space.
        /// </exception>
        public FluentUriBuilder WithHost(string host)
        {
            host.ThrowIfNullOrWhiteSpace(nameof(host));

            this.host = host;

            return this;
        }

        /// <summary>
        ///     Creates a new <see cref="Uri"/> instance from the values specified.
        /// </summary>
        /// <returns>
        ///     The <see cref="Uri"/>.
        /// </returns>
        public Uri ToUri()
        {
            // The parameterized `UriBuilder` constructor throws an exception if its parameter is
            // null or white space, so we need need to check this here.
            var uriBuilder = string.IsNullOrWhiteSpace(baseUri)
                ? new UriBuilder()
                : new UriBuilder(baseUri);

            if (fragment != null) uriBuilder.Fragment = fragment;
            if (host != null) uriBuilder.Host = host;

            return uriBuilder.Uri;
        }
    }
}
