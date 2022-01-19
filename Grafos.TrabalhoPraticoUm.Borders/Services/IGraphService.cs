using Grafos.TrabalhoPraticoUm.Borders.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Borders.Services
{
    public interface IGraphService
    {
        Task<int> ReturnOrder(FileRequest request);
        Task<int> ReturnSize(FileRequest request);
        Task<int> ReturnDensity(FileRequest request);
        Task<IEnumerable<int>> ReturnNeighborhood(FileRequest request, int node);
        Task<int> ReturnDegree(FileRequest request, int node);
        Task<bool> IsArticulation(FileRequest request, int node);
    }
}
