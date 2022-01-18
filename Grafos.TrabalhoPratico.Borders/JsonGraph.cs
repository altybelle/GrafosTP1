using System.Collections.Generic;
using System.Text.Json.Serialization;
using static Grafos.TrabalhoPraticoUm.Shared.Constants;

namespace Grafos.TrabalhoPraticoUm.Borders
{
    public class JsonGraph
    {
        public Data Data { get; set; }
        public JsonGraphOptions Options { get; }
        public bool Ponderado { get; set; }
        public bool Ordenado { get; set; }

    }

    public class Data
    {
        public DataNodes Nodes { get; set; }
        public DataEdges Edges { get; set; }
        public class DataNodes
        {
            [JsonPropertyName("_data")]
            public Dictionary<string, NodeData> Data { get; set; }
            public int Length { get; set; } = 6;
            [JsonPropertyName("_idProp")]
            public string IdProp { get; set; } = "id";
            public class NodeData
            {
                public int Id { get; set; }
                public double? X { get; set; }
                public double? Y { get; set; }
                public string Label { get; set; }
                
            }
        }
        public class DataEdges
        {
            public int Length { get; set; } = 6;
            [JsonPropertyName("_idProp")]
            public string IdProp { get; set; } = "id";
            public Dictionary<string, EdgeData> Data { get; set; }
            public class EdgeData
            {
                public int From { get; set; }
                public int To { get; set; }
                public int Id { get; set; }
            }
        }
    }
}
