
namespace GrantAssignment
{
    public partial class Graph
    {
        // ░██████╗░██╗░░░██╗███████╗░██████╗████████╗██╗░█████╗░███╗░░██╗  ██████╗░
        // ██╔═══██╗██║░░░██║██╔════╝██╔════╝╚══██╔══╝██║██╔══██╗████╗░██║  ╚════██╗
        // ██║██╗██║██║░░░██║█████╗░░╚█████╗░░░░██║░░░██║██║░░██║██╔██╗██║  ░░███╔═╝
        // ╚██████╔╝██║░░░██║██╔══╝░░░╚═══██╗░░░██║░░░██║██║░░██║██║╚████║  ██╔══╝░░
        // ░╚═██╔═╝░╚██████╔╝███████╗██████╔╝░░░██║░░░██║╚█████╔╝██║░╚███║  ███████╗
        // ░░░╚═╝░░░░╚═════╝░╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝  ╚══════╝
        //
        //QUESTION 2 BFS TRAVERSAL
        public void BFS(int labelNode, OnNodeVisited onVisit)
        {
            Queue<GNode> queue = new Queue<GNode>();

            //probably better to hash int over GNode
            HashSet<int> visited = new HashSet<int>();
            queue.Enqueue(graph[labelNode]);
            visited.Add(labelNode);

            while (queue.Count > 0)
            {
                GNode current = queue.Dequeue();

                foreach (var edge in current.neighbours)
                {
                    //because only 1 edge is stored we dont know which node of the pair to visit next so we get the neighbour of current
                    var neighbour = edge.nodes.Neighbour(current);
                    var label = neighbour.label;

                    if (!visited.Contains(label))
                    {
                        queue.Enqueue(neighbour);
                        visited.Add(label);
                    }
                }

                onVisit(current);
            }

        }

        //QUESTION 2 DFS TRAVERSAL
        public void DFS(int labelNode, OnNodeVisited onVisit)
        {
            Stack<GNode> stack = new Stack<GNode>();
            HashSet<int> visited = new HashSet<int>();
            stack.Push(graph[labelNode]);
            visited.Add(labelNode);

            while (stack.Count > 0)
            {
                GNode current = stack.Pop();

                // Want to visit left most nodes first so adding in reverse oder, so top of stack is the left
                for (int i = current.neighbours.Count - 1; i >= 0; i--)
                {
                    var edge = current.neighbours[i];
                    var neighbour = edge.nodes.Neighbour(current);

                    if (!visited.Contains(neighbour.label))
                    {
                        stack.Push(neighbour);
                        visited.Add(neighbour.label);
                    }
                }

                onVisit(current);
            }
        }

        //QUESTION 2 READ FROM FILE
        public void FromFile(string path)
        {
            //used to determine which line we are on in file
            int lineNumber = 0;

            List<string> edgesToCreate = new List<string>();

            graph.Clear();
            allEdges.Clear();

            int nodesToAdd = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string ln;
                while ((ln = sr.ReadLine()) != null)
                {


                    if (lineNumber == 0)
                    {
                        Int32.TryParse(ln, out nodesToAdd);
                    }
                    else if (lineNumber == 1)
                    {
                        int edgeCount = 0;
                        Int32.TryParse(ln, out edgeCount);
                        edgesToCreate.Capacity = edgeCount;
                    }
                    else
                    {
                        edgesToCreate.Add(ln);
                    }

                    lineNumber++;
                }
            }
            BuildNodes(1, nodesToAdd);
            BuildEdges(edgesToCreate);
        }

        private void BuildNodes(int minLable, int maxLable)
        {
            // Small optimization to avoid reallocation and add in range[min,max] inclusive
            graph.EnsureCapacity(maxLable - minLable + 1);
            for (; minLable <= maxLable; minLable++)
            {
                AddNode(minLable);
            }
        }
        private void BuildEdges(List<string> edges)
        {

            // Small optimization to avoid reallocation
            allEdges.EnsureCapacity(edges.Count);
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
