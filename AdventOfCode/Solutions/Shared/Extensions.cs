using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Extensions
    {
        internal static IEnumerable<IList<T>> GetPermutations<T>(this IList<T> sequence, int? count = null)
        {
            count ??= sequence.Count;

            if (count == 1) yield return sequence;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in GetPermutations(sequence, count - 1))
                        yield return perm;
                    T tmp = sequence[count.Value - 1];
                    sequence.RemoveAt(count.Value - 1);
                    sequence.Insert(0, tmp);
                }
            }
        }

        internal static int DigitAt(this int i, int p) => (int) (i % Math.Pow(10, p + 1) / Math.Pow(10, p));
    }
}