using Grafos.TrabalhoPraticoUm.Borders.Request;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Borders.Services
{
    public interface IFileService
    {
        Task<FileGraph> ConvertFromJson(FileRequest request);
        Task<JsonGraph> ConvertFromTxt(FileRequest request);
        Task<JsonGraph> ReadJson(IFormFile request);
        Task<FileGraph> ReadTxt(IFormFile request);
    }
}
