using Grafos.TrabalhoPraticoUm.Borders.Graph;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grafos.TrabalhoPraticoUm.UseCases.Services
{
    public class GraphService : IGraphService
    {
        private readonly IFileService fileService;
        private readonly IMemoryService memoryService;
        public GraphService(IFileService fileService, IMemoryService memoryService)
        {
            this.fileService = fileService;
            this.memoryService = memoryService;
        }

        public bool IsArticulated(int node)
        {
            var graph = CreateGraph();

            if (node > graph.Nodes)
                throw new IndexOutOfRangeException($"[GraphService][IsArticulated] Index out of bounds. Current amount of nodes: {graph.Nodes}.");

            for (int i = 1; i <= graph.Nodes; i++)
            {
                if (i != node)
                {
                    if (!(ReturnNeighborhood(i)).Any(s => s != node))
                        return false;
                }
            }

            return true;
        }

        public int ReturnDegree(int node)
        {
            var graph = CreateGraph();

            if (node > graph.Nodes)
                throw new IndexOutOfRangeException($"[GraphService][ReturnDegree] Index out of bounds. Current amount of nodes: {graph.Nodes}.");

            int degree = 0;

            for (int i = 1; i <= graph.Nodes; i++)
            {
                if (graph.Connections[node, i] != float.MaxValue)
                {
                    degree++;
                }
            }

            if (graph.Connections[node, node] != float.MaxValue)
                degree++;

            return degree;
        }

        public float ReturnDensity()
        {
            var graph = CreateGraph();

            float density = Math.Abs((float)graph.Edges) / Math.Abs((float)graph.Nodes);

            return density;
        }

        public IEnumerable<int> ReturnNeighborhood(int node)
        {
            var graph = CreateGraph();

            if (node > graph.Nodes)
                throw new IndexOutOfRangeException($"[GraphService][ReturnNeighborhood] Index out of bounds. Current amount of nodes: {graph.Nodes}.");

            var neighbors = new List<int>();

            for (int i = 1; i <= graph.Nodes; i++)
            {
                if (graph.Connections[node, i] != float.MaxValue)
                    neighbors.Add(i);
            }

            return neighbors;
        }

        public int ReturnOrder()
        {
            var graph = CreateGraph();
            return graph.Nodes;
        }

        public int ReturnSize()
        {
            var graph = CreateGraph();
            return graph.Edges;
        }

        internal FileGraph CreateGraph()
        {
            var graph = memoryService.Load();

            if (graph == null)
                throw new NoFileWasLoadedException("[GraphService][CreateGraph] No file was loaded.");

            if (graph.GetType() == typeof(FileGraph))
            {
                return (FileGraph)graph;
            }
            else
            {
                return fileService.ConvertFromJson((JsonGraph)graph);
            }
        }
    }
}
