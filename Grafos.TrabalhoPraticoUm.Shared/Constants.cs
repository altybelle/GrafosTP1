using System.Text.Json.Serialization;

namespace Grafos.TrabalhoPraticoUm.Shared
{
    public class Constants
    {
        public class FileContent
        {
            public static readonly string JsonFormat = "application/json";
            public static readonly string TxtFormat = "text/plain";
        }

        public class JsonGraphOptions
        {
            [JsonPropertyName("locale")]
            public string Locale { get; set; } = "pt_br";
            [JsonPropertyName("manipulation")]
            public Manipulation Manipulation { get; set; } = default;
            [JsonPropertyName("edges")]
            public Edges Edges { get; set; } = default;
            [JsonPropertyName("nodes")]
            public Nodes Nodes { get; set; } = default;
            [JsonPropertyName("physics")]
            public Physics Physics { get; set; } = default;

        }
        public class Manipulation
        {
            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; } = false;
        }

        public class Edges
        {
            [JsonPropertyName("font")]
            public EdgeFont Font { get; set; } = default;
            public class EdgeFont
            {
                [JsonPropertyName("color")]
                public string Color { get; set; } = "#ffffff";
                [JsonPropertyName("strokeWidth")]
                public int StrokeWidth { get; set; } = 0;
                [JsonPropertyName("size")]
                public int Size { get; set; } = 18;
            }
        }

        public class Nodes
        {
            [JsonPropertyName("color")]
            public NodeColor Color { get; set; } = default;
            [JsonPropertyName("font")]
            public NodeFont Font { get; set; } = default;
            public class NodeColor
            {
                [JsonPropertyName("border")]
                public  string Border { get; set; } = "#698B69";
                [JsonPropertyName("background")]
                public  string Background { get; set; } = "#458B74";
                [JsonPropertyName("highlight")]
                public NodeHighlight Highlight { get; set; } = default;
                public class NodeHighlight
                {
                    [JsonPropertyName("border")]
                    public string Border { get; set; } = "#698B69";
                    [JsonPropertyName("background")]
                    public string Background { get; set; } = "#4f6e4f";
                }
            }

            public class NodeFont
            {
                [JsonPropertyName("color")]
                public string Color { get; set; } = "white";
            }
        }
        public class Physics
        {
            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; } = true;
            [JsonPropertyName("forceAtlas2Based")]
            public PhysicsForceAtlas2Based ForceAtlas2Based { get; set; } = default;
            [JsonPropertyName("maxVelocity")]
            public int MaxVelocity { get; set; } = 50;
            [JsonPropertyName("minVelocity")]
            public double MinVelocity { get; set; } = 0.1;
            [JsonPropertyName("solver")]
            public string Solver { get; set; } = "forceAtlas2Based";
            [JsonPropertyName("stabilization")]
            public PhysicsStabilization Stabilization { get; set; }
            [JsonPropertyName("timestep")]
            public double Timestep { get; set; } = 0.5;
            [JsonPropertyName("adaptativeTimestep")]
            public bool AdaptativeTimestep { get; set; } = true;

            public class PhysicsForceAtlas2Based
            {
                [JsonPropertyName("gravitationalConstant")]
                public int GravitationalConstant { get; set; } = -50;
                [JsonPropertyName("centralGravity")]
                public double CentralGravity { get; set; } = 0.01;
                [JsonPropertyName("springConstant")]
                public double SpringConstant { get; set; } = 0.02;
                [JsonPropertyName("springLength")]
                public int SpringLength { get; set; } = 100;
                [JsonPropertyName("damping")]
                public double Damping { get; set; } = 0.4;
                [JsonPropertyName("avoidOverlap")]
                public int AvoidOverlap { get; set; } = 0;
            }
            
            public class PhysicsStabilization
            {
                [JsonPropertyName("enabled")]
                public bool Enabled { get; set; } = true;
                [JsonPropertyName("iterations")]
                public int Iterations { get; set; } = 1000;
                [JsonPropertyName("updateInterval")]
                public int UpdateInterval { get; set; } = 100;
                [JsonPropertyName("onlyDynamicEdges")]
                public bool OnlyDynamicEdges { get; set; } = false;
                [JsonPropertyName("fit")]
                public bool Fit { get; set; } = true;
            }
        }
    }
}
