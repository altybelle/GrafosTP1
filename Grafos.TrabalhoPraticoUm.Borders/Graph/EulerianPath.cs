using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders.Graph
{
    public class EulerianPath
    {
        public EulerianPath(float[,] originalGraph, int size)
        {
            IsEulerian = false;
            ClosedPath = new List<int>();
            Length = size - 1;
            AuxiliarGraph = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    AuxiliarGraph[i, j] = originalGraph[i, j];
                }
            }
        }
        public bool IsEulerian { get; set; }
        public List<int> ClosedPath { get; set; }
        private float[,] AuxiliarGraph { get; set; }
        public int Length { get; set; }
        public bool IsBridge(int x)
        {
            int degree = 0;
            for (int i = 1; i <= Length; i++)
            {
                if (AuxiliarGraph[x, i] != float.MaxValue)
                {
                    degree++;
                }
                if (degree > 1)
                    return false;
            }
            return true;
        }

        public int EdgeCount()
        {
            int count = 0;
            for (int i = 1; i <= Length; i++)
            {
                for (int j = i; j <= Length; j++)
                {
                    if (AuxiliarGraph[i, j] != float.MaxValue)
                        count++;
                }
            }
            return count;
        }

        public void Fleury(int start)
        {
            int edgeAmount = EdgeCount();
            for (int i = 1; i <= Length; i++)
            {
                if (AuxiliarGraph[start, i] != float.MaxValue)
                {
                    if (edgeAmount <= 1 || !IsBridge(i))
                    {
                        ClosedPath.Add(start);
                        AuxiliarGraph[start, i] = AuxiliarGraph[i, start] = float.MaxValue;
                        edgeAmount--;
                        Fleury(i);
                    }
                }
            }
        }
    }
}
