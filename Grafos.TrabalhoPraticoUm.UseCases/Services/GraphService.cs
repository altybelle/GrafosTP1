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

            return Enumerable.Range(0, graph.Connections.GetLength(0))
                .Where(i => i != node && i > 0)
                .All(n => ReturnNeighborhood(n).Any(s => s != node));
        }

        public int ReturnDegree(int node)
        {
            var graph = CreateGraph();

            if (node > graph.Nodes)
                throw new IndexOutOfRangeException($"[GraphService][ReturnDegree] Index out of bounds. Current amount of nodes: {graph.Nodes}.");

            return Enumerable.Range(0, graph.Connections.GetLength(0))
                .Select(i => graph.Connections[i, node])
                .Where(i => i != float.MaxValue).Count();
        }

        public float ReturnDensity()
        {
            var graph = CreateGraph();
            return Math.Abs((float)graph.Edges) / Math.Abs((float)graph.Nodes);
        }

        public IEnumerable<int> ReturnNeighborhood(int node)
        {
            var graph = CreateGraph();

            if (node > graph.Nodes)
                throw new IndexOutOfRangeException($"[GraphService][ReturnNeighborhood] Index out of bounds. Current amount of nodes: {graph.Nodes}.");

            return Enumerable.Range(0, graph.Connections.GetLength(0))
                .Where(i => i > 0 && graph.Connections[node, i] != float.MaxValue);
        }

        public int ReturnOrder()
        {
            return CreateGraph().Nodes;
        }

        public int ReturnSize()
        {
            return CreateGraph().Edges;
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
