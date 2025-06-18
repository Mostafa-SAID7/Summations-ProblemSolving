public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        Func<int, int, bool> isMatch = (a, b) => nums[a] + nums[b] == target;

        for (int i = 0; i < nums.Length; i++) {
            for (int j = i + 1; j < nums.Length; j++) {
                if (isMatch(i, j))
                    return new int[] { i, j };
            }
        }

        throw new Exception("No solution");
    }
}
