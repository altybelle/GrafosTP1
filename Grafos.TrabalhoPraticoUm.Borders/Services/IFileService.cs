using Grafos.TrabalhoPraticoUm.Borders.Graph;
using Grafos.TrabalhoPraticoUm.Borders.Solutions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Borders.Services
{
    public interface IFileService
    {
        FileGraph ConvertFromJson(JsonGraph graph);
        JsonGraph ConvertFromTxt(FileGraph graph);
        Task<JsonGraph> ReadJson(IFormFile request);
        Task<FileGraph> ReadTxt(IFormFile request);
        (byte[], string, string) GenerateConvertedFile(IGraph graph);
        (byte[], string, string) GenerateKruskalFile(Kruskal kruskal);
    }
}
