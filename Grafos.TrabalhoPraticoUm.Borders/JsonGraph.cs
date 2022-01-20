using System.Collections.Generic;
using System.Text.Json.Serialization;
using static Grafos.TrabalhoPraticoUm.Shared.Constants;

namespace Grafos.TrabalhoPraticoUm.Borders
{
    public class JsonGraph
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
        [JsonPropertyName("options")]
        public JsonGraphOptions Options { get; set; } = default;
        [JsonPropertyName("ponderado")]
        public bool Ponderado { get; set; } = true;
        [JsonPropertyName("ordenado")]
        public bool Ordenado { get; set; }

    }

    public class Data
    {
        [JsonPropertyName("nodes")]
        public DataNodes Nodes { get; set; }
        [JsonPropertyName("edges")]
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
                [JsonPropertyName("id")]
                public int Id { get; set; }
                [JsonPropertyName("x")]
                public double? X { get; set; }
                [JsonPropertyName("y")]
                public double? Y { get; set; }
                [JsonPropertyName("label")]
                public string Label { get; set; }
                
            }
        }
        public class DataEdges
        {
            [JsonPropertyName("_data")]
            public Dictionary<string, EdgeData> Data { get; set; }
            [JsonPropertyName("length")]
            public int Length { get; set; } = 6;
            [JsonPropertyName("_idProp")]
            public string IdProp { get; set; } = "id";
            public class EdgeData
            {
                [JsonPropertyName("from")]
                public int From { get; set; }
                [JsonPropertyName("to")]
                public int To { get; set; }
                [JsonPropertyName("label")]
                public string Label { get; set; }
                [JsonPropertyName("id")]
                public int Id { get; set; }
            }
        }
    }
}
