using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Microsoft.AspNetCore.Mvc;
using System;
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
                var json = "{\"data\":{\"nodes\":{\"_data\":{\"1\":{\"id\":1,\"label\":\"1\"},\"2\":{\"id\":2,\"label\":\"2\"},\"3\":{\"id\":3,\"label\":\"3\"},\"4\":{\"id\":4,\"label\":\"4\"},\"5\":{\"id\":5,\"label\":\"5\"},\"6\":{\"id\":6,\"x\":-183.99334464608938,\"y\":55.44433981064223,\"label\":\"6\"}},\"length\":6,\"_idProp\":\"id\"},\"edges\":{\"_data\":{\"1\":{\"from\":1,\"to\":3,\"id\":1},\"2\":{\"from\":1,\"to\":2,\"id\":2},\"3\":{\"from\":2,\"to\":4,\"id\":3},\"4\":{\"from\":2,\"to\":5,\"id\":4},\"6\":{\"from\":6,\"to\":3,\"label\":\"\",\"id\":6},\"7\":{\"from\":5,\"to\":6,\"label\":\"\",\"id\":7}},\"length\":6,\"_idProp\":\"id\"}},\"options\":{\"locale\":\"pt-br\",\"manipulation\":{\"enabled\":false},\"edges\":{\"font\":{\"color\":\"#ffffff\",\"strokeWidth\":0,\"size\":18}},\"nodes\":{\"color\":{\"border\":\"#698B69\",\"background\":\"#458B74\",\"highlight\":{\"border\":\"#698B69\",\"background\":\"#4f6e4f\"}},\"font\":{\"color\":\"white\"}},\"physics\":{\"enabled\":true,\"forceAtlas2Based\":{\"gravitationalConstant\":-50,\"centralGravity\":0.01,\"springConstant\":0.02,\"springLength\":100,\"damping\":0.4,\"avoidOverlap\":0},\"maxVelocity\":50,\"minVelocity\":0.1,\"solver\":\"forceAtlas2Based\",\"stabilization\":{\"enabled\":true,\"iterations\":1000,\"updateInterval\":100,\"onlyDynamicEdges\":false,\"fit\":true},\"timestep\":0.5,\"adaptiveTimestep\":true}},\"ponderado\":false,\"ordenado\":false}";
                var obj = JsonSerializer.Deserialize<JsonGraph>(json);
                return Ok(obj);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
