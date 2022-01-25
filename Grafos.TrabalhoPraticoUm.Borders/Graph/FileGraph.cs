namespace Grafos.TrabalhoPraticoUm.Borders.Graph
{
    public class FileGraph : IGraph
    {
        public int Nodes { get; set; }
        public int Edges { get; set; }
        public float[,] Connections { get; set; }
    }
}