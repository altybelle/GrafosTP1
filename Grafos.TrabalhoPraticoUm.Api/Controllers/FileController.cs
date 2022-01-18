using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Grafos.TrabalhoPraticoUm.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPut("convert")]
        public ActionResult<JsonGraph> ConvertFile([FromForm] FileRequest request)
        {
            try {
                
                JsonGraph obj = null;

                if (request.File != null && request.File.Length > 0)
                {
                    using var ms = new MemoryStream();
                    request.File.CopyTo(ms);
                    string s = Encoding.UTF8.GetString(ms.ToArray());
                    obj = JsonSerializer.Deserialize<JsonGraph>(s);
                }
                
                return Ok(obj);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
