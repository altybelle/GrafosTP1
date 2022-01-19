using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders
{
    public class FileGraph
    {
        public int Lines { get; set; }
        public int Edges { get; set; }
        public double[,] Connections { get; set; }
    }
}