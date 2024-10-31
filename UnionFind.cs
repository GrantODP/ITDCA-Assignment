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
        int parent = -1;

        while (child.Equals(parent))
        {
            parent = parentMap[child];
            //next child
            child = parentMap[parent];
        }

        return parent;
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
