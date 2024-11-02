using System.Collections.Generic;

namespace GrantAssignment
{

  // ░██████╗░██╗░░░██╗███████╗░██████╗████████╗██╗░█████╗░███╗░░██╗  ░░███╗░░
  // ██╔═══██╗██║░░░██║██╔════╝██╔════╝╚══██╔══╝██║██╔══██╗████╗░██║  ░████║░░
  // ██║██╗██║██║░░░██║█████╗░░╚█████╗░░░░██║░░░██║██║░░██║██╔██╗██║  ██╔██║░░
  // ╚██████╔╝██║░░░██║██╔══╝░░░╚═══██╗░░░██║░░░██║██║░░██║██║╚████║  ╚═╝██║░░
  // ░╚═██╔═╝░╚██████╔╝███████╗██████╔╝░░░██║░░░██║╚█████╔╝██║░╚███║  ███████╗
  // ░░░╚═╝░░░░╚═════╝░╚══════╝╚═════╝░░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝  ╚══════╝

  class GNode
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
  class EdgePair
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

  class GEdge : IComparable<GEdge>
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


  class Graph
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

        foreach (var edge in current.neighbours)
        {
          var neighbour = edge.nodes.Neighbour(current);
          var label = neighbour.label;

          if (!visited.Contains(label))
          {
            stack.Push(neighbour);
            visited.Add(label);
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

      int i = 0;

      while (i < graph.Count && i < allEdges.Count)
      {
        var edge = allEdges[i];
        var x = unionFind.FindParent(edge.nodes.node1.label);
        var y = unionFind.FindParent(edge.nodes.node2.label);

        if (x != y)
        {
          unionFind.Union(x, y);
          mstWeight += edge.weight;
          edgesMST.Add(edge);
        }

        i++;
      }

      return edgesMST;

    }

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
              edgeQueue.Add(edge);

          });
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
