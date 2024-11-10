using GrantAssignment;


Graph graph = new Graph();
graph.FromFile("Graph.txt");


var kst = graph.MeasureKruskalMST();
var pst = graph.MeasurePrimsMST(1);


using (StreamWriter sw = File.AppendText("Performance.csv"))
{
    sw.WriteLine($"{kst.measuredRun},{pst.measuredRun}");
}
Console.WriteLine(kst);
Console.WriteLine(pst);




