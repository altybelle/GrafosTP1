using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class FileService : IFileService
    {
        public async Task<FileGraph> ConvertFromJson(FileRequest request)
        {
            var obj = await ReadTxt(request.File);
            return obj;
        }

        public async Task<JsonGraph> ConvertFromTxt(FileRequest request)
        {
            var obj = await ReadJson(request.File);
            return obj;
        }

        public async Task<FileGraph> ReadTxt(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                throw new FileIsNullOrEmptyException("[FileService][ReadTxt] The file is null or empty.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            string txt = Encoding.UTF8.GetString(ms.ToArray()).Replace("\r", string.Empty);
            string[] data = txt.Split("\n");

            int edgeAmount = int.Parse(data[0]);

            var obj = new FileGraph
            {
                Nodes = edgeAmount,
                Connections = new float[edgeAmount + 1, edgeAmount + 1]
            };
            for (int i = 0; i <= edgeAmount; i++)
            {
                for(int j = 0; j <= edgeAmount; j++)
                {
                    obj.Connections[i, j] = float.MaxValue;
                }                    
            }

            for (int i = 1; i < data.Length; i++)
            {
                string[] connections = data[i].Split(" ");

                int index1 = int.Parse(connections[0]);
                int index2 = int.Parse(connections[1]);
                float value = float.Parse(connections[2], CultureInfo.InvariantCulture);

                obj.Connections[index1, index2] = value;
                obj.Connections[index2, index1] = value;

            }

            return obj;
        }

        public async Task<JsonGraph> ReadJson(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                throw new FileIsNullOrEmptyException("[FileService][ReadJson] The file is null or empty.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            string json = Encoding.UTF8.GetString(ms.ToArray());
            return JsonSerializer.Deserialize<JsonGraph>(json);
        }
    }
}
