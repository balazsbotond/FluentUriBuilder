using System;
using System.Collections.Generic;
using System.Web;

namespace FluentUri
{
    public class FluentUriBuilder
    {
        //
        // If the following private fields are
        //
        // - `null`, that means they haven't been updated,
        // - empty, that means they have been removed,
        //
        // otherwise, they have been updated with a value.
        //
        private string baseUri;
        private string fragment;
        private string host;
        private string path;
        private string scheme;
        private bool removePort;
        private int? port;
        private UriCredentials credentials;
        private List<UriQueryParam> queryParams;

        private class UriCredentials
        {
            public UriCredentials(string user, string password)
            {
                User = user;
                Password = password;
            }

            public string User { get; set; }
            public string Password { get; set; }
        }

        private class UriQueryParam
        {
            public UriQueryParam(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; set; }
            public string Value { get; set; }
        }

        public static FluentUriBuilder From(string baseUri)
        {
            return new FluentUriBuilder(baseUri);
        }

        public static FluentUriBuilder Create()
        {
            return new FluentUriBuilder(string.Empty);
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
        public FluentUriBuilder Fragment(string fragment)
        {
            Precondition.NotNull(fragment, nameof(fragment));

            this.fragment = fragment;

            return this;
        }

        /// <summary>
        ///     Removes the fragment part of the URI.
        /// </summary>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder RemoveFragment()
        {
            // `string.Empty` indicates that the fragment has been updated but it is empty.
            fragment = string.Empty;

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
        public FluentUriBuilder Host(string host)
        {
            Precondition.NotNullOrWhiteSpace(host, nameof(host));

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
        public FluentUriBuilder Credentials(string user, string password)
        {
            Precondition.NotNullOrWhiteSpace(user, nameof(user));
            Precondition.NotNullOrWhiteSpace(password, nameof(password));

            credentials = new UriCredentials(user, password);

            return this;
        }

        /// <summary>
        ///     Removes the user name and password from the URI.
        /// </summary>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder RemoveCredentials()
        {
            credentials = new UriCredentials(string.Empty, string.Empty);

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
        public FluentUriBuilder Path(string path)
        {
            Precondition.NotNull(path, nameof(path));

            this.path = path;

            return this;
        }

        /// <summary>
        ///     Removes the local path from the URI.
        /// </summary>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder RemovePath()
        {
            this.path = string.Empty;

            return this;
        }

        /// <summary>
        ///     Sets or updates the port number in the URI.
        /// </summary>
        /// <param name="port">
        ///     An integer between -1 and 65535, inclusive. -1 indicates that the default
        ///     port number for the protocol is to be used.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If <see cref="port"/> is less than -1 or greater than 65535.
        /// </exception>
        public FluentUriBuilder Port(int port)
        {
            Precondition.Fulfills(port,
                p => -1 <= p && p <= 65535, nameof(port), 
                "Port should be between -1 and 65535, inclusive");

            this.port = port;

            return this;
        }

        /// <summary>
        ///     Updates the URI to use the default port for the protocol.
        /// </summary>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder DefaultPort()
        {
            port = -1;

            return this;
        }

        /// <summary>
        ///     Removes the port number from the URI.
        /// </summary>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder RemovePort()
        {
            removePort = true;

            return this;
        }

        /// <summary>
        ///     Sets or updates the protocol scheme of the URI.
        /// </summary>
        /// <param name="scheme">
        ///     The new value of the protocol scheme.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder Scheme(UriScheme scheme)
        {
            switch (scheme)
            {
                case UriScheme.File:
                    this.scheme = "file";
                    break;
                case UriScheme.Ftp:
                    this.scheme = "ftp";
                    break;
                case UriScheme.Gopher:
                    this.scheme = "gopher";
                    break;
                case UriScheme.Http:
                    this.scheme = "http";
                    break;
                case UriScheme.Https:
                    this.scheme = "https";
                    break;
                case UriScheme.Mailto:
                    this.scheme = "mailto";
                    break;
                case UriScheme.News:
                    this.scheme = "news";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scheme));
            }

            return this;
        }

        /// <summary>
        ///     Adds a query parameter to the URI.
        /// </summary>
        /// <param name="key">
        ///     The key (name) of the query parameter.
        /// </param>
        /// <param name="value">
        ///     The value of the query parameter.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     If either <see cref="key"/> or <see cref="value"/> is null or empty.
        /// </exception>
        public FluentUriBuilder QueryParam<T>(string key, T value)
        {
            Precondition.NotNullOrWhiteSpace(key, nameof(key));
            Precondition.NotNull(value, nameof(value));

            var valueStr = StringHelper.ToStringInvariantCulture(value);
            Precondition.NotNullOrWhiteSpace(valueStr, nameof(value));

            InitializeQueryParamsList();

            queryParams.Add(new UriQueryParam(key, valueStr));

            return this;
        }

        /// <summary>
        ///     Sets the query parameters of the URI.
        /// </summary>
        /// <param name="queryParams">
        ///     The query parameters to add to the URI.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <see cref="queryParams"/> is <c>null</c>;
        /// </exception>
        public FluentUriBuilder QueryParams<T>(IDictionary<string, T> queryParams)
        {
            Precondition.NotNull(queryParams, nameof(queryParams));

            InitializeQueryParamsList();

            foreach (var kvp in queryParams)
            {
                this.queryParams.Add(
                    new UriQueryParam(kvp.Key, StringHelper.ToStringInvariantCulture(kvp.Value)));
            }

            return this;
        }

        /// <summary>
        ///     Sets the query parameters of the URI.
        /// </summary>
        /// <param name="queryParams">
        ///     The object containing the query parameters to add to the URI. Property names
        ///     and their values are used as parameter keys and values.
        /// </param>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <see cref="queryParams"/> is <c>null</c>;
        /// </exception>
        public FluentUriBuilder QueryParams(object queryParams)
        {
            Precondition.NotNull(queryParams, nameof(queryParams));

            InitializeQueryParamsList();

            var properties = queryParams.GetType().GetProperties();

            foreach (var property in properties)
            {
                var key = property.Name;
                var value = property.GetValue(queryParams, index: null);

                this.queryParams.Add(
                    new UriQueryParam(key, StringHelper.ToStringInvariantCulture(value)));
            }

            return this;
        }

        /// <summary>
        ///     Removes all query parameters from the URI.
        /// </summary>
        /// <returns>
        ///     A <see cref="FluentUriBuilder"/> instance to allow chaining.
        /// </returns>
        public FluentUriBuilder RemoveQueryParams()
        {
            queryParams = new List<UriQueryParam>();

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
            var uriBuilder = StringHelper.IsNullOrWhiteSpace(baseUri)
                ? new UriBuilder()
                : new UriBuilder(baseUri);

            // By convention, if these private fields are null, they haven't been updated so
            // we have to leave the corresponding `UriBuilder` property alone.
            if (fragment != null) uriBuilder.Fragment = fragment;
            if (host != null) uriBuilder.Host = host;
            if (path != null) uriBuilder.Path = path;
            if (scheme != null) uriBuilder.Scheme = scheme;

            // Removing the port means setting the default port for the protocol, which
            // can be specified by passing -1 to UriBuilder.
            if (port != null) uriBuilder.Port = port.Value;
            else if (removePort) uriBuilder.Port = -1;

            if (credentials != null)
            {
                uriBuilder.UserName = credentials.User;
                uriBuilder.Password = credentials.Password;
            }

            if (queryParams != null)
            {
                var parameters = HttpUtility.ParseQueryString(string.Empty);

                foreach (var queryParam in queryParams)
                {
                    parameters.Add(queryParam.Key, queryParam.Value);
                }

                uriBuilder.Query = parameters.ToString();
            }

            return uriBuilder.Uri;
        }

        /// <summary>
        ///     Returns the URI built by this instance as a string.
        /// </summary>
        /// <returns>
        ///     The URI built by this instance.
        /// </returns>
        public override string ToString()
        {
            return ToUri().AbsoluteUri;
        }

        private void InitializeQueryParamsList()
        {
            if (queryParams == null)
            {
                // This is the first time we add query params. `null` indicated that query
                // params have not yet been updated, so we have to create an empty list.
                queryParams = new List<UriQueryParam>();
            }
        }
    }
}
