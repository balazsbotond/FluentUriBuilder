using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentUriBuilder
{
    public static class BuildUri
    {
        public static FluentUriBuilder From(string baseUri)
        {
            return new FluentUriBuilder(baseUri);
        }
    }
}
