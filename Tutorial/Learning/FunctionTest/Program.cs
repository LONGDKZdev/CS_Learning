// See https://aka.ms/new-console-template for more information
using System;

namespace Method
{
    class Program
    {
        static void Show(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Length > 3)
                {
                    Console.Write(arr[i] + " ");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Enter the number of strings:");
            int n = int.Parse(Console.ReadLine());
            string[] arr = new string[n];
            for (int i = 0; i < n; i++)
            {
                arr[i] = Console.ReadLine();
            }
            Console.WriteLine("Strings with more than 3 characters:");
            Show(arr);
            Console.ReadKey();
        }
    }
}