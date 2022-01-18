using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpPut("convert")]
        public async Task<ActionResult<JsonGraph>> ConvertFile([FromForm] FileRequest request)
        {
            try {
                JsonGraph response = null;

                if (request.File.ContentType == Constants.FileContent.JsonFormat)
                {
                    response = await fileService.ConvertFromJson(request);
                } else if (request.File.ContentType == Constants.FileContent.TxtFormat)
                {
                    response = await fileService.ConvertFromTxt(request);
                }

                return Ok(response);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
