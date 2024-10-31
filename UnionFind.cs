class UnionFind
{

    Dictionary<int, int> parentMap;

    Dictionary<int, int> rankMap;

    public UnionFind(int maxItems)
    {
        parentMap = new Dictionary<int, int>(maxItems);
        rankMap = new Dictionary<int, int>(maxItems);
    }
    // 1:1  2:1  3:2 
    public int FindParent(int child)
    {
        if (child < 0)
        {
            throw new InvalidOperationException("Can not find parent of negative");
        }

        int root = child;

        // look for root
        while (!root.Equals(parentMap[root]))
        {
            root = parentMap[root];

        }

        //path compression. Reduce number of look ups by directly by updating to root parent
        int compressChild = child;

        while (!compressChild.Equals(parentMap[compressChild]))
        {
            int parent = parentMap[compressChild];
            parentMap[compressChild] = root;
            compressChild = parent;

        }
        return root;

    }

    public void Add(int value)
    {
        parentMap[value] = value;
        rankMap[value] = 0;
    }

    public void Union(int a, int b)
    {

        int parentA = FindParent(a);
        int parentB = FindParent(b);
        int parentRankA = rankMap[parentA];
        int parentRankB = rankMap[parentB];

        if (parentRankA > parentRankB)
        {
            parentMap[parentB] = parentA;
        }
        else if (parentRankA < parentRankB)
        {
            parentMap[parentA] = parentB;
        }
        else
        {
            parentMap[parentB] = parentA;
            rankMap[parentA] += 1;
        }
    }

}
