namespace GrantAssignment
{


    public partial class Graph
    {


        // ░██████╗░██╗░░░██╗███████╗░██████╗████████╗██╗░█████╗░███╗░░██╗  ░░██╗██╗
        // ██╔═══██╗██║░░░██║██╔════╝██╔════╝╚══██╔══╝██║██╔══██╗████╗░██║  ░██╔╝██║
        // ██║██╗██║██║░░░██║█████╗░░╚█████╗░░░░██║░░░██║██║░░██║██╔██╗██║  ██╔╝░██║
        // ╚██████╔╝██║░░░██║██╔══╝░░░╚═══██╗░░░██║░░░██║██║░░██║██║╚████║  ███████║
        // ░╚═██╔═╝░╚██████╔╝███████╗██████╔╝░░░██║░░░██║╚█████╔╝██║░╚███║  ╚════██║
        // ░░░╚═╝░░░░╚═════╝░╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝  ░░░░░╚═╝

        public HashSet<GEdge> PrimsMST(int label, HashSet<GEdge> edgesMST, HashSet<int> mstNodes)
        {

            //Dont want to add duplicate edges so use hashset and sortedset

            SortedSet<GEdge> edgeQueue = new SortedSet<GEdge>();

            GNode node = graph[label];
            mstNodes.Add(node.label);
            node.neighbours.ForEach(edge => edgeQueue.Add(edge));

            while (edgeQueue.Count > 0)
            {
                //dequeue highest priority
                //
                var minEdge = edgeQueue.Min;
                edgeQueue.Remove(minEdge);
                node = minEdge.nodes.node1;


                if (mstNodes.Contains(node.label))
                {
                    //becuase we want to add neighbours edges we switch to neighbour
                    node = minEdge.nodes.node2;
                }

                //ensure no visted nodes are visited again.
                if (!mstNodes.Contains(node.label))
                {
                    edgesMST.Add(minEdge);
                    mstNodes.Add(node.label);
                    node.neighbours.ForEach(edge =>
                    {
                        //just to ensure edges already in MST are not added back to queue
                        if (!edgesMST.Contains(edge))
                        {
                            edgeQueue.Add(edge);
                        }

                    });
                }
            }

            return edgesMST;
        }



        public PerformanceTracker MeasurePrimsMST(int label)
        {
            PerformanceTracker tracker = new PerformanceTracker();

            HashSet<GEdge> edgesMST = new HashSet<GEdge>(graph.Count - 1);
            HashSet<int> mstNodes = new HashSet<int>(graph.Count);
            tracker.Measure(() =>
            {
                PrimsMST(label, edgesMST, mstNodes);
            });

            return tracker;
        }

    }
}
