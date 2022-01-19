using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class GraphService : IGraphService
    {
        private readonly IFileService fileService;
        public GraphService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<bool> IsArticulation(FileRequest request, int node)
        {
            var graph = await CreateGraph(request);
            return true;
        }

        public async Task<int> ReturnDegree(FileRequest request, int node)
        {
            var graph = await CreateGraph(request);
            return 1;
        }

        public async Task<int> ReturnDensity(FileRequest request)
        {
            var graph = await CreateGraph(request);
            return 1;
        }

        public async Task<IEnumerable<int>> ReturnNeighborhood(FileRequest request, int node)
        {
            var graph = await CreateGraph(request);
            var neighbors = new List<int>();
            return neighbors;
        }

        public async Task<int> ReturnOrder(FileRequest request)
        {
            var graph = await CreateGraph(request);
            return 1;
        }

        public async Task<int> ReturnSize(FileRequest request)
        {
            var graph = await CreateGraph(request);
            return 1;
        }

        internal async Task<FileGraph> CreateGraph(FileRequest request)
        {
            return (await ValidateContentTypeAndGenerateGraph(request));
        }

        internal async Task<FileGraph> ValidateContentTypeAndGenerateGraph(FileRequest request)
        {
            if (request.File.ContentType == Constants.FileContent.JsonFormat)
            {
                return (await fileService.ConvertFromJson(request));
            }
            else if (request.File.ContentType == Constants.FileContent.TxtFormat)
            {
                return (await fileService.ReadTxt(request.File));
            }

            throw new InvalidContentTypeException("[GraphService][ValidateContentTypeAndGeneraateGraph] The type of this file is not supported.");
        }
    }
}
