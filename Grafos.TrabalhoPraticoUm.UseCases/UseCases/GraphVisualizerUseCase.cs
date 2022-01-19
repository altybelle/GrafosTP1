using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Borders.UseCases;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.UseCases.UseCases
{
    public class GraphVisualizerUseCase : IGraphVisualizerUseCase
    {
        private readonly IFileService fileService;
        private readonly IGraphService graphService;

        public GraphVisualizerUseCase(IFileService fileService, IGraphService graphService)
        {
            this.fileService = fileService;
            this.graphService = graphService;
        }

        public async Task<int> Execute(FileRequest request, string nome) // Recebendo uma request
        {
            var obj = await fileService.ConvertFromTxt(request);
            return obj.Data.Edges.Data[nome].From;
        }
    }
}
