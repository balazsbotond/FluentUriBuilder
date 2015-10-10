namespace FluentUriBuilder
{
    public static class BuildUri
    {
        public static FluentUriBuilder From(string baseUri)
        {
            return new FluentUriBuilder(baseUri);
        }

        public static FluentUriBuilder Create()
        {
            return new FluentUriBuilder(string.Empty);
        }
    }
}
