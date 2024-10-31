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
        int parent = parentMap[child];
        int tempChild = parentMap[parent];

        while (!tempChild.Equals(parent))
        {
            parent = parentMap[tempChild];
            //next child
            tempChild = parentMap[parent];
        }

        //path compression. Reduce number of look ups by directly by updating to root parent
        parentMap[child] = parent;
        return parent;
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
            parentMap[b] = parentA;
        }
        else if (parentRankA < parentRankB)
        {
            parentMap[a] = parentB;
        }
        else
        {
            parentMap[b] = parentA;
            rankMap[parentA] += 1;
        }
    }

}
