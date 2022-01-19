using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Api.Controllers
{
    [Route("api/graph")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IGraphVisualizerUseCase graphVisualizerUseCase;
        public GraphController(IGraphVisualizerUseCase graphVisualizerUseCase)
        {
            this.graphVisualizerUseCase = graphVisualizerUseCase;
        }

        [HttpPut("order")]
        public async Task<ActionResult<int>> RetornaVizinho([FromForm] FileRequest request, string nome)
        {
            try
            {
                return Ok(await graphVisualizerUseCase.Execute(request, nome));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
