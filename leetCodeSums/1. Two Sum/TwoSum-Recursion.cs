public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        return Find(nums, target, 0);
    }

    private int[] Find(int[] nums, int target, int index) {
        if (index >= nums.Length)
            throw new Exception("No solution");

        for (int j = index + 1; j < nums.Length; j++) {
            if (nums[index] + nums[j] == target)
                return new int[] { index, j };
        }

        return Find(nums, target, index + 1);
    }
}
