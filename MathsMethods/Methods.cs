using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathsMethods
{
    internal class Methods
    {
        // Удобная структура результата: план + стоимость
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

        // ===========================
        //  ОБЩИЕ ВСПОМОГАТЕЛЬНЫЕ ШТУКИ
        // ===========================
        private static void BalanceProblem(ref int[] supply, ref int[] demand, ref int[,] costs)
        {
            int sumSupply = 0;
            int sumDemand = 0;
            for (int i = 0; i < supply.Length; i++) sumSupply += supply[i];
            for (int j = 0; j < demand.Length; j++) sumDemand += demand[j];

            if (sumSupply == sumDemand) return;

            if (sumSupply < sumDemand)
            {
                // Добавляем фиктивного поставщика
                int diff = sumDemand - sumSupply;

                int[] newSupply = new int[supply.Length + 1];
                for (int i = 0; i < supply.Length; i++) newSupply[i] = supply[i];
                newSupply[newSupply.Length - 1] = diff;

                int[,] newCosts = new int[costs.GetLength(0) + 1, costs.GetLength(1)];
                for (int i = 0; i < costs.GetLength(0); i++)
                {
                    for (int j = 0; j < costs.GetLength(1); j++)
                    {
                        newCosts[i, j] = costs[i, j];
                    }
                }
                // Тарифы фиктивного поставщика = 0
                for (int j = 0; j < newCosts.GetLength(1); j++)
                {
                    newCosts[newCosts.GetLength(0) - 1, j] = 0;
                }
                supply = newSupply;
                costs = newCosts;
            }
            else
            {
                // Добавляем фиктивного потребителя
                int diff = sumSupply - sumDemand;

                int[] newDemand = new int[demand.Length + 1];
                for (int j = 0; j < demand.Length; j++) newDemand[j] = demand[j];
                newDemand[newDemand.Length - 1] = diff;

                int[,] newCosts = new int[costs.GetLength(0), costs.GetLength(1) + 1];
                for (int i = 0; i < costs.GetLength(0); i++)
                {
                    for (int j = 0; j < costs.GetLength(1); j++)
                    {
                        newCosts[i, j] = costs[i, j];
                    }
                }

                // Тарифы фиктивного потребителя = 0
                for (int i = 0; i < newCosts.GetLength(0); i++)
                {
                    newCosts[i, newCosts.GetLength(1) - 1] = 0;
                }
                demand = newDemand;
                costs = newCosts;
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

        // ===========================
        //  1) СЕВЕРО-ЗАПАДНЫЙ УГОЛ
        // ===========================
        public static TransportResult NorthwestCorner(int[] supplyInput, int[] demandInput, int[,] costsInput)
        {
            int[] supply = (int[])supplyInput.Clone();
            int[] demand = (int[])demandInput.Clone();
            int[,] costs = (int[,])costsInput.Clone();

            BalanceProblem(ref supply, ref demand, ref costs);

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

        // ===========================
        //  2) МЕТОД МИНИМАЛЬНОГО ЭЛЕМЕНТА
        // ===========================
        public static TransportResult MinCostMethod(int[] supplyInput, int[] demandInput, int[,] costsInput)
        {
            int[] supply = (int[])supplyInput.Clone();
            int[] demand = (int[])demandInput.Clone();
            int[,] costs = (int[,])costsInput.Clone();

            BalanceProblem(ref supply, ref demand, ref costs);

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

                // если не нашли (бывает при нулях) — выходим
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

        // ===========================
        //  3) АППРОКСИМАЦИЯ ФОГЕЛЯ (VAM)
        // ===========================
        public static TransportResult VogelApproximation(int[] supplyInput, int[] demandInput, int[,] costsInput)
        {
            int[] supply = (int[])supplyInput.Clone();
            int[] demand = (int[])demandInput.Clone();
            int[,] costs = (int[,])costsInput.Clone();

            BalanceProblem(ref supply, ref demand, ref costs);

            int n = supply.Length;
            int m = demand.Length;
            int[,] plan = new int[n, m];

            bool[] rowDone = new bool[n];
            bool[] colDone = new bool[m];
            int activeRows = n, activeCols = m;

            while (activeRows > 0 && activeCols > 0)
            {
                // штрафы строк и столбцов
                int[] rowPenalty = new int[n];
                int[] colPenalty = new int[m];

                for (int i = 0; i < n; i++)
                {
                    if (rowDone[i] || supply[i] == 0) { rowPenalty[i] = -1; continue; }

                    int min1 = int.MaxValue, min2 = int.MaxValue;
                    for (int j = 0; j < m; j++)
                    {
                        if (colDone[j] || demand[j] == 0) continue;
                        int c = costs[i, j];
                        if (c < min1) { min2 = min1; min1 = c; }
                        else if (c < min2) { min2 = c; }
                    }
                    rowPenalty[i] = (min2 == int.MaxValue) ? min1 : (min2 - min1);
                }

                for (int j = 0; j < m; j++)
                {
                    if (colDone[j] || demand[j] == 0) { colPenalty[j] = -1; continue; }

                    int min1 = int.MaxValue, min2 = int.MaxValue;
                    for (int i = 0; i < n; i++)
                    {
                        if (rowDone[i] || supply[i] == 0) continue;
                        int c = costs[i, j];
                        if (c < min1) { min2 = min1; min1 = c; }
                        else if (c < min2) { min2 = c; }
                    }
                    colPenalty[j] = (min2 == int.MaxValue) ? min1 : (min2 - min1);
                }

                // выбираем максимальный штраф
                bool chooseRow = true;
                int idx = -1;
                int bestPenalty = -1;

                for (int i = 0; i < n; i++)
                    if (rowPenalty[i] > bestPenalty)
                    {
                        bestPenalty = rowPenalty[i];
                        idx = i;
                        chooseRow = true;
                    }

                for (int j = 0; j < m; j++)
                    if (colPenalty[j] > bestPenalty)
                    {
                        bestPenalty = colPenalty[j];
                        idx = j;
                        chooseRow = false;
                    }

                // теперь в выбранной строке/столбце ищем ячейку с минимальным тарифом
                int bestI = -1, bestJ = -1;
                int bestCost = int.MaxValue;

                if (chooseRow)
                {
                    int i = idx;
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
                else
                {
                    int j = idx;
                    for (int i = 0; i < n; i++)
                    {
                        if (rowDone[i] || supply[i] == 0) continue;
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
                    activeRows--;
                }
                if (demand[bestJ] == 0 && !colDone[bestJ])
                {
                    colDone[bestJ] = true;
                    activeCols--;
                }
            }

            int total = ComputeCost(plan, costs);
            return new TransportResult(plan, total);
        }
    }
}
