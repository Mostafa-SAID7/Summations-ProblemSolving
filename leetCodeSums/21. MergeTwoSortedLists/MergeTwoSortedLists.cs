using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeTwoSortedListsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solver = new Solution();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Merge Two Sorted Lists =====");
                Console.WriteLine("Input two sorted lists as comma-separated integers.");
                Console.Write("Enter first sorted list: ");
                ListNode list1 = CreateList(Console.ReadLine());

                Console.Write("Enter second sorted list: ");
                ListNode list2 = CreateList(Console.ReadLine());

                Console.WriteLine("\nChoose the method:");
                Console.WriteLine("1. Two-Pointer Merge Logic (Iterative)");
                Console.WriteLine("2. Recursive Merge Logic");
                Console.WriteLine("3. In-Place Merge (Iterative without dummy)");
                Console.WriteLine("4. Convert to Array + Merge + Rebuild");
                Console.WriteLine("5. LINQ Merge & Sort");
                Console.WriteLine("0. Exit");

                Console.Write("\nYour choice: ");
                string choice = Console.ReadLine();
                ListNode merged = null;

                switch (choice)
                {
                    case "1":
                        merged = solver.MergeTwoLists_TwoPointer(list1, list2);
                        break;
                    case "2":
                        merged = solver.MergeTwoLists_Recursive(list1, list2);
                        break;
                    case "3":
                        merged = solver.MergeTwoLists_InPlace(list1, list2);
                        break;
                    case "4":
                        merged = solver.MergeTwoLists_ArrayMerge(list1, list2);
                        break;
                    case "5":
                        merged = solver.MergeTwoLists_Linq(list1, list2);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine("\nMerged List:");
                PrintList(merged);

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Helper: Create linked list from comma-separated string
        static ListNode CreateList(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            string[] parts = input.Split(',');
            ListNode dummy = new ListNode();
            ListNode current = dummy;

            foreach (var part in parts)
            {
                if (int.TryParse(part.Trim(), out int val))
                {
                    current.next = new ListNode(val);
                    current = current.next;
                }
                else
                {
                    Console.WriteLine($"Invalid number '{part.Trim()}', skipping.");
                }
            }

            return dummy.next;
        }

        // Helper: Print linked list
        static void PrintList(ListNode head)
        {
            if (head == null)
            {
                Console.WriteLine("Empty list.");
                return;
            }

            ListNode current = head;
            while (current != null)
            {
                Console.Write(current.val);
                if (current.next != null)
                    Console.Write(" -> ");
                current = current.next;
            }
            Console.WriteLine();
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class Solution
    {
        // 1. Two-Pointer Merge Logic (Iterative)
        public ListNode MergeTwoLists_TwoPointer(ListNode list1, ListNode list2)
        {
            ListNode dummy = new ListNode();
            ListNode current = dummy;

            while (list1 != null && list2 != null)
            {
                if (list1.val < list2.val)
                {
                    current.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    current.next = list2;
                    list2 = list2.next;
                }
                current = current.next;
            }

            current.next = (list1 != null) ? list1 : list2;
            return dummy.next;
        }

        // 2. Recursive Merge Logic
        public ListNode MergeTwoLists_Recursive(ListNode list1, ListNode list2)
        {
            if (list1 == null) return list2;
            if (list2 == null) return list1;

            if (list1.val < list2.val)
            {
                list1.next = MergeTwoLists_Recursive(list1.next, list2);
                return list1;
            }
            else
            {
                list2.next = MergeTwoLists_Recursive(list1, list2.next);
                return list2;
            }
        }

        // 3. In-Place Merge (Iterative without dummy)
        public ListNode MergeTwoLists_InPlace(ListNode list1, ListNode list2)
        {
            if (list1 == null) return list2;
            if (list2 == null) return list1;

            if (list1.val > list2.val)
            {
                var temp = list1;
                list1 = list2;
                list2 = temp;
            }

            ListNode head = list1;

            while (list1 != null && list2 != null)
            {
                ListNode temp = null;
                while (list1 != null && list1.val <= list2.val)
                {
                    temp = list1;
                    list1 = list1.next;
                }
                temp.next = list2;

                // Swap list1 and list2
                var swap = list1;
                list1 = list2;
                list2 = swap;
            }

            return head;
        }

        // 4. Convert to Array + Merge + Rebuild Linked List
        public ListNode MergeTwoLists_ArrayMerge(ListNode list1, ListNode list2)
        {
            List<int> arr = new List<int>();

            // Convert list1 to array
            while (list1 != null)
            {
                arr.Add(list1.val);
                list1 = list1.next;
            }

            // Convert list2 to array
            while (list2 != null)
            {
                arr.Add(list2.val);
                list2 = list2.next;
            }

            // Sort the merged array
            arr.Sort();

            // Build new linked list from sorted array
            ListNode dummy = new ListNode();
            ListNode current = dummy;

            foreach (int val in arr)
            {
                current.next = new ListNode(val);
                current = current.next;
            }

            return dummy.next;
        }

        // 5. LINQ Merge & Sort
        public ListNode MergeTwoLists_Linq(ListNode list1, ListNode list2)
        {
            // Extract values from both lists using LINQ
            IEnumerable<int> seq1 = EnumerateList(list1);
            IEnumerable<int> seq2 = EnumerateList(list2);

            // Merge and sort
            var mergedSorted = seq1.Concat(seq2).OrderBy(x => x);

            // Build linked list
            ListNode dummy = new ListNode();
            ListNode current = dummy;

            foreach (var val in mergedSorted)
            {
                current.next = new ListNode(val);
                current = current.next;
            }

            return dummy.next;
        }

        // Helper for LINQ extraction
        private IEnumerable<int> EnumerateList(ListNode head)
        {
            while (head != null)
            {
                yield return head.val;
                head = head.next;
            }
        }
    }
}
