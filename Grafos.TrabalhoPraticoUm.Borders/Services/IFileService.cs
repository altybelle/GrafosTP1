using Grafos.TrabalhoPraticoUm.Borders.Request;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Borders.Services
{
    public interface IFileService
    {
        Task<JsonGraph> ConvertFromJson(FileRequest request);
        Task<JsonGraph> ConvertFromTxt(FileRequest request);
    }
}
