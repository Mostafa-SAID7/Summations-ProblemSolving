using System.Threading.Tasks;

public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        int[] result = null;
        Parallel.For(0, nums.Length, (i, state) => {
            for (int j = i + 1; j < nums.Length; j++) {
                if (nums[i] + nums[j] == target) {
                    result = new int[] { i, j };
                    state.Stop(); // Stop all threads
                }
            }
        });

        return result ?? throw new Exception("No solution");
    }
}
