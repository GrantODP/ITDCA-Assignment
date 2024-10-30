using System.Collections.Generic;

namespace GrantAssignment
{
    class GNode
    {
        public int label { get; }
        public List<GEdge> neighbours = new List<GEdge>();
        public GNode(int key)
        {
            this.label = key;
        }

        public bool HasEdge(float weight, int neighbour)
        {
            return neighbours.Any(edge => (edge.neighbour.label == neighbour) && (edge.weight == weight));
        }

        public void AddEdge(GEdge edge)
        {
            neighbours.Add(edge);
        }


    }

    class GEdge
    {
        public float weight { get; set; }
        public GNode neighbour { get; }

        public GEdge(float weight, GNode neighbour)
        {
            this.weight = weight;
            this.neighbour = neighbour;
        }
    }

    public interface IOnNodeVisited<T>
    {
        void visited(T node);
    }

    class Graph
    {

        Dictionary<int, GNode> graph = new Dictionary<int, GNode>();


        public void AddNode(int label)
        {
            ThrowIfNodeDoesntExist(label);
            graph.Add(label, new GNode(label));

        }

        private void ThrowIfNodeDoesntExist(int label)
        {
            if (HasNode(label))
            {
                return;
            }
            throw new InvalidOperationException("Node already exists in graph");
        }

        public bool HasEdge(float weight, int node1, int node2)
        {
            ThrowIfNodeDoesntExist(node1);
            ThrowIfNodeDoesntExist(node2);

            return graph[node1].HasEdge(weight, node2) && graph[node2].HasEdge(weight, node1);


        }

        public void AddEdge(float weight, int node1, int node2)
        {
            GEdge ed1 = new GEdge(weight, graph[node2]);
            GEdge ed2 = new GEdge(weight, graph[node1]);

            graph[node1].AddEdge(ed1);
            graph[node2].AddEdge(ed2);
        }


        public bool HasNode(int node)
        {
            return graph.ContainsKey(node);
        }

        //QUESTION 2 BFS TRAVERSAL
        public void BFS(int labelNode, IOnNodeVisited<GNode> onNodeVisited)
        {
            ThrowIfNodeDoesntExist(labelNode);
            Queue<GNode> queue = new Queue<GNode>();
            HashSet<int> visited = new HashSet<int>();
            queue.Enqueue(graph[labelNode]);
            visited.Add(labelNode);

            while (queue.Count > 0)
            {
                GNode current = queue.Dequeue();

                foreach (var edge in current.neighbours)
                {
                    if (!visited.Contains(edge.neighbour.label))
                    {
                        queue.Enqueue(edge.neighbour);
                    }
                }

                onNodeVisited.visited(current);
            }

        }

        //QUESTION 2 DFS TRAVERSAL
        public void DFS(int labelNode, IOnNodeVisited<GNode> onNodeVisited)
        {
            ThrowIfNodeDoesntExist(labelNode);
            Stack<GNode> stack = new Stack<GNode>();
            HashSet<int> visited = new HashSet<int>();
            stack.Push(graph[labelNode]);
            visited.Add(labelNode);

            while (stack.Count > 0)
            {
                GNode current = stack.Pop();

                foreach (var edge in current.neighbours)
                {
                    if (!visited.Contains(edge.neighbour.label))
                    {
                        stack.Push(edge.neighbour);
                    }
                }

                onNodeVisited.visited(current);
            }
        }
    }
}
