using System.Globalization;

namespace FluentUri
{
    public static class FormatExtensions
    {
        public static string ToStringInvariantCulture(this object obj)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", obj);
        }
    }
}
