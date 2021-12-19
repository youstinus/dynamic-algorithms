using System;
using System.Collections.Generic;
using _2AcyclicGraph.Directed;

namespace _2AcyclicGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAcyclicDirectedGraph();

            Console.ReadKey();
        }

        static void TestAcyclicDirectedGraph()
        {
            var s = 0;
            var t = 4;
            
            var dist = new int[6];
            var next = new int[6];
            InitializeLists(ref dist, ref next);
            var g = InitializeGraph();
            
            // dynamic algorithm
            g.LongestPathInDirectedAcyclicGraph(s, t);

            // recurrent algorithm
            SearchLongestPath(g, s, t, dist, next);

            Console.ReadKey();
        }

        static void PrintPath(int s, int t, IReadOnlyList<int> next)
        {
            var u = s;
            Console.Write("{0} ", u);
            while (u != t)
            {
                Console.Write("{0} ", next[u]);
                u = next[u];
            }
        }

static Graph InitializeGraph()
{
    var g = new Graph(6);
    g.Add(0, 1, 5);
    g.Add(0, 2, 3);
    g.Add(1, 3, 6);
    g.Add(1, 2, 2);
    g.Add(2, 4, 4);
    g.Add(2, 5, 2);
    g.Add(2, 3, 7);
    g.Add(3, 5, 1);
    g.Add(3, 4, -1);
    g.Add(4, 5, -2);
    return g;
}

        static void InitializeLists(ref int[] dist, ref int[] next)
        {
            for (var i = 0; i < 6; i++)
            {
                dist[i] = int.MinValue;
                next[i] = -1;
            }
        }

        static void SearchLongestPath(Graph g, int s, int t, int[] dist, int[] next)
        {
            g.LongestPathAux(s, t, ref dist, ref next);
            if (dist[s] == int.MinValue)
                Console.WriteLine("No path exists");
            else
            {
                Console.WriteLine("Longest path is {0}", dist[s]);
                PrintPath(s, t, next);
            }
        }
    }
}
