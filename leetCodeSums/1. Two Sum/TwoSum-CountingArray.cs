public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        int max = 10000;
        int[] seen = new int[2 * max + 1];
        Array.Fill(seen, -1);

        for (int i = 0; i < nums.Length; i++) {
            int complement = target - nums[i];
            int index = complement + max;

            if (seen[index] != -1) {
                return new int[] { seen[index], i };
            }

            seen[nums[i] + max] = i;
        }

        throw new Exception("No solution");
    }
}
