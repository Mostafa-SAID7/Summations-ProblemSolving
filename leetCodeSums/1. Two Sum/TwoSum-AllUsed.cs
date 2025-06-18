// Two Sum - All Possible Logic Implementations in C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TwoSumVariants
{
    // 1. Hash Map (Efficient)
    public static int[] HashMap(int[] nums, int target)
    {
        var map = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            if (map.ContainsKey(complement))
                return new[] { map[complement], i };
            map[nums[i]] = i;
        }
        return null;
    }

    // 2. Brute Force
    public static int[] BruteForce(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
            for (int j = i + 1; j < nums.Length; j++)
                if (nums[i] + nums[j] == target)
                    return new[] { i, j };
        return null;
    }

    // 3. Two Pointers (Sorted)
    public static int[] TwoPointers(int[] nums, int target)
    {
        var indexed = nums.Select((val, idx) => (val, idx)).ToList();
        indexed.Sort((a, b) => a.val.CompareTo(b.val));
        int left = 0, right = indexed.Count - 1;
        while (left < right)
        {
            int sum = indexed[left].val + indexed[right].val;
            if (sum == target)
                return new[] { indexed[left].idx, indexed[right].idx };
            if (sum < target) left++;
            else right--;
        }
        return null;
    }

    // 4. LINQ Query
    public static int[] LinqQuery(int[] nums, int target)
    {
        var result = (from i in Enumerable.Range(0, nums.Length)
                      from j in Enumerable.Range(i + 1, nums.Length - i - 1)
                      where nums[i] + nums[j] == target
                      select new[] { i, j }).FirstOrDefault();
        return result;
    }

    // 5. Binary Search
    public static int[] BinarySearchBased(int[] nums, int target)
    {
        var indexed = nums.Select((val, idx) => (val, idx)).OrderBy(x => x.val).ToArray();
        for (int i = 0; i < indexed.Length; i++)
        {
            int complement = target - indexed[i].val;
            int lo = i + 1, hi = indexed.Length - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (indexed[mid].val == complement)
                    return new[] { indexed[i].idx, indexed[mid].idx };
                if (indexed[mid].val < complement) lo++;
                else hi--;
            }
        }
        return null;
    }

    // 6. Recursion
    public static int[] Recursion(int[] nums, int target, int index = 0)
    {
        if (index >= nums.Length) return null;
        for (int j = index + 1; j < nums.Length; j++)
            if (nums[index] + nums[j] == target)
                return new[] { index, j };
        return Recursion(nums, target, index + 1);
    }

    // 7. Stream Processing (Online)
    public class TwoSumStream
    {
        private HashSet<int> seen = new HashSet<int>();
        private int target;
        public TwoSumStream(int tgt) => target = tgt;
        public bool Add(int num)
        {
            if (seen.Contains(target - num)) return true;
            seen.Add(num);
            return false;
        }
    }
}