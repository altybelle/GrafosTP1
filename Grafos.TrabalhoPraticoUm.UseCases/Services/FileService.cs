using Grafos.TrabalhoPraticoUm.Borders.Extensions;
using Grafos.TrabalhoPraticoUm.Borders.Graph;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Borders.Solutions;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Grafos.TrabalhoPraticoUm.Borders.Graph.Data;
using static Grafos.TrabalhoPraticoUm.Borders.Graph.Data.DataEdges;
using static Grafos.TrabalhoPraticoUm.Borders.Graph.Data.DataNodes;
using static Grafos.TrabalhoPraticoUm.Shared.Constants;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Edges;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Nodes;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Nodes.NodeColor;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Physics;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class FileService : IFileService
    {
        public FileGraph ConvertFromJson(JsonGraph graph)
        {
            int nodes = graph.Data.Nodes.Length;
            int edges = graph.Data.Edges.Length;

            var obj = new FileGraph
            {
                Nodes = nodes,
                Edges = edges,
                Connections = new float[nodes + 1, nodes + 1]
            };

            for (int i = 0; i <= nodes; i++)
            {
                for (int j = 0; j <= nodes; j++)
                {
                    obj.Connections[i, j] = float.MaxValue;
                }
            }

            var dict = graph.Data.Edges.Data;

            foreach (string key in dict.Keys)
            {
                int index1 = dict[key].To;
                int index2 = dict[key].From;
                float value = float.Parse(dict[key].Label, CultureInfo.InvariantCulture);

                if (graph.Ordenado)
                {
                    obj.Connections[index1, index2] = value;
                }
                else
                {
                    obj.Connections[index1, index2] = value;
                    obj.Connections[index2, index1] = value;
                }
            }

            return obj;
        }

        public JsonGraph ConvertFromTxt(FileGraph graph)
        {
            int nodes = graph.Nodes;
            int edges = graph.Edges;

            return GenerateJsonGraph(graph.Connections, nodes, edges);
        }

        public async Task<FileGraph> ReadTxt(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                throw new FileIsNullOrEmptyException("[FileService][ReadTxt] The file is null or empty.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            string txt = Encoding.UTF8.GetString(ms.ToArray()).Replace("\r", string.Empty);
            string[] data = txt.Split("\n");

            bool isDirected = int.Parse(data[0]) == 1 ? true : false;
            int nodeAmount = int.Parse(data[1]);
            int edgeAmount = data.Length - 2;

            var obj = new FileGraph
            {
                Directed = isDirected,
                Nodes = nodeAmount,
                Edges = edgeAmount,
                Connections = new float[nodeAmount + 1, nodeAmount + 1]
            };

            for (int i = 0; i <= nodeAmount; i++)
            {
                for (int j = 0; j <= nodeAmount; j++)
                {
                    obj.Connections[i, j] = float.MaxValue;
                }
            }

            for (int i = 2; i <= edgeAmount; i++)
            {
                string[] connections = data[i].Split(" ");

                int index1 = int.Parse(connections[0]);
                int index2 = int.Parse(connections[1]);
                float value = float.Parse(connections[2], CultureInfo.InvariantCulture);

                if (obj.Directed)
                {
                    obj.Connections[index1, index2] = value;
                }
                else
                {
                    obj.Connections[index1, index2] = value;
                    obj.Connections[index2, index1] = value;
                }
            }

            return obj;
        }

        public async Task<JsonGraph> ReadJson(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                throw new FileIsNullOrEmptyException("[FileService][ReadJson] The file is null or empty.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            string json = Encoding.UTF8.GetString(ms.ToArray());
            return JsonSerializer.Deserialize<JsonGraph>(json);
        }
        public (byte[], string, string) GenerateConvertedFile(IGraph graph)
        {
            string fileName;
            string mimeType;

            if (graph.GetType() == typeof(FileGraph))
            {
                fileName = "response.json";
                mimeType = "application/json";

                var response = ConvertFromTxt((FileGraph)graph);
                var newFile = JsonSerializer.SerializeToUtf8Bytes(response);

                return (newFile, mimeType, fileName);
            }
            else if (graph.GetType() == typeof(JsonGraph))
            {
                fileName = "response.txt";
                mimeType = "plain/text";

                var newFile = ConvertFromJson((JsonGraph)graph).ToFileString();

                return (Encoding.ASCII.GetBytes(newFile), mimeType, fileName);
            }
            else
            {
                throw new InvalidContentTypeException("[FileController][ConvertFile] Invalid content type.");
            }
        }

        public (byte[], string, string) GenerateKruskalFile(Kruskal kruskal)
        {
            var fileName = "kruskal.txt";
            var mimeType = "plain/text";

            int max = 0;
            foreach (string item in kruskal.Tree.Values)
            {
                var items = item.Split(' ');
                if (max < int.Parse(items[0]) || max < int.Parse(items[1]))
                    max = int.Parse(items[0]) > int.Parse(items[1]) ? int.Parse(items[0]) : int.Parse(items[1]);
            }

            var result = string.Empty;
            result += $"{max}\n";

            foreach (string item in kruskal.Tree.Values)
            {
                result += $"{item}\n";
            }

            result += $"Min cost: {kruskal.MinCost.ToString(CultureInfo.InvariantCulture)}";

            return (Encoding.ASCII.GetBytes(result), mimeType, fileName);
        }
        internal static JsonGraph GenerateJsonGraph(float[,] connections, int nodes, int edges)
        {
            return new JsonGraph
            {
                Data = new Data
                {
                    Edges = new DataEdges
                    {
                        Data = GenerateEdges(connections, nodes),
                        Length = edges
                    },
                    Nodes = new DataNodes
                    {
                        Data = GenerateNodes(nodes),
                        Length = nodes
                    }
                },
                Options = new JsonGraphOptions
                {
                    Edges = new Edges
                    {
                        Font = new EdgeFont()
                    },
                    Nodes = new Nodes
                    {
                        Color = new NodeColor
                        {
                            Highlight = new NodeHighlight()
                        },
                        Font = new NodeFont(),
                    },
                    Physics = new Physics
                    {
                        ForceAtlas2Based = new PhysicsForceAtlas2Based(),
                        Stabilization = new PhysicsStabilization()
                    },
                    Manipulation = new Manipulation()
                }
            };
        }
        internal static Dictionary<string, NodeData> GenerateNodes(int nodes)
        {
            var dict = new Dictionary<string, NodeData>();

            for (int i = 1; i <= nodes; i++)
            {
                dict[i.ToString()] = new NodeData
                {
                    Label = i.ToString(),
                    Id = i
                };
            }

            return dict;
        }

        internal static Dictionary<string, EdgeData> GenerateEdges(float[,] connections, int nodes)
        {
            var dict = new Dictionary<string, EdgeData>();

            int id = nodes;
            for (int i = 1; i <= nodes; i++)
            {
                for (int j = i + 1; j <= nodes; j++)
                {
                    if (connections[i, j] != float.MaxValue)
                    {
                        id++;
                        dict[id.ToString()] = new EdgeData
                        {
                            From = i,
                            To = j,
                            Label = connections[i, j].ToString(CultureInfo.InvariantCulture),
                            Id = id.ToString()
                        };
                    }
                }
            }

            return dict;
        }
    }
}
