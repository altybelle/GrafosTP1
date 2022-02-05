using Grafos.TrabalhoPraticoUm.Borders.Graph;

namespace Grafos.TrabalhoPraticoUm.Borders.Extensions
{
    public static class FileGraphExtension
    {
        public static string ToFileString(this FileGraph graph)
        {
            string data = string.Empty;

            if (graph != null)
            {
                data += $"{graph.Nodes}\n";
                for (int i = 1; i <= graph.Nodes; i++)
                {
                    for (int j = i + 1; j <= graph.Nodes; j++)
                    {
                        if (graph.Connections[i,j] != float.MaxValue)
                        {
                            data += $"{i} {j} {graph.Connections[i,j]}";
                        }
                    }
                    
                }
            }

            return data;
        }
    }
}
