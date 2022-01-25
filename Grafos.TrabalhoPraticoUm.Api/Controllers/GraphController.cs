using Grafos.TrabalhoPraticoUm.Borders.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public ActionResult<int> ReturnOrder()
        {
            try
            {
                var response = graphService.ReturnOrder();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("size")]
        public ActionResult<int> ReturnSize()
        {
            try
            {
                var response = graphService.ReturnSize();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("density")]
        public ActionResult<double> ReturnDensity()
        {
            try
            {
                var response = graphService.ReturnDensity();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("neighborhood/{node}")]
        public ActionResult<int> ReturnNeighborhood([FromRoute] int node)
        {
            try
            {
                var response = graphService.ReturnNeighborhood(node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("degree/{node}")]
        public ActionResult<int> ReturnDegree([FromRoute] int node)
        {
            try
            {
                var response = graphService.ReturnDegree(node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("isArticulation/{node}")]
        public ActionResult<bool> IsArticulation([FromRoute] int node)
        {
            try
            {
                var response = graphService.IsArticulation(node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
