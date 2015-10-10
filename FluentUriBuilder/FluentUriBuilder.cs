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

        public FluentUriBuilder(string baseUri)
        {
            this.baseUri = baseUri;
        }

        public Uri ToUri()
        {
            return new Uri(this.baseUri);
        }
    }
}
