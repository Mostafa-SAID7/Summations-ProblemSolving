using System;

namespace FirstOccurrenceFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solver = new Solution();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Find First Occurrence in String =====");

                Console.Write("Enter haystack: ");
                string haystack = Console.ReadLine();

                Console.Write("Enter needle: ");
                string needle = Console.ReadLine();

                if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
                {
                    Console.WriteLine("Invalid input. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("\nChoose the algorithm:");
                Console.WriteLine("1. Naive Brute Force");
                Console.WriteLine("2. Sliding Window Comparison");
                Console.WriteLine("3. Knuth-Morris-Pratt (KMP)");
                Console.WriteLine("0. Exit");
                Console.Write("\nYour choice: ");
                string choice = Console.ReadLine();

                int index = -1;

                switch (choice)
                {
                    case "1":
                        index = solver.StrStr_Naive(haystack, needle);
                        break;
                    case "2":
                        index = solver.StrStr_SlidingWindow(haystack, needle);
                        break;
                    case "3":
                        index = solver.StrStr_KMP(haystack, needle);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine($"\nResult: {index}");
                Console.WriteLine(index == -1 ? "❌ Needle not found." : $"✅ Found at index {index}.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public class Solution
    {
        // 1. Naive Brute-Force
        public int StrStr_Naive(string haystack, string needle)
        {
            if (needle.Length > haystack.Length) return -1;

            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                bool match = true;
                for (int j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match) return i;
            }

            return -1;
        }

        // 2. Sliding Window with string.Equals
        public int StrStr_SlidingWindow(string haystack, string needle)
        {
            int h = haystack.Length, n = needle.Length;
            if (n > h) return -1;

            for (int i = 0; i <= h - n; i++)
            {
                if (haystack.Substring(i, n).Equals(needle))
                {
                    return i;
                }
            }

            return -1;
        }

        // 3. KMP Algorithm (O(n + m) time)
        public int StrStr_KMP(string haystack, string needle)
        {
            if (needle.Length == 0) return 0;

            int[] lps = BuildLPS(needle);
            int i = 0, j = 0;

            while (i < haystack.Length)
            {
                if (haystack[i] == needle[j])
                {
                    i++; j++;
                    if (j == needle.Length)
                        return i - j;
                }
                else
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }

            return -1;
        }

        // LPS = Longest Prefix Suffix table
        private int[] BuildLPS(string needle)
        {
            int[] lps = new int[needle.Length];
            int len = 0;
            int i = 1;

            while (i < needle.Length)
            {
                if (needle[i] == needle[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                        len = lps[len - 1];
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }
    }
}
