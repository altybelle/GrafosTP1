using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class FileService : IFileService
    {
        public async Task<JsonGraph> ConvertFromJson(FileRequest request)
        {
            var obj = await ReadJson(request.File);
            return obj;
        }

        public async Task<JsonGraph> ConvertFromTxt(FileRequest request)
        {
            var obj = await ReadTxt(request.File);
            return obj;
        }

        internal async Task<JsonGraph> ReadTxt(IFormFile file)
        {
            return await ReadJson(file);
        }

        internal async Task<JsonGraph> ReadJson(IFormFile file)
        {
            JsonGraph obj = null;

            if (file != null && file.Length > 0)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                string json = Encoding.UTF8.GetString(ms.ToArray());
                obj = JsonSerializer.Deserialize<JsonGraph>(json);
            }

            return obj;
        } 
    }
}
