using Grafos.TrabalhoPraticoUm.Borders.Request;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Borders.UseCases
{
    public interface IGraphVisualizerUseCase
    {
        Task<int> Execute(FileRequest request, string nome);
    }
}
