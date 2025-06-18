using System.Linq;

public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var query = (from i in Enumerable.Range(0, nums.Length).AsParallel()
                     from j in Enumerable.Range(i + 1, nums.Length - i - 1)
                     where nums[i] + nums[j] == target
                     select new int[] { i, j }).FirstOrDefault();

        return query ?? throw new Exception("No solution");
    }
}
