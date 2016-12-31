using System;

namespace FluentUri
{
    internal static class Precondition
    {
        /// <summary>
        /// Documents that a given value can be null. This method does nothing,
        /// it is only for documentation purposes.
        /// </summary>
        /// <param name="obj"></param>
        public static void CanBeNull(object obj) { }

        public static void NotNull(object obj, string paramName)
        {
            if (obj == null) throw new ArgumentNullException(paramName);
        }

        public static void NotNullOrEmpty(string str, string paramName)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentOutOfRangeException(
                    paramName, "Should not be null or empty.");
        }

        public static void NotNullOrWhiteSpace(string str, string paramName)
        {
            if (StringHelper.IsNullOrWhiteSpace(str))
                throw new ArgumentOutOfRangeException(
                    paramName, "Should not be null or white space.");
        }

        public static void NotEmpty(Guid guid, string paramName)
        {
            if (guid == Guid.Empty)
                throw new ArgumentOutOfRangeException(
                    paramName, "Should not be empty.");
        }

        public static void Fulfills<T>(
            T obj, Predicate<T> predicate, string paramName, string message = "")
        {
            if (!predicate(obj))
                throw new ArgumentOutOfRangeException(message, paramName);
        }

        public static T IsOfType<T>(object obj, string paramName)
            where T : class
        {
            var t = obj as T;
            if (t == null)
                throw new ArgumentOutOfRangeException("Invalid type", paramName);

            return t;
        }
    }
}
