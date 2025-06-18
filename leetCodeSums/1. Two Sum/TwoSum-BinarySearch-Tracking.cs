public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var indexedNums = new List<(int num, int index)>();
        for (int i = 0; i < nums.Length; i++) {
            indexedNums.Add((nums[i], i));
        }

        indexedNums.Sort((a, b) => a.num.CompareTo(b.num));

        for (int i = 0; i < indexedNums.Count; i++) {
            int left = i + 1, right = indexedNums.Count - 1;
            int complement = target - indexedNums[i].num;

            while (left <= right) {
                int mid = left + (right - left) / 2;
                if (indexedNums[mid].num == complement) {
                    return new int[] { indexedNums[i].index, indexedNums[mid].index };
                } else if (indexedNums[mid].num < complement) {
                    left = mid + 1;
                } else {
                    right = mid - 1;
                }
            }
        }
    }
}
