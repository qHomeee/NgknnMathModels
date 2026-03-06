using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathsMethods
{
    internal class Methods
    {
        //структура результата
        public struct TransportResult
        {
            public int[,] Plan;
            public int TotalCost;

            public TransportResult(int[,] plan, int totalCost)
            {
                Plan = plan;
                TotalCost = totalCost;
            }
        }

       

        private static int ComputeCost(int[,] plan, int[,] costs)
        {
            int total = 0;
            for (int i = 0; i < plan.GetLength(0); i++)
            {
                for (int j = 0; j < plan.GetLength(1); j++)
                {
                    total += plan[i, j] * costs[i, j];
                }
            }
            return total;
        }

        //  1) СЕВЕРО-ЗАПАДНЫЙ УГОЛ
        
        public static TransportResult NorthwestCorner(int[] supplyInput, int[] demandInput, int[,] costsInput)
        {
            int[] supply = (int[])supplyInput.Clone();
            int[] demand = (int[])demandInput.Clone();
            int[,] costs = (int[,])costsInput.Clone();

         

            int n = supply.Length;
            int m = demand.Length;
            int[,] plan = new int[n, m];

            int i = 0, j = 0;
            while (i < n && j < m)
            {
                int x = Math.Min(supply[i], demand[j]);
                plan[i, j] = x;
                supply[i] -= x;
                demand[j] -= x;

                if (supply[i] == 0) i++;
                if (demand[j] == 0) j++;
            }

            int total = ComputeCost(plan, costs);
            return new TransportResult(plan, total);
        }

        //  2) МЕТОД МИНИМАЛЬНОГО ЭЛЕМЕНТА
        public static TransportResult MinElemMethod(int[] supplyInput, int[] demandInput, int[,] costsInput)
        {
            int[] supply = (int[])supplyInput.Clone();
            int[] demand = (int[])demandInput.Clone();
            int[,] costs = (int[,])costsInput.Clone();

            int n = supply.Length;
            int m = demand.Length;
            int[,] plan = new int[n, m];

            bool[] rowDone = new bool[n];
            bool[] colDone = new bool[m];

            int doneRows = 0, doneCols = 0;

            while (doneRows < n || doneCols < m)
            {
                int bestI = -1, bestJ = -1;
                int bestCost = int.MaxValue;

                for (int i = 0; i < n; i++)
                {
                    if (rowDone[i] || supply[i] == 0) continue;
                    for (int j = 0; j < m; j++)
                    {
                        if (colDone[j] || demand[j] == 0) continue;

                        if (costs[i, j] < bestCost)
                        {
                            bestCost = costs[i, j];
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }

                if (bestI == -1 || bestJ == -1) break;

                int x = Math.Min(supply[bestI], demand[bestJ]);
                plan[bestI, bestJ] = x;
                supply[bestI] -= x;
                demand[bestJ] -= x;

                if (supply[bestI] == 0 && !rowDone[bestI])
                {
                    rowDone[bestI] = true;
                    doneRows++;
                }
                if (demand[bestJ] == 0 && !colDone[bestJ])
                {
                    colDone[bestJ] = true;
                    doneCols++;
                }
            }

            int total = ComputeCost(plan, costs);
            return new TransportResult(plan, total);
        }
        




        public static bool OptimOrNot(int[,] Plan)
        {
            
        }
    }
}
