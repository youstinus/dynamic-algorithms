namespace _2AcyclicGraph.Topological
{
    public class Item
    {
        public string Name { get; private set; }
        public Item[] Dependencies { get; private set; }

        public Item(string name, params Item[] dependencies)
        {
            Name = name;
            Dependencies = dependencies;
        }
    }

}
