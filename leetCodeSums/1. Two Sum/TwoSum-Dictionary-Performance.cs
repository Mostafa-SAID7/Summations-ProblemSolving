using System;
using System.Collections.Generic;

public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        // Dictionary to store the number and its index
        Dictionary<int, int> map = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++) {
            int complement = target - nums[i];

            // Check if the complement exists in the dictionary
            if (map.ContainsKey(complement)) {
                // Return indices of the two numbers
                return new int[] { map[complement], i };
            }

            // If not found, store the current number with its index
            if (!map.ContainsKey(nums[i])) {
                map[nums[i]] = i;
            }
        }

        // No solution found - but the problem says one solution always exists
        throw new ArgumentException("No two sum solution exists.");
    }
}
