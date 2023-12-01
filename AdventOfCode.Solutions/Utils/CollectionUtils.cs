// Utility methods for working with collections.

/*
 * Class for providing some static utility methods for working with collections.
 */
namespace AdventOfCode.Solutions.Utils
{
    public static class CollectionUtils
    {
        // Take a collection of collections (XD) and return the intersection of all collections.
        public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> input)
            => input.Aggregate(input.First(), (intersector, next) => intersector.Intersect(next));

        // Make a new collection of items and join them into a single string, separated by the specified delimiter.
        public static string JoinAsStrings<T>(this IEnumerable<T> items, string delimiter = "")
            => string.Join(delimiter, items);

        // Given a collection of values, return all the possible permutations of those values.
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values) => values.Count() == 1
            ? new[] { values }
            : values.SelectMany(v =>
                Permutations(values.Where(x => x?.Equals(v) == false)), (v, p) => p.Prepend(v));
    }
}
