using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentUriBuilder
{
    public class FluentUriBuilder
    {
        private string baseUri;
        private string fragment;

        public FluentUriBuilder(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Uri ToUri()
        {
            var uriBuilder = new UriBuilder(this.baseUri);

            uriBuilder.Fragment = this.fragment;

            return uriBuilder.Uri;
        }

        public FluentUriBuilder WithFragment(string fragment)
        {
            this.fragment = fragment;

            return this;
        }
    }
}
