using System;
using System.Collections.Generic;
using System.Linq;

namespace decomplex.Extensions
{
    /// <summary>
    /// Extension methods for IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Invokes action for each item in collection.
        /// </summary>
        /// <typeparam name="TItem">Type of items.</typeparam>
        /// <param name="items">Collection.</param>
        /// <param name="action">Action.</param>
        public static void ForEach<TItem>(this IEnumerable<TItem> items, Action<TItem> action)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Returns IEnumerable with only one item.
        /// </summary>
        /// <typeparam name="TItem">Type of item.</typeparam>
        /// <param name="item">Item to be put in collection.</param>
        /// <returns>Collection with the item.</returns>
        public static IEnumerable<TItem> Yield<TItem>(this TItem item)
        {
            yield return item;
        }

        /// <summary>
        /// Wraps generic IEnumerable<T> to IReadOnlyCollection<T>.
        /// </summary>
        /// <typeparam name="T">Type of elements.</typeparam>
        /// <param name="enumerable">Collection.</param>
        /// <returns>Instance of IReadOnlyCollection.</returns>
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList().AsReadOnly();
        }
    }
}