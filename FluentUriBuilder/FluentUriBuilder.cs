using System;

namespace FluentUriBuilder
{
    public class FluentUriBuilder
    {
        private string baseUri;
        private string fragment;
        private string host;
        private string password;
        private string path;
        private Credentials credentials;

        private class Credentials
        {
            public Credentials(string user, string password)
            {
                User = user;
                Password = password;
            }

            public string User { get; set; }
            public string Password { get; set; }
        }

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
        ///     Sets or updates the user credentials (user name and password) in the URI.
        /// </summary>
        /// <param name="user">
        ///     The new value of the user.
        /// </param>
        /// <param name="password">
        ///     The new value of the password.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If either of the arguments specified is <c>null</c>.
        /// </exception>
        public FluentUriBuilder WithCredentials(string user, string password)
        {
            user.ThrowIfNullOrWhiteSpace(nameof(user));
            password.ThrowIfNullOrWhiteSpace(nameof(password));

            credentials = new Credentials(user, password);

            return this;
        }

        /// <summary>
        ///     Sets or updates the local path in the URI.
        /// </summary>
        /// <param name="path">
        ///     The new value of the local path.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder WithPath(string path)
        {
            path.ThrowIfNull(nameof(path));

            this.path = path;

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
            if (path != null) uriBuilder.Path = path;

            if (credentials != null)
            {
                uriBuilder.UserName = credentials.User;
                uriBuilder.Password = credentials.Password;
            }

            return uriBuilder.Uri;
        }
    }
}
