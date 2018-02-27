using System.Collections.Generic;

namespace Coinigy.API
{
    internal static class Extensions
    {
        public static IEnumerable<KeyValuePair<T, U>> ToList<T, U>(this KeyValuePair<T, U> p) => new KeyValuePair<T, U>[] { p };
    }
}