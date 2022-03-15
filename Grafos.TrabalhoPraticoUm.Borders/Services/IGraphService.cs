using Grafos.TrabalhoPraticoUm.Borders.Solutions;
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
        Kruskal KruskalsAlgorithm();
        RoyConnectedComponents RoysAlgorithm();
        IEnumerable<int> GreedyHeuristic();
        List<DsaturColouredNodes> DSATUR();
    }
}
