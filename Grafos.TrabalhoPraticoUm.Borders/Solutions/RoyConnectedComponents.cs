using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders.Solutions
{
    public class RoyConnectedComponents
    {
        public RoyConnectedComponents(float[,] graph, int length)
        {
            Result = new Dictionary<string, string>();
            Graph = graph;
            Length = length;
            Amount = 0;
        }
        public Dictionary<string, string> Result { get; set; }
        public int Amount { get; set; }
        private float[,] Graph { get; set; }
        private int Length { get; set; }
        public void Run()
        {
            for (int k = 1; k <= Length; k++)
                for (int i = 1; i <= Length; i++)
                    for (int j = 1; j <= Length; j++)
                        if (Graph[i, k] + Graph[k, j] < Graph[i, j])
                            Graph[i, j] = Graph[i, k] + Graph[k, j];

            RegisterSolution();
        }

        private void RegisterSolution()
        {
            for (int i = 1; i <= Length; ++i) {
                for (int j = i + 1; j <= Length; ++j)
                {
                    if (Graph[i, j] == float.MaxValue)
                    {
                        Result.Add($"({i},{j})", "INF");
                    } else
                    {
                        Result.Add($"({i},{j})", $"{Graph[i, j]}");
                        Amount++;
                    }
                }
            }
        }





















    }
}
