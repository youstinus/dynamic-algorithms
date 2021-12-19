using System;
using System.Collections.Generic;
using System.Linq;

namespace _2AcyclicGraph.Directed
{
 public class Graph
 {
     private int VerticesCount { get; }
     private List<VertexNode>[] Vertices { get; }

     public Graph(int verticesCount)
     {
         VerticesCount = verticesCount;
         Vertices = new List<VertexNode>[verticesCount];
         for (var i = 0; i < Vertices.Length; i++)
             Vertices[i] = new List<VertexNode>();
     }

     public void Add(int u, int v, int weight)
     {
         Vertices[u].Add(new VertexNode(v, weight));
     }

     private void SortTopologically(int vertex, IList<bool> visited, ICollection<int> sorted)
     {
         visited[vertex] = true;

         for (var i = 0; i < Vertices[vertex].Count; ++i)
         {
             var node = Vertices[vertex][i];

             if (!visited[node.Vertex])
                 SortTopologically(node.Vertex, visited, sorted);
         }

         sorted.Add(vertex);
     }

     public void LongestPathInDirectedAcyclicGraph(int s, int t)
     {
         var graph = new List<int>();
         var distance = new int[VerticesCount];
         var path = new int[VerticesCount];
         var visited = new bool[VerticesCount];

         for (var i = 0; i < VerticesCount; i++)
             if (visited[i] == false)
                 SortTopologically(i, visited, graph);

         for (var i = 0; i < VerticesCount; i++)
             distance[i] = int.MinValue;

         path = (int[]) distance.Clone();
         distance[s] = 0;

         graph.Reverse();

         foreach (var u in graph)
         {
             if (distance[u] == int.MinValue)
                 continue;

             for (var i = 0; i < Vertices[u].Count; ++i)
             {
                 if (distance[Vertices[u][i].Vertex] >= distance[u] + Vertices[u][i].Weight)
                     continue;

                 distance[Vertices[u][i].Vertex] = distance[u] + Vertices[u][i].Weight;
                 path[Vertices[u][i].Vertex] = u;
             }
         }

         /*Console.WriteLine("Longest path to other vertices:");
         for (var i = 0; i < VerticesCount; i++)
             Console.Write("{0} ", (distance[i] == int.MinValue) ? "-1" : distance[i].ToString());*/

         Console.WriteLine("\nLongest path from {0} to {1} is = {2}", s, t, distance[t]);
         Console.WriteLine("\nLongest path: ");
         
         Console.Write("{0} ", s);
         for (var i = s; i < VerticesCount; i++)
         {
             var index = path.ToList().IndexOf(i);
             if (index > 0)
             {
                 Console.Write("{0} ", index);
             }
         }
         Console.WriteLine();
     }

     public void LongestPathAux(int u, int t, ref int[] dist, ref int[] next)
     {
         if(u == t)
         {
             dist[u] = 0;
             return;
         }

         if (next[u] >= 0)
         {
             return;
         }

         foreach (var vertex in Vertices[u])
         {
             LongestPathAux(vertex.Vertex, t, ref dist, ref next);
             if (vertex.Weight + dist[vertex.Vertex] <= dist[u])
                 continue;

             dist[u] = vertex.Weight + dist[vertex.Vertex];
             next[u] = vertex.Vertex;
         }
     }
 }
}
