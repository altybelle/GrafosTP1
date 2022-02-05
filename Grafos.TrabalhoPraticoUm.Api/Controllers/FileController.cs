using Grafos.TrabalhoPraticoUm.Borders.Extensions;
using Grafos.TrabalhoPraticoUm.Borders.Graph;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;
        private readonly IMemoryService memoryService;
        public FileController(IFileService fileService, IMemoryService memoryService)
        {
            this.fileService = fileService;
            this.memoryService = memoryService;
        }
        /// <summary>
        /// Loads JSON or TXT file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("load")]
        public async Task<ActionResult<string>> LoadFile([FromForm] FileRequest request)
        {
            try
            {
                if (request.File.ContentType == Constants.FileContent.JsonFormat)
                {
                    memoryService.Save(await fileService.ReadJson(request.File));
                    return Ok("[FileController][SaveFile] JSON file loaded succesfully.");
                }
                else if (request.File.ContentType == Constants.FileContent.TxtFormat)
                {
                    memoryService.Save(await fileService.ReadTxt(request.File));
                    return Ok("[FileController][SaveFile] TXT file loaded succesfully.");
                }
                else
                {
                    throw new InvalidContentTypeException("[FileController][SaveFile] Invalid content type.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        /// <summary>
        /// Converts JSON to TXT and vice-versa.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPut("convert")]
        public ActionResult ConvertFile()
        {
            try
            {
                string fileName;
                string mimeType;

                var graph = memoryService.Load();

                if (graph.GetType() == typeof(FileGraph)) {
                    fileName = "response.json";
                    mimeType = "application/json";

                    var response = fileService.ConvertFromTxt((FileGraph)graph);
                    var newFile = JsonSerializer.SerializeToUtf8Bytes(response);

                    return new FileContentResult(newFile, mimeType)
                    {
                        FileDownloadName = fileName,
                    };
                } else if (graph.GetType() == typeof(JsonGraph))
                {
                    fileName = "response.txt";
                    mimeType = "plain/text";

                    var response = fileService.ConvertFromJson((JsonGraph)graph).ToFileString();
                   
                    return new FileContentResult(Encoding.ASCII.GetBytes(response), mimeType)
                    {
                        FileDownloadName = fileName,
                    };
                }
                else
                {
                    throw new InvalidContentTypeException("[FileController][ConvertFile] Invalid content type.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
