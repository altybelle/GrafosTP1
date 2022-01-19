using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders
{
    public class FileGraph
    {
        public int Nodes { get; set; }
        public int Edges { get; set; }
        public float[,] Connections { get; set; }
    }
}