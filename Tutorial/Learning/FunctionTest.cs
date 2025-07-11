using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial.testCode
{
    public class FunctionTest
    {
            class Program {
        static void Show(string[] arr){
            for(int i = 0; i < arr.Length; i++){
                if(arr[i].Length > 3){
                    Console.Write(arr[i] + " ");
                }
            }
        }

        static void Main(string[] args) {
            int n = int.Parse(Console.ReadLine());
            string[] arr = new string[n];
            for (int i = 0; i < n; i++) {
                arr[i] = Console.ReadLine();
            }
            Show(arr);
        }
    }
    }
}