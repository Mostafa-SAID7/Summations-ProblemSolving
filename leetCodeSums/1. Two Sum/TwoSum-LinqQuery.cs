public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var indexedNums = nums.Select((val, idx) => (val, idx)).ToList();
        indexedNums.Sort((a, b) => a.val.CompareTo(b.val));

        int left = 0, right = nums.Length - 1;
        while (left < right) {
            int sum = indexedNums[left].val + indexedNums[right].val;
            if (sum == target)
                return new int[] { indexedNums[left].idx, indexedNums[right].idx };
            else if (sum < target)
                left++;
            else
                right--;
        }

        throw new Exception("No solution");
    }
}
