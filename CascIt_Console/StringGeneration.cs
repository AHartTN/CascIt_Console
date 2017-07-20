using System;
using System.Collections.Generic;
using System.Linq;

namespace CascIt_Console
{
    public class StringGeneration
    {
        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static void GetPermutations(char[] list)
        {
            int x = list.Length - 1;
            GetPermutations(list, 0, x);
        }

        private static void GetPermutations(char[] list, int k, int m)
        {
            if (k == m)
            {
                Console.Write(list);
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPermutations(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IReadOnlyCollection<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new[] { t2 }));
        }

        public static IEnumerable<string> GetAllValues(int length, int firstChar, int lastChar)
        {
            for (int n = 0; n < lastChar - firstChar; n++)
            {
                string retval = " ";
                for (int i = 0; i < n; i++)
                {
                    var mr = Enumerable.Range(firstChar, lastChar).Cast<char>();
                    foreach (var c in mr)
                    {
                        retval = retval.Substring(0, retval.Length - 1) + c;
                        yield return retval;
                    }
                }
            }
        }

        public IList<String> GetPossibleValues(int maxLength, string alphabet)
        {
            return GetPossibleValuesFrom("", maxLength, alphabet.ToCharArray());
        }

        public IList<String> GetPossibleValues(int maxLength, char[] alphabet)
        {
            return GetPossibleValuesFrom("", maxLength, alphabet);
        }

        private IList<String> GetPossibleValuesFrom(string baseValue, int maxLength, char[] alphabet)
        {
            if (baseValue.Length >= maxLength)
                return new List<string>();

            var list = new List<String>();
            foreach (char c in alphabet)
            {
                list.Add(baseValue + c);
                list.AddRange(GetPossibleValuesFrom(baseValue + c, maxLength, alphabet));
            }
            return list;
        }

    }
}