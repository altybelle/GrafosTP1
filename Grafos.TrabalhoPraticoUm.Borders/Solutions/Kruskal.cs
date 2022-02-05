using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders.Solutions
{
    public class Kruskal
    {
        public Dictionary<int, string> Tree { get; set; }
        private List<int> Parents { get; set; }
        public float MinCost { get; set; }
        public void KruskalMinimumSpanningTree(float[,] connections, int length)
        {
            Tree = new Dictionary<int, string>();
            Parents = new List<int>();

            MinCost = 0;
            int edgeCount = 0;

            for (int i = 0; i < length; i++) {
                Parents.Add(i);
            }

            while (edgeCount < length - 1)
            {
                float min = float.MaxValue;
                int a = -1, b = -1;
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (Find(i) != Find(j) && connections[i, j] < min)
                        {
                            min = connections[i, j];
                            a = i;
                            b = j;
                        }
                    }
                }
                Union(a, b);
                Tree.Add(edgeCount + 1, $"{a + 1} {b + 1} {min}");
                MinCost += min;
                edgeCount++;
            }
        }
        public int Find(int i)
        {
            while (Parents[i] != i)
                i = Parents[i];
            return i;
        }
        public void Union(int i, int j)
        {
            int a = Find(i);
            int b = Find(j);
            Parents[a] = b;
        }
    }
}
