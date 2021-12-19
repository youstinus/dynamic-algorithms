namespace _2AcyclicGraph.Directed
{
    public class VertexNode
    {
        public int Vertex { get; }
        public int Weight { get; }

        public VertexNode(int vertex, int weight)
        {
            Vertex = vertex;
            Weight = weight;
        }
    }
}
