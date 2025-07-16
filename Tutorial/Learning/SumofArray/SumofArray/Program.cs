using System;

namespace Method
{
    class Program
    {
        public static int SumOfArray(int[] arr, int n)
        {
            if (n == 1) return arr[0];
            return arr[n - 1] + SumOfArray(arr, n - 1);
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }
            Console.Write(SumOfArray(arr, n));
        }
    }
}