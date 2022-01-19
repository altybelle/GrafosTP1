using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Grafos.TrabalhoPraticoUm.Api.Controllers
{
    [Route("api/graph")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IGraphService graphService;
        public GraphController(IGraphService graphService)
        {
            this.graphService = graphService;
        }

        [HttpPut("order")]
        public async Task<ActionResult<int>> ReturnOrder([FromForm] FileRequest request)
        {
            try
            {
                var response = await graphService.ReturnOrder(request);
                return Ok(response);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("size")]
        public async Task<ActionResult<int>> ReturnSize([FromForm] FileRequest request)
        {
            try
            {
                var response = await graphService.ReturnSize(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("density")]
        public async Task<ActionResult<int>> ReturnDensity([FromForm] FileRequest request)
        {
            try
            {
                var response = await graphService.ReturnDensity(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("neighborhood")]
        public async Task<ActionResult<int>> ReturnNeighborhood([FromForm] FileRequest request, int node)
        {
            try
            {
                var response = await graphService.ReturnNeighborhood(request, node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("degree")]
        public async Task<ActionResult<int>> ReturnDegree([FromForm] FileRequest request, int node)
        {
            try
            {
                var response = await graphService.ReturnDegree(request, node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("isArticulation")]
        public async Task<ActionResult<bool>> IsArticulation([FromForm] FileRequest request, int node)
        {
            try
            {
                var response = await graphService.IsArticulation(request, node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
