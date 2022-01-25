using Grafos.TrabalhoPraticoUm.Borders.Graph;
using Grafos.TrabalhoPraticoUm.Borders.Services;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class MemoryService: IMemoryService
    {
        private IGraph Graph { get; set; }
        public void Save(IGraph graph)
        {
            this.Graph = graph;
        }

        public IGraph Load()
        {
            return Graph;
        }
    }
}
