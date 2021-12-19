using System;
using System.Collections.Generic;
using System.Linq;
using _2AcyclicGraph.Models;
using _2AcyclicGraph.Topological;

namespace _2AcyclicGraph.Stash
{
    class Stash
    {
        static void TopologicalOrder()
        {

        }

        static void LongestPath(List<int> vertices)
        {
            var dist = new int[vertices.Count];
            foreach (var vertex in vertices)
            {

            }
        }

        static SimpleDirectedGraph<int, int> Method2()
        {
            var grap = new SimpleDirectedGraph<int, int>();
            grap.AddEdge(1, 2, 3);
            grap.AddEdge(1, 4, 5);
            grap.AddEdge(2, 6, 4);
            grap.AddEdge(2, 3, 7);
            grap.AddEdge(2, 5, 2);
            grap.AddEdge(3, 5, 1);
            grap.AddEdge(3, 6, -1);
            grap.AddEdge(4, 2, 2);
            grap.AddEdge(6, 5, -2);
            return grap;
        }

        static void Topo()
        {
            var a = new Item("A");
            var c = new Item("C");
            var f = new Item("F");
            var h = new Item("H");
            var d = new Item("D", a);
            var g = new Item("G", f, h);
            var e = new Item("E", d, g);
            var b = new Item("B", c, e);

            var unsorted = new[] { a, b, c, d, e, f, g, h };

            var sorted = TopoSort.Sort(unsorted, x => x.Dependencies);
            foreach (var item in sorted)
            {
                Console.Write("{0} ", item.Name);
            }
        }






        //====================================

        /* static Tuple<int[], int[]> LONGESTPATHAUS(int[][] G, int u, int t, int[] dist, int[] next)
         {
             if (u == t)
             {
                 dist[u] = 0;
                 return Tuple.Create(dist, next);
             }

             if (next[u] >= 0)
             {
                 return Tuple.Create(dist, next);
             }

             next[u] = 0;
             foreach (var i in G[u])
             {
                 (dist, next) = LONGESTPATHAUS(G, G[u][i], t, dist, next);
                 if (w(u, G[u][i]) + dist[G[u][i]] > dist[u])
                 {
                     dist[u] = w(u, G[u][i]) + dist[G[u][i]];
                     next[u] = G[u][i];
                 }
             }

             return Tuple.Create(dist, next);
         }*/

        static List<List<long>> FloydWarshal(List<List<long>> graph)
        {
            var d = new List<List<long>>();//(graph.size(), List < long, long > (graph.size()));

            //Initialize d
            for (int i = 0; i < graph.Count; ++i)
            {
                for (int j = 0; j < graph[i].Count(); ++j)
                {
                    d[i][j] = graph[i][j];
                }
            }

            for (int i = 0; i < graph.Count(); ++i)
                for (int j = 0; j < graph.Count(); ++j)
                    for (int k = 0; k < graph.Count(); k++)
                        d[i][j] = Math.Min(d[i][j], d[i][k] + d[k][j]);

            return d;
        }

        // Topological Sort Using DFS
        static List<int> topologicalSortDFS(List<List<int>> graph)
        {
            List<int> result = new List<int>();

            // map of node to 0: not visited, 1: being visited, 2: visited
            Dictionary<int, int> visited = new Dictionary<int, int>();
            List<int> unvisited = new List<int>();

            for (int i = 0; i < graph.Count(); i++)
            {
                unvisited.Add(i);
            }
            // While there is unvisited nodes
            while (unvisited.Any())
            {
                DFS(graph, visited, unvisited, result, (unvisited.First()));
            }

            result.Reverse();
            return result;
        }
        //-----------------------------------------------------
        // Recursively visits nodes
        // If all children of a node is visited, adds it to sorted
        // Marks nodes 0: not visited, 1: being visited, 2: visited
        static void DFS(List<List<int>> graph, Dictionary<int, int> visited, List<int> unvisited, List<int> sorted, int n)
        {
            if (visited[n] == 1)
            {
                Console.WriteLine("Detected cycle!");
                return;
            }

            if (visited[n] == 2)
            {
                return;
            }

            visited[n] = 1;

            for (int j = 0; j < graph.Count(); j++)
            {
                if (j != n && graph[n][j] != int.MaxValue)
                {
                    DFS(graph, visited, unvisited, sorted, j);
                }
            }
            unvisited.Remove(n);
            visited[n] = 2;
            sorted.Add(n);
        }

        // Shortest path using topological sort
        static List<long> ShortestPathTopo(List<List<int>> graph, int source)
        {
            List<long> d = new List<long>();//;(graph.size(), int.MaxValue);
            d[source] = 0;
            List<int> sorted = topologicalSortDFS(graph);
            foreach (var n in sorted)
            {
                for (int j = 0; j < graph.Count(); j++)
                {
                    // Relax outgoing edges of n
                    if (graph[n][j] != int.MaxValue)
                    {
                        d[j] = Math.Min(d[j], d[n] + graph[n][j]);
                    }
                }
            }
            return d;
        }
    }
}
