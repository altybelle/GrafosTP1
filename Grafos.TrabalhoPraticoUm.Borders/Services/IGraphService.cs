using Grafos.TrabalhoPraticoUm.Borders.Graph;
using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Borders.Services
{
    public interface IGraphService
    {
        int ReturnOrder();
        int ReturnSize();
        float ReturnDensity();
        IEnumerable<int> ReturnNeighborhood(int node);
        int ReturnDegree(int node);
        bool IsArticulated(int node);
        IEnumerable<string> BFS(int node);
        bool IsCyclic();
        EulerianPath EulerianPath();
        Djikstra DistanceAndShortestPath(int node);
    }
}
