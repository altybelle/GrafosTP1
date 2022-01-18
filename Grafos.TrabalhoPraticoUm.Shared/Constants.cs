using System;

namespace Grafos.TrabalhoPraticoUm.Shared
{
    public class Constants
    {
        public class JsonGraphOptions
        {
            public string Locale { get; set; } = "pt_br";
            public Manipulation Manipulation { get; set; } = default;
            public Edges Edges { get; set; } = default;
            public Nodes Nodes { get; set; } = default;

        }
        public class Manipulation
        {
            public bool Enabled { get; set; } = false;
        }

        public class Edges
        {
            public EdgeFont Font { get; set; } = default;
            public class EdgeFont
            {
                public string Color { get; set; } = "#ffffff";
                public int StrokeWidth { get; set; } = 0;
                public int Size { get; set; } = 18;
            }
        }

        public class Nodes
        {
            public NodeColor Color { get; set; } = default;
            public NodeFont Font { get; set; } = default;
            public class NodeColor
            {
                public  string Border { get; set; } = "#698B69";
                public  string Background { get; set; } = "#458B74";
                public NodeHighlight Highlight { get; set; } = default;
                public class NodeHighlight
                {
                    public string Border { get; set; } = "#698B69";
                    public string Background { get; set; } = "#4f6e4f";
                }
            }

            public class NodeFont
            {
                public string Color { get; set; } = "white";
            }
        }
        public class Physics
        {
            public bool Enabled { get; set; } = true;
            public PhysicsForceAtlas2Based ForceAtlas2Based { get; set; } = default;
            public int MaxVelocity { get; set; } = 50;
            public double MinVelocity { get; set; } = 0.1;
            public string Solver { get; set; } = "forceAtlas2Based";
            public PhysicsStabilization Stabilization { get; set; }
            public double Timestep { get; set; } = 0.5;
            public bool AdaptativeTimestep { get; set; } = true;

            public class PhysicsForceAtlas2Based
            {
                public int GravitationalConstant { get; set; } = -50;
                public double CentralGravity { get; set; } = 0.01;
                public double SpringConstant { get; set; } = 0.02;
                public int SpringLength { get; set; } = 100;
                public double Damping { get; set; } = 0.4;
                public int AvoidOverlap { get; set; } = 0;
            }
            
            public class PhysicsStabilization
            {
                public bool Enabled { get; set; } = true;
                public int Iterations { get; set; } = 1000;
                public int UpdateInterval { get; set; } = 100;
                public bool OnlyDynamicEdges { get; set; } = false;
                public bool Fit { get; set; } = true;
            }
        }
    }
}
