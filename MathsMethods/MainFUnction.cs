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
            int lengn = arrStore.Length + 1;
            int lengm = arrCustomer.Length + 1;
            int[,] arrFinal = new int[lengn, lengm];
            int k = 0;
            int num = 0;
            int l = 0;
            int num2 = 0;
            int predel = arrRate.GetLength(0);
            int predel2 = arrRate.GetLength(1);
            for (int i = 0; i < arrFinal.GetLength(0); i++)
            {
                for (int j = 0; j < arrFinal.GetLength(1); j++)
                {
                    if (j == 0 && i == 0)
                    {
                        arrFinal[i, j] = 0;
                    }
                    else if (j != 0 && i == 0)
                    {
                        arrFinal[i, j] = arrStore[num];
                        num++;
                    }
                    else if (j == 0 && i != 0)
                    {
                        arrFinal[i, j] = arrCustomer[num2];
                        num2++;

                    }
                    else
                    {
                        if (k < predel)
                        {
                            arrFinal[i, j] = arrRate[k, l];
                            l++;
                            if (l == predel2 && k < predel)
                            {
                                k++;
                                l = 0;
                            }
                        }

                    }
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
