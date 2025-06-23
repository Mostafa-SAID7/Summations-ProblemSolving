using System;
using System.Linq;
using System.Collections.Generic;

namespace RemoveElementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solver = new Solution();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Remove Element Console Tool =====\n");

                Console.Write("Enter array elements (comma-separated): ");
                string input = Console.ReadLine();
                int[] nums;

                try
                {
                    nums = input.Split(',').Select(int.Parse).ToArray();
                }
                catch
                {
                    Console.WriteLine("Invalid input. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Enter value to remove: ");
                if (!int.TryParse(Console.ReadLine(), out int val))
                {
                    Console.WriteLine("Invalid value. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("\nChoose the method to remove the element:");
                Console.WriteLine("1. Forward Overwrite (Classic Two-Pointer)");
                Console.WriteLine("2. Swap With End (Efficient if value is rare)");
                Console.WriteLine("3. LINQ Filter (Not in-place)");
                Console.WriteLine("4. Mark & Compact (Two-pass)");
                Console.WriteLine("5. Queue-Based Compacting");
                Console.WriteLine("6. Buffer Copy Simulation");
                Console.WriteLine("0. Exit");

                Console.Write("\nYour choice: ");
                string choice = Console.ReadLine();
                int length = 0;
                int[] numsCopy = (int[])nums.Clone();

                switch (choice)
                {
                    case "1":
                        length = solver.RemoveElement_ForwardOverwrite(numsCopy, val);
                        break;
                    case "2":
                        length = solver.RemoveElement_SwapWithEnd(numsCopy, val);
                        break;
                    case "3":
                        var filtered = solver.RemoveElement_Linq(numsCopy, val);
                        Console.WriteLine($"\nResult (new array): [{string.Join(", ", filtered)}]");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    case "4":
                        length = solver.RemoveElement_MarkAndCompact(numsCopy, val);
                        break;
                    case "5":
                        length = solver.RemoveElement_QueueBased(numsCopy, val);
                        break;
                    case "6":
                        length = solver.RemoveElement_BufferCopy(numsCopy, val);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine($"\nNew Length: {length}");
                Console.WriteLine($"Modified Array: [{string.Join(", ", numsCopy.Take(length))}]");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public class Solution
    {
        // 1. Forward overwrite
        public int RemoveElement_ForwardOverwrite(int[] nums, int val)
        {
            int i = 0;
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[j] != val)
                {
                    nums[i++] = nums[j];
                }
            }
            return i;
        }

        // 2. Swap with end
        public int RemoveElement_SwapWithEnd(int[] nums, int val)
        {
            int n = nums.Length;
            int i = 0;
            while (i < n)
            {
                if (nums[i] == val)
                {
                    nums[i] = nums[n - 1];
                    n--;
                }
                else
                {
                    i++;
                }
            }
            return n;
        }

        // 3. LINQ (not in-place)
        public int[] RemoveElement_Linq(int[] nums, int val)
        {
            return nums.Where(x => x != val).ToArray();
        }

        // 4. Mark and compact
        public int RemoveElement_MarkAndCompact(int[] nums, int val)
        {
            int marker = int.MinValue;
            int n = nums.Length;

            // First pass: mark
            for (int i = 0; i < n; i++)
            {
                if (nums[i] == val)
                    nums[i] = marker;
            }

            // Second pass: compact
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                if (nums[i] != marker)
                    nums[index++] = nums[i];
            }

            return index;
        }

        // 5. Queue-based
        public int RemoveElement_QueueBased(int[] nums, int val)
        {
            var queue = new Queue<int>();
            foreach (int num in nums)
            {
                if (num != val)
                    queue.Enqueue(num);
            }

            int i = 0;
            foreach (var item in queue)
                nums[i++] = item;

            return i;
        }

        // 6. Buffer copy with optional sort
        public int RemoveElement_BufferCopy(int[] nums, int val)
        {
            var list = new List<int>();
            int left = 0, right = nums.Length - 1;

            while (left <= right)
            {
                if (nums[left] != val)
                    list.Add(nums[left]);
                if (left != right && nums[right] != val)
                    list.Add(nums[right]);
                left++;
                right--;
            }

            list.Sort(); // Optional
            for (int i = 0; i < list.Count; i++)
                nums[i] = list[i];

            return list.Count;
        }
    }
}
