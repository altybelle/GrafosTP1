using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Borders.Solutions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Grafos.TrabalhoPraticoUm.Api.Controllers
{
    [Route("api/graph")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private readonly IGraphService graphService;
        private readonly IFileService fileService;
        public GraphController(IGraphService graphService, IFileService fileService)
        {
            this.graphService = graphService;
            this.fileService = fileService;
        }
        /// <summary>
        /// Returns the order of a ponderated graph.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns the size of a ponderated graph.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns the size of a ponderated graph.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns the neighborhood of a specific graph node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns the degree of a specific node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Discovers if a graph is articulated based on the removal of a specific node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Executes the Breadh-First Search given a specific node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        [HttpGet("bfs/{node}")]
        public ActionResult<IEnumerable<string>> BFS([FromRoute] int node)
        {
            try
            {
                var response = graphService.BFS(node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Verifies if the generated graph is cyclic.
        /// </summary>
        /// <returns></returns>
        [HttpGet("is_cyclic")]
        public ActionResult<bool> IsCyclic()
        {
            try
            {
                var response = graphService.IsCyclic();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Applies Fleury's algorithm to verify if the generated graph is Eulerian. Case not, returns empty values.
        /// </summary>
        /// <returns></returns>
        [HttpGet("eulerian_path")]
        public ActionResult<EulerianPath> EulerianPath()
        {
            try
            {
                var response = graphService.EulerianPath();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Applies Djikstra's algorithm based on a specified node and returns the minimum path and distance.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        [HttpGet("minimum_distance/{node}")]
        public ActionResult<Djikstra> DistanceAndShortestPath([FromRoute] int node)
        {
            try
            {
                var response = graphService.DistanceAndShortestPath(node);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Applies Kruskal's algorithm to generate a Minimum Spanning Tree and the minimum cost.
        /// </summary>
        /// <returns></returns>
        [HttpGet("minimum_spanning_tree/")]
        public ActionResult MinimumSpanningTree()
        {
            try
            {
                var response = graphService.KruskalsAlgorithm();
                var data = fileService.GenerateKruskalFile(response);
                return new FileContentResult(data.Item1, data.Item2)
                {
                    FileDownloadName = data.Item3
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Applies Roy's algorithm to discover connected components.
        /// </summary>
        /// <returns></returns>
        [HttpGet("connected_components/")]
        public ActionResult<RoyConnectedComponents> ConnectedComponents()
        {
            try
            {
                var response = graphService.RoysAlgorithm();
                return Ok(response);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
