using System.Collections.Generic;
using System.Linq;

namespace Automaton.Model
{
    internal static class EnumerableExtensions
    {
        public static bool ContainsAny<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }
    }
}