using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
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
        private readonly IMemoryService memoryService;
        public FileController(IFileService fileService, IMemoryService memoryService)
        {
            this.fileService = fileService;
            this.memoryService = memoryService;
        }

        /// <summary>
        /// Saves a file for continuous use of the program.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("save")]
        public async Task<ActionResult<string>> SaveFile([FromForm] FileRequest request)
        {
            try
            {
                if (request.File.ContentType == Constants.FileContent.JsonFormat)
                {
                    memoryService.Save(await fileService.ReadJson(request.File));
                    return Ok("[FileController][SaveFile] JSON file saved succesfully.");
                }
                else if (request.File.ContentType == Constants.FileContent.TxtFormat)
                {
                    memoryService.Save(await fileService.ReadTxt(request.File));
                    return Ok("[FileController][SaveFile] TXT file saved succesfully.");
                }
                else
                {
                    throw new InvalidContentTypeException("[FileController][SaveFile] Invalid content type.");
                }
            } catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
        /// <summary>
        /// Converts a file into another file of different format.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("convert")]
        public async Task<ActionResult<object>> ConvertFile([FromForm] FileRequest request)
        {
            try
            {
                object response = null;

                if (request.File.ContentType == Constants.FileContent.JsonFormat)
                {
                    var graph = await fileService.ReadJson(request.File);
                    response = fileService.ConvertFromJson(graph);
                }
                else if (request.File.ContentType == Constants.FileContent.TxtFormat)
                {
                    var graph = await fileService.ReadTxt(request.File);
                    response = fileService.ConvertFromTxt(graph);
                }
                else
                {
                    throw new InvalidContentTypeException("[FileController][ConvertFile] Invalid content type.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
