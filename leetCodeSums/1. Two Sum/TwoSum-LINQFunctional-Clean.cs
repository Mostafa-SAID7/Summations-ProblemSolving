using System;
using System.Linq;

public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        return nums
            .Select((num, index) => new { num, index })
            .SelectMany((x, i) =>
                nums.Skip(i + 1).Select((num2, j) => new { i1 = x.index, i2 = j + i + 1, sum = x.num + num2 }))
            .Where(pair => pair.sum == target)
            .Select(pair => new int[] { pair.i1, pair.i2 })
            .FirstOrDefault();
    }
}
