using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] input = { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };

        RunAllMethods(input);
    }

    static void RunAllMethods(int[] original)
    {
        Console.WriteLine("Original array: " + string.Join(", ", original));

        // Clone array for each method
        Run("Two-pointer", original, RemoveDuplicates_TwoPointer);
        Run("Manual Index", original, RemoveDuplicates_ManualIndex);
        Run("Brute-force List", original, RemoveDuplicates_List);
        Run("HashSet + Sort (invalid)", original, RemoveDuplicates_HashSet);
        Run("LINQ (invalid)", original, RemoveDuplicates_LINQ);
        Run("Two-loop Compact", original, RemoveDuplicates_TwoLoop);
    }

    static void Run(string label, int[] input, Func<int[], int> method)
    {
        int[] nums = (int[])input.Clone();
        int k = method(nums);
        Console.WriteLine($"{label} => k = {k}, result: [{string.Join(", ", nums.Take(k))}]");
    }

    // Logic: Use two pointers (slow-fast). Fast scans, slow overwrites next unique spot.
    static int RemoveDuplicates_TwoPointer(int[] nums)
    {
        if (nums.Length == 0) return 0;
        int k = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[i - 1])
            {
                nums[k] = nums[i];
                k++;
            }
        }
        return k;
    }

    // Logic: Same as above but with a slightly different naming (manual index strategy).
    static int RemoveDuplicates_ManualIndex(int[] nums)
    {
        if (nums.Length == 0) return 0;
        int index = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[index])
            {
                index++;
                nums[index] = nums[i];
            }
        }
        return index + 1;
    }

    // Logic: Brute-force using List<T> to track uniques (violates in-place constraint).
    static int RemoveDuplicates_List(int[] nums)
    {
        List<int> unique = new List<int>();
        foreach (int num in nums)
        {
            if (unique.Count == 0 || unique[unique.Count - 1] != num)
                unique.Add(num);
        }
        for (int i = 0; i < unique.Count; i++)
            nums[i] = unique[i];
        return unique.Count;
    }

    // Logic: Use HashSet + Sort (not valid for LeetCode 26, just a demonstration).
    static int RemoveDuplicates_HashSet(int[] nums)
    {
        HashSet<int> set = new HashSet<int>(nums);
        int[] result = set.ToArray();
        Array.Sort(result);
        for (int i = 0; i < result.Length; i++)
            nums[i] = result[i];
        return result.Length;
    }

    // Logic: Use LINQ to get distinct values (not in-place, invalid per constraints).
    static int RemoveDuplicates_LINQ(int[] nums)
    {
        var distinct = nums.Distinct().ToArray();
        for (int i = 0; i < distinct.Length; i++)
            nums[i] = distinct[i];
        return distinct.Length;
    }

    // Logic: Use nested loops (inefficient O(n^2)), shifting elements left to remove duplicates.
    static int RemoveDuplicates_TwoLoop(int[] nums)
    {
        int n = nums.Length;
        if (n == 0) return 0;
        int k = 1;

        for (int i = 1; i < n; i++)
        {
            bool isDuplicate = false;
            for (int j = 0; j < k; j++)
            {
                if (nums[i] == nums[j])
                {
                    isDuplicate = true;
                    break;
                }
            }
            if (!isDuplicate)
            {
                nums[k++] = nums[i];
            }
        }
        return k;
    }
}
