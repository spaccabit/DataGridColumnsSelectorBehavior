using System.Collections;

namespace TDG
{
    static class EnumerableExtensions
    {
        public static bool Any(this IEnumerable source)
        {
            var result = default(bool?);
            var enumerator = source?.GetEnumerator();
            result = enumerator?.MoveNext();
            return result.GetValueOrDefault();
        }

        public static object GetFirstOrDefault(this IEnumerable source, object defaultValue = null)
        {
            var enumerator = source?.GetEnumerator();
            var result = enumerator?.MoveNext();
            if (result == true)
            {
                return enumerator.Current;
            }
            return defaultValue;
        }
    }
}
