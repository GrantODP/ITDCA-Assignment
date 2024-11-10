namespace GrantAssignment
{


    public partial class Graph
    {

        // ░██████╗░██╗░░░██╗███████╗░██████╗████████╗██╗░█████╗░███╗░░██╗  ██████╗░
        // ██╔═══██╗██║░░░██║██╔════╝██╔════╝╚══██╔══╝██║██╔══██╗████╗░██║  ╚════██╗
        // ██║██╗██║██║░░░██║█████╗░░╚█████╗░░░░██║░░░██║██║░░██║██╔██╗██║  ░█████╔╝
        // ╚██████╔╝██║░░░██║██╔══╝░░░╚═══██╗░░░██║░░░██║██║░░██║██║╚████║  ░╚═══██╗
        // ░╚═██╔═╝░╚██████╔╝███████╗██████╔╝░░░██║░░░██║╚█████╔╝██║░╚███║  ██████╔╝
        // ░░░╚═╝░░░░╚═════╝░╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝  ╚═════╝░

        public DisjointSet BuildUnionFind()
        {
            DisjointSet unionFind = new DisjointSet(graph.Count);
            foreach (var key in graph.Keys)
            {
                unionFind.Add(key);
            }
            return unionFind;
        }

        public List<GEdge> KruskalMST(Dictionary<int, GNode> graph, DisjointSet unionFind, List<GEdge> edgesMST)
        {

            float mstWeight = 0;
            allEdges.Sort((x, y) => x.weight.CompareTo(y.weight));


            foreach (var edge in allEdges)
            {
                var x = unionFind.FindParent(edge.nodes.node1.label);
                var y = unionFind.FindParent(edge.nodes.node2.label);

                if (x != y)
                {
                    unionFind.Union(x, y);
                    mstWeight += edge.weight;
                    edgesMST.Add(edge);
                }

            }

            return edgesMST;

        }

        public PerformanceTracker MeasureKruskalMST()
        {
            PerformanceTracker tracker = new PerformanceTracker();
            var disjointset = BuildUnionFind();

            List<GEdge> edgesMST = new List<GEdge>(graph.Count - 1);

            tracker.Measure(() =>
            {
                KruskalMST(this.graph, disjointset, edgesMST);
            });

            return tracker;
        }


    }
}
