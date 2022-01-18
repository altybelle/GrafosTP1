using Microsoft.AspNetCore.Http;

namespace Grafos.TrabalhoPraticoUm.Borders.Request
{
    public class FileRequest
    {
        public IFormFile File { get; set; }
    }
}
