using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders.Graph
{
    public class Djikstra
    {
        public Djikstra(float[,] originalGraph, int length)
        {
            Result = new List<DjikstraPair>();
            ShortestPathSet = new List<bool>();
            Graph = new float[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Graph[i, j] = originalGraph[i, j];
                }
            }
            GraphLength = length - 1;
            Minimum = -1;
        }
        public List<DjikstraPair> Result { get; set; }
        public int Minimum { get; set; }
        private List<bool> ShortestPathSet { get; set; }
        private float[,] Graph { get; set; }
        private int GraphLength { get; set; }
        public void Run(int start)
        {
            for (int i = 0; i < GraphLength; i++)
            {
                Result.Add(new DjikstraPair { Node = i + 1, Distance = float.MaxValue });
                ShortestPathSet.Add(false);
            }

            Result[start - 1] = new DjikstraPair {Node = start, Distance = 0 };

            for (int count = 0; count < GraphLength - 1; count++)
            {
                MinimumDistance();
                ShortestPathSet[Minimum - 1] = true;
                for (int v = 0; v < GraphLength; v++)
                {
                    if (!ShortestPathSet[v] && Graph[Minimum, v + 1] != float.MaxValue &&
                        Result[Minimum - 1].Distance != float.MaxValue && Result[Minimum - 1].Distance + Graph[Minimum, v + 1] < Result[v].Distance)
                    {
                        Result[v] = new DjikstraPair { Node = v + 1, Distance = Result[Minimum - 1].Distance + Graph[Minimum, v + 1] };
                    }
                }
            }
        }
        public void MinimumDistance()
        {
            float min = float.MaxValue;
            for (int i = 1; i <= GraphLength; i++)
            {
                if (!ShortestPathSet[i - 1] && Result[i - 1].Distance <= min)
                {
                    min = Result[i - 1].Distance;
                    Minimum = i;
                }
            }
        }
    }
}
