using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MathsMethods
{
    internal class MainFunction
    {
        public static int[,] CreateFinalTable(int[,] arrRate, int[] arrCustomer, int[] arrStore)
        {
            int rows = arrStore.Length + 1;    
            int cols = arrCustomer.Length + 1;  
            int[,] arrFinal = new int[rows, cols];

            arrFinal[0, 0] = 0;


            for (int j = 1; j < cols; j++)
            {
                arrFinal[0, j] = arrCustomer[j - 1];
            }

            for (int i = 1; i < rows; i++)
            {
                arrFinal[i, 0] = arrStore[i - 1];
            }

            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    arrFinal[i, j] = arrRate[i - 1, j - 1];
                }
            }
            return arrFinal;
        }
        public static void printmas(int[,] array)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        Console.Write(" " + array[i, j]);
                    }
                    Console.WriteLine();
                }
           }
        public static int[] initmas( ref int[] mas)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                try
                {
                    mas[i] = int.Parse(Console.ReadLine()!);
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }

            }
            return mas;
        }

        public static int[,] init2mas(ref int[,] mas)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    try
                    {
                        mas[i, j] = int.Parse(Console.ReadLine()!);
                    }
                    catch (Exception e) { Console.WriteLine(e.ToString()); }
                }
            }
            return mas;
        }
    }
}
