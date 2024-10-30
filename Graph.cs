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

        public int Count
        {
            get
            {
                return graph.Count;
            }
        }

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

        //QUESTION 2 READ FROM FILE
        public void FromFile(string path)
        {
            int counter = 0;
            List<string> edgesToCreate = new List<string>();
            int nodesToAdd = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {


                    if (counter == 0)
                    {
                        Int32.TryParse(ln, out nodesToAdd);
                    }
                    else if (counter == 1)
                    {
                        int edgeCount = 0;
                        Int32.TryParse(ln, out edgeCount);
                        edgesToCreate.Capacity = edgeCount;
                    }
                    else
                    {
                        edgesToCreate.Add(ln);
                    }

                    counter++;
                }
            }
            BuildNodes(1, nodesToAdd);
            BuildEdges(edgesToCreate);
        }

        private void BuildNodes(int minLable, int maxLable)
        {
            for (; minLable <= maxLable; minLable++)
            {
                AddNode(minLable);
            }
        }
        private void BuildEdges(List<string> edges)
        {

            foreach (var edge in edges)
            {
                int[] values = Array.ConvertAll(edge.Split(" "), Int32.Parse);

                if (values.Length != 3)
                    continue;

                AddEdge(values[2], values[0], values[1]);

            }
        }

    }
}
