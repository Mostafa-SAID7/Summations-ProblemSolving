public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        // Step 1: Store number with original index
        var indexedNums = new List<(int num, int index)>();
        for (int i = 0; i < nums.Length; i++) {
            indexedNums.Add((nums[i], i));
        }

        // Step 2: Sort by value
        indexedNums.Sort((a, b) => a.num.CompareTo(b.num));

        // Step 3: Two-pointer approach
        int left = 0;
        int right = nums.Length - 1;

        while (left < right) {
            int sum = indexedNums[left].num + indexedNums[right].num;
            if (sum == target) {
                return new int[] { indexedNums[left].index, indexedNums[right].index };
            }
            else if (sum < target) {
                left++;
            }
            else {
                right--;
            }
        }

        throw new ArgumentException("No two sum solution found.");
    }
}
