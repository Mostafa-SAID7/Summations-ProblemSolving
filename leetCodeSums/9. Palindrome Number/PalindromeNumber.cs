using System;

namespace PalindromeNumberApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solver = new Solution();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Palindrome Number Checker =====");
                Console.Write("Enter an integer to test: ");

                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Invalid input. Press any key to try again.");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("\nChoose the method:");
                Console.WriteLine("1. String Reverse Comparison");
                Console.WriteLine("2. Two-Pointer String Comparison");
                Console.WriteLine("3. Full Integer Reversal");
                Console.WriteLine("4. Optimized Half Reverse");
                Console.WriteLine("5. Digit-by-Digit Comparison");
                Console.WriteLine("0. Exit");

                Console.Write("\nYour choice: ");
                string choice = Console.ReadLine();
                bool result = false;

                switch (choice)
                {
                    case "1":
                        result = solver.IsPalindrome_StringReverse(input);
                        break;
                    case "2":
                        result = solver.IsPalindrome_TwoPointerString(input);
                        break;
                    case "3":
                        result = solver.IsPalindrome_IntegerReverse(input);
                        break;
                    case "4":
                        result = solver.IsPalindrome_HalfReverse(input);
                        break;
                    case "5":
                        result = solver.IsPalindrome_DigitCompare(input);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine($"\nResult: {(result ? "Palindrome ✅" : "Not a Palindrome ❌")}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public class Solution
    {
        public bool IsPalindrome_StringReverse(int x)
        {
            string s = x.ToString();
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return s == new string(arr);
        }

        public bool IsPalindrome_TwoPointerString(int x)
        {
            string s = x.ToString();
            int left = 0, right = s.Length - 1;

            while (left < right)
            {
                if (s[left] != s[right])
                    return false;
                left++;
                right--;
            }

            return true;
        }

        public bool IsPalindrome_IntegerReverse(int x)
        {
            if (x < 0) return false;

            int original = x, reversed = 0;

            while (x != 0)
            {
                int digit = x % 10;

                if (reversed > (int.MaxValue - digit) / 10)
                    return false;

                reversed = reversed * 10 + digit;
                x /= 10;
            }

            return original == reversed;
        }

        public bool IsPalindrome_HalfReverse(int x)
        {
            if (x < 0 || (x % 10 == 0 && x != 0))
                return false;

            int reversed = 0;
            while (x > reversed)
            {
                reversed = reversed * 10 + x % 10;
                x /= 10;
            }

            return x == reversed || x == reversed / 10;
        }

        public bool IsPalindrome_DigitCompare(int x)
        {
            if (x < 0) return false;

            int div = 1;
            while (x / div >= 10)
                div *= 10;

            while (x != 0)
            {
                int left = x / div;
                int right = x % 10;

                if (left != right)
                    return false;

                x = (x % div) / 10;
                div /= 100;
            }

            return true;
        }
    }
}
