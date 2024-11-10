namespace GrantAssignment
{
    // ░██████╗░██╗░░░██╗███████╗░██████╗████████╗██╗░█████╗░███╗░░██╗  ░░███╗░░
    // ██╔═══██╗██║░░░██║██╔════╝██╔════╝╚══██╔══╝██║██╔══██╗████╗░██║  ░████║░░
    // ██║██╗██║██║░░░██║█████╗░░╚█████╗░░░░██║░░░██║██║░░██║██╔██╗██║  ██╔██║░░
    // ╚██████╔╝██║░░░██║██╔══╝░░░╚═══██╗░░░██║░░░██║██║░░██║██║╚████║  ╚═╝██║░░
    // ░╚═██╔═╝░╚██████╔╝███████╗██████╔╝░░░██║░░░██║╚█████╔╝██║░╚███║  ███████╗
    // ░░░╚═╝░░░░╚═════╝░╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝  ╚══════╝

    public class GNode
    {
        /*
         *Node will store the key/label and edges/ connected nodes as adjency list
         * */
        public int label { get; }
        public List<GEdge> neighbours = new List<GEdge>();
        public GNode(int key)
        {
            this.label = key;
        }

        public bool HasEdge(float weight, int neighbour)
        {
            return neighbours.Any(edge => ((edge.nodes.IsNeighbour(this)) && (edge.weight == weight)));
        }

        public void AddEdge(GEdge edge)
        {
            neighbours.Add(edge);
        }


    }
    public class EdgePair
    {
        // because graph is  undirected only need 1 edge to store relationship, instead of from and source nodes.
        public GNode node1;
        public GNode node2;

        public EdgePair(GNode node1, GNode node2)
        {
            this.node1 = node1;
            this.node2 = node2;
        }

        //Utility function to chech if current is a neighbour.
        public GNode Neighbour(GNode node)
        {
            if (node == node1)
                return node2;
            else if (node == node2)
                return node1;

            return null;
        }

        public bool IsNeighbour(GNode node)
        {
            return node == node1 || node == node2;
        }

    }

    public class GEdge : IComparable<GEdge>
    {
        public float weight { get; set; }
        public EdgePair nodes { get; }

        public GEdge(float weight, GNode node1, GNode node2)
        {
            this.weight = weight;
            this.nodes = new EdgePair(node1, node2);
        }

        public override string ToString()
        {
            return $"[{nodes.node1.label}]<-->[{nodes.node2.label}]";

        }

        public int CompareTo(GEdge rhs)
        {
            if (rhs == null) return 1;

            return weight.CompareTo(rhs.weight);

        }
    }


    public partial class Graph
    {

        public int Count
        {
            get
            {
                return graph.Count;
            }
        }

        //Utility used for testing in traversal
        public delegate void OnNodeVisited(GNode node);

        //use Dictionary so we can reference node by label
        Dictionary<int, GNode> graph = new Dictionary<int, GNode>();
        List<GEdge> allEdges = new List<GEdge>();

        public void AddNode(int label)
        {
            graph.Add(label, new GNode(label));

        }

        public bool HasEdge(float weight, int node1, int node2)
        {
            return graph[node1].HasEdge(weight, node2) && graph[node2].HasEdge(weight, node1);


        }

        public void AddEdge(float weight, int node1, int node2)
        {
            GEdge edge = new GEdge(weight, graph[node1], graph[node2]);
            allEdges.Add(edge);
            graph[node1].AddEdge(edge);
            graph[node2].AddEdge(edge);
        }


        public bool HasNode(int node)
        {
            return graph.ContainsKey(node);
        }

    }
}
