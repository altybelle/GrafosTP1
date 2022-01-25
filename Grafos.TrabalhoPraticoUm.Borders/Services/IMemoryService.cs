using Grafos.TrabalhoPraticoUm.Borders.Graph;

namespace Grafos.TrabalhoPraticoUm.Borders.Services
{
    public interface IMemoryService
    {
        void Save(IGraph graph);
        IGraph Load();
    }
}
