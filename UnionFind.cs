class UnionFind<T>
{

    Dictionary<T, T> parentMap;
    Dictionary<T, int> rankMap;

    public UnionFind(int maxItems)
    {
        parentMap = new Dictionary<T, T>(maxItems);
        rankMap = new Dictionary<T, int>(maxItems);
    }
    // 1:1  2:1  3:2 
    T FindParent(T child)
    {
        if (child == null)
        {
            throw new InvalidOperationException("Can not find parent of null");
        }
        T parent = default(T);

        while (child.Equals(parent))
        {
            parent = parentMap[child];
            //next child
            child = parentMap[parent];
        }

        return parent;
    }

}
