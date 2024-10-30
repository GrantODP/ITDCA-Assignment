using GrantAssignment;


Graph graph = new Graph();
graph.FromFile("Graph.txt");
graph.BFS(1, n => Console.WriteLine(n.label));
