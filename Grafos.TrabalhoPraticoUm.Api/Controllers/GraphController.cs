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

        [HttpGet("order")]
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

        [HttpGet("size")]
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

        [HttpGet("density")]
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

        [HttpGet("neighborhood/{node}")]
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

        [HttpGet("degree/{node}")]
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

        [HttpGet("articulated/{node}")]
        public ActionResult<bool> IsArticulated([FromRoute] int node)
        {
            try
            {
                var response = graphService.IsArticulated(node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
