using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Utilities
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TKey> getKey)
        {
            return (from item in first
                    join otherItem in second on getKey(item) equals getKey(otherItem) into tempItems
                    from temp in tempItems.DefaultIfEmpty<T>()
                    where object.ReferenceEquals(null, temp) || temp.Equals(default(T))
                    select item);
        }
        public static int IndexOf<T>(this IEnumerable<T> enumerable, T element)
        {
            int num = 1;
            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;
            foreach (T local in enumerable)
            {
                if (comparer.Equals(local, element))
                {
                    return num;
                }
                num++;
            }
            return -1;
        }
    }
}
