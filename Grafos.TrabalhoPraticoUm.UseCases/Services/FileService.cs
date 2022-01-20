using Grafos.TrabalhoPraticoUm.Borders;
using Grafos.TrabalhoPraticoUm.Borders.Request;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Grafos.TrabalhoPraticoUm.Borders.Data;
using static Grafos.TrabalhoPraticoUm.Borders.Data.DataEdges;
using static Grafos.TrabalhoPraticoUm.Borders.Data.DataNodes;
using static Grafos.TrabalhoPraticoUm.Shared.Constants;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Edges;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Nodes;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Nodes.NodeColor;
using static Grafos.TrabalhoPraticoUm.Shared.Constants.Physics;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class FileService : IFileService
    {
        public async Task<FileGraph> ConvertFromJson(FileRequest request)
        {
            var json = await ReadJson(request.File);

            int nodes = json.Data.Nodes.Length;

            var obj = new FileGraph
            {
                Nodes = nodes,
                Connections = new float[nodes + 1, nodes + 1]
            };

            for (int i = 0; i <= nodes; i++)
            {
                for (int j = 0; j <= nodes; j++)
                {
                    obj.Connections[i, j] = float.MaxValue;
                }
            }

            var dict = json.Data.Edges.Data;


            foreach (string key in dict.Keys)
            {
                int index1 = dict[key].To;
                int index2 = dict[key].From;
                float value = float.Parse(dict[key].Label, CultureInfo.InvariantCulture);

                obj.Connections[index1, index2] = value;
                obj.Connections[index2, index1] = value;
            }

            return obj;
        }

        public async Task<JsonGraph> ConvertFromTxt(FileRequest request)
        {
            var txt = await ReadTxt(request.File);

            int nodes = txt.Nodes;
            int edges = txt.Edges;

            return GenerateJsonGraph(txt.Connections, nodes, edges);
        }

        public async Task<FileGraph> ReadTxt(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                throw new FileIsNullOrEmptyException("[FileService][ReadTxt] The file is null or empty.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            string txt = Encoding.UTF8.GetString(ms.ToArray()).Replace("\r", string.Empty);
            string[] data = txt.Split("\n");

            int edgeAmount = int.Parse(data[0]);

            var obj = new FileGraph
            {
                Nodes = edgeAmount,
                Connections = new float[edgeAmount + 1, edgeAmount + 1]
            };
            for (int i = 0; i <= edgeAmount; i++)
            {
                for(int j = 0; j <= edgeAmount; j++)
                {
                    obj.Connections[i, j] = float.MaxValue;
                }                    
            }
            int edges = 0;
            for (int i = 1; i < data.Length; i++)
            {
                string[] connections = data[i].Split(" ");

                int index1 = int.Parse(connections[0]);
                int index2 = int.Parse(connections[1]);
                float value = float.Parse(connections[2], CultureInfo.InvariantCulture);

                obj.Connections[index1, index2] = value;
                obj.Connections[index2, index1] = value;

                edges++;
            }


            obj.Edges = edges;

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
        internal JsonGraph GenerateJsonGraph(float[,] connections, int nodes, int edges)
        {
            return new JsonGraph
            {
                Data = new Data
                {
                    Edges = new DataEdges
                    {
                        Data = GenerateEdges(connections, nodes, edges),
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
        internal  Dictionary<string, NodeData> GenerateNodes(int nodes)
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

        internal Dictionary<string, EdgeData> GenerateEdges(float[,] connections, int nodes, int edges)
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
                            Id = id
                        };
                    }
                }
            }

            return dict;
        }
    }
}
