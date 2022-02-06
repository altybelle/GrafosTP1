using Grafos.TrabalhoPraticoUm.Borders.Graph;
using Grafos.TrabalhoPraticoUm.Borders.Services;
using Grafos.TrabalhoPraticoUm.Borders.Solutions;
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

        public IEnumerable<string> BFS(int node)
        {
            var graph = CreateGraph();

            Queue<int> queue = new Queue<int>();
            List<float> visitedEdges = new List<float>();
            List<int> flagged = new List<int>();

            flagged.Add(node);
            queue.Enqueue(node);

            while (queue.Any())
            {
                int v = queue.Dequeue();
                for (int w = 1; w <= graph.Nodes; w++)
                {
                    if (!flagged.Contains(w) && graph.Connections[v, w] != float.MaxValue)
                    {
                        visitedEdges.Add(graph.Connections[v, w]);
                        queue.Enqueue(w);
                        flagged.Add(w);
                    }
                }
            }

            List<string> unvisitedEdges = new List<string>();
            for (int i = 1; i <= graph.Nodes; i++)
            {
                for (int j = i + 1; j <= graph.Nodes; j++)
                {
                    var item = graph.Connections[i, j];
                    if (!visitedEdges.Contains(item) && item != float.MaxValue)
                    {
                        unvisitedEdges.Add($"({i}, {j})");
                    }

                }
            }

            return unvisitedEdges;
        }

        public bool IsCyclic()
        {
            var graph = CreateGraph();
            List<bool> visited = new List<bool>();


            for (int i = 1; i <= graph.Nodes; i++)
                visited.Add(false);

            for (int i = 1; i <= graph.Nodes; i++)
                if (!visited[i])
                    if (CycleDetection(graph, i, visited, -1))
                        return true;

            return false;
        }

        private bool CycleDetection(FileGraph graph, int v, List<bool> visited, int parent)
        {
            visited[v] = true;

            for (int i = 1; i <= graph.Nodes; i++)
            {
                if (!visited[i])
                {
                    if (CycleDetection(graph, i, visited, v))
                        return true;
                }
                else if (i != parent)
                {
                    return true;
                }
            }
            return false;
        }
        public EulerianPath EulerianPath()
        {
            var graph = CreateGraph();

            EulerianPath euler = new EulerianPath(graph.Connections, graph.Nodes + 1);

            for (int i = 1; i <= graph.Nodes; i++)
            {
                if (ReturnDegree(i) % 2 != 0)
                    return euler;
            }

            euler.IsEulerian = true;
            euler.Fleury(1);

            return euler;
        }

        public Djikstra DistanceAndShortestPath(int node)
        {
            var graph = CreateGraph();

            Djikstra djikstra = new Djikstra(graph.Connections, graph.Nodes + 1);
            djikstra.Run(node);

            return djikstra;
        }
        public Kruskal KruskalsAlgorithm()
        {
            var graph = CreateGraph();

            var kruskal = new Kruskal();
            kruskal.KruskalMinimumSpanningTree(GraphAdapter(graph), graph.Nodes);

            return kruskal;
        }
        public RoyConnectedComponents RoysAlgorithm()
        {
            var graph = CreateGraph();

            var roys = new RoyConnectedComponents(graph.Connections, graph.Nodes);
            roys.Run();

            return roys;
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

        internal float[,] GraphAdapter(FileGraph graph)
        {
            float[,] connections = new float[graph.Nodes, graph.Nodes];
            for (int i = 0; i < graph.Nodes; i++)
            {
                for (int j = 0; j < graph.Nodes; j++)
                {
                    connections[i, j] = graph.Connections[i + 1, j + 1];
                }
            }
            return connections;
        }
    }
}
