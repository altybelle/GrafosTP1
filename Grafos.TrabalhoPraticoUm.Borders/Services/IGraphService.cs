using Grafos.TrabalhoPraticoUm.Borders.Request;
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
        bool IsArticulation(int node);
    }
}
