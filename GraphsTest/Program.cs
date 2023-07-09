
using GraphsMath.Graphs.ALGraphs;
using GraphsMath.Graphs.AMGraphs;
using GraphsMath.Graphs.Graph_Components;
using GraphsMath.Graphs.Interfaces;
using GraphsMath.Graphs.MultiDimGrid;
using GraphsMath.SolvingOfProblems;
using GraphsMath.SolvingOfProblems.ShortestPathProblem;
using GraphsMath.SolvingOfProblems.ShortestPathProblemOnWDAG;
using GraphsMath.SolvingOfProblems.SolverArgs;
using System.Diagnostics;
using System.Net.WebSockets;
using static CommonFunctions.CF;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Graphs Test Project");

#region AM Graph

#region DFS Try

//AdjacentMatrixGraph<int> g = new AdjacentMatrixGraph<int>(5);

//g.AddEdge(0, 1, 5);
//g.AddEdge(1, 3, 7);
//g.AddEdge(2, 0, 9);
//g.AddEdge(0, 2, 8);
//g.AddEdge(2, 0, 9);
//g.AddEdge(2, 3, 4);
//g.AddEdge(3, 2, 5);
//g.AddEdge(3, 4, 9);
//g.AddEdge(4, 2, 10);

//PrintMessage("Matrix representation of a graph", ConsoleColor.Green);

//Console.WriteLine(g.ToString());

//PrintMessage("Search for neighbors", ConsoleColor.Green);

//for (int i = 0; i < g.VertexCount; i++)
//{
//    Console.WriteLine();

//    PrintMessage($"Neighbors for {i}", ConsoleColor.Green);

//    PrintInLine(g.GetNeighbors(i));

//    Console.WriteLine();
//}

//PrintMessage("DFS try", ConsoleColor.Green);

//var Route = new List<int>();

//var visitArray = g.InitializeVisitDS();

//g.DFS(0, visitArray, (v, vA) =>
//{
//    Route.Add(v);

//    return true;
//});

//PrintMessage("Show Route of DFS:", ConsoleColor.Green);

//PrintInLine(Route);

//Console.WriteLine();
//Console.WriteLine();

#endregion

#region Common Components Problem

//AdjacentMatrixGraph<int> g1 = new AdjacentMatrixGraph<int>(9);

//g1.AddEdge(0, 1, 5);
//g1.AddEdge(0, 2, 2);
//g1.AddEdge(1, 2, 1);
//g1.AddEdge(3, 4, 3);
//g1.AddEdge(4, 3, 10);
//g1.AddEdge(5, 6, 5);
//g1.AddEdge(5, 8, 7);
//g1.AddEdge(6, 8, 9);
//g1.AddEdge(7, 5, 2);
//g1.AddEdge(7, 8, 1);// try 7 8 1
//g1.AddEdge(8, 6, 8);

//Console.WriteLine(g1);

//CommonComponents_Problem<int, int, int> CCP =
//    new CommonComponents_Problem<int, int, int>(g1);

//var r = CCP.Solve();

//if (!r.HasError)
//{
//    PrintMessage("Common components are:", ConsoleColor.Green);

//    var comp = (Dictionary<int, int>)r.Result[0];

//    for (int i = 0; i < comp.Count; i++)
//    {
//        Console.WriteLine($"Vertex {i}: Index: {comp[i]}");
//    }
//}
//else
//{
//    PrintMessage("Error!!", ConsoleColor.Red);
//}

#endregion

#region BFS test

//AdjacentMatrixGraph<int> g2 = new AdjacentMatrixGraph<int>(9);

//g2.AddEdge(0, 1, 1);
//g2.AddEdge(0, 4, 1);
//g2.AddEdge(0, 2, 1);
//g2.AddEdge(2, 3, 1);
//g2.AddEdge(1, 4, 1);
//g2.AddEdge(2, 5, 1);
//g2.AddEdge(2, 5, 1);
//g2.AddEdge(3, 4, 1);
//g2.AddEdge(3, 5, 1);
//g2.AddEdge(3, 7, 1);
//g2.AddEdge(4, 7, 1);
//g2.AddEdge(5, 6, 1);
//g2.AddEdge(6, 3, 1);
//g2.AddEdge(6, 8, 1);
//g2.AddEdge(7, 8, 1);

//Console.WriteLine(g2);

//PrintMessage("BFS Result:", ConsoleColor.Green);

//Console.WriteLine();

//for (int i = 0; i < g2.VertexCount; i++)
//{
//    PrintMessage($"Start Vertex is:{i}", ConsoleColor.Yellow);
//    Console.WriteLine();

//    var path = new List<int>();

//    g2.BFS(i, (c, n) =>
//    {
//        path.Add(n);

//        return true;
//    });

//    PrintInLine(path);

//    Console.WriteLine();
//}

#endregion

#region Shortes path problem

//AdjacentMatrixGraph<int> g4 = new AdjacentMatrixGraph<int>(9);

//g4.AddEdge(0, 1, 1);
//g4.AddEdge(0, 2, 1);
//g4.AddEdge(0, 3, 1);

//g4.AddEdge(1, 2, 1);
//g4.AddEdge(1, 5, 1);

//g4.AddEdge(2, 5, 1);
//g4.AddEdge(2, 4, 1);

//g4.AddEdge(3, 2, 1);
//g4.AddEdge(3, 4, 1);

//g4.AddEdge(4, 6, 1);
//g4.AddEdge(4, 7, 1);

//g4.AddEdge(5, 4, 1);
//g4.AddEdge(5, 6, 1);

//g4.AddEdge(6, 7, 1);
//g4.AddEdge(6, 8, 1);

//g4.AddEdge(7, 8, 1);

//PrintMessage("Adjacency matrix", ConsoleColor.Green);

//Console.WriteLine(g4);

//int nullV = -1;


//ShortestPath_Problem<int, int, int> SPP =
//    new ShortestPath_Problem<int, int, int>(g4, nullV);

//int start = 2;

//var r = SPP.Solve(new ShortestPathProblemArgs<int>(start));

//if (!r.HasError)
//{
//    PrintMessage($"Prev Path Array if we start from {start}:", ConsoleColor.Yellow);

//    Console.WriteLine();

//    PrintInLine((Dictionary<int, int>)r.Result[0]);

//    Console.WriteLine();

//    List<int> Path = null;

//    for (int i = 0; i < g4.VertexCount; i++)
//    {
//        Path = SPP.ReconstructPath(start, i, (Dictionary<int, int>)r.Result[0]);

//        PrintMessage($"Index: {i}", ConsoleColor.Green);

//        Console.WriteLine();

//        Console.WriteLine();

//        PrintMessage("Shortest path was found: ", ConsoleColor.Yellow);

//        Console.WriteLine();

//        PrintInLine(Path);

//        Console.WriteLine();

//        Path.Clear();

//    }
//}
//else
//{
//    Console.WriteLine();
//    Console.WriteLine("Error!");
//}


#endregion

#region Topological Sorting

//AdjacentMatrixGraph<int> g5 = new AdjacentMatrixGraph<int>(5);

//g5.AddEdge(0, 1, 1);

//g5.AddEdge(0, 2, 1);

//g5.AddEdge(1, 3, 1);

//g5.AddEdge(2, 3, 1);

//g5.AddEdge(2, 4, 1);

//g5.AddEdge(3, 4, 1);

//Console.WriteLine(g5);

//var sort = g5.TopSort();

//PrintMessage("Top Sort result:", ConsoleColor.Green);

//PrintInLine(sort);

#endregion

#endregion

#region Shortest path problem on a n - Dim grid

//string[,] TwoDimGrid = new string[4, 5];

//for (int i = 0; i < TwoDimGrid.GetLongLength(0); i++)
//{
//    for (int j = 0; j < TwoDimGrid.GetLongLength(1); j++)
//    {
//        TwoDimGrid[i, j] = ".";
//    }
//}

//string start = "S";
//string obstacle = "#";
//TwoDimGrid[0, 0] = start;

//TwoDimGrid[0, 1] = obstacle;
//TwoDimGrid[0, 3] = obstacle;
//TwoDimGrid[1, 2] = obstacle;
//TwoDimGrid[2, 0] = obstacle;
//TwoDimGrid[2, 4] = obstacle;
//TwoDimGrid[3, 0] = obstacle;
//TwoDimGrid[3, 4] = obstacle;

//var startPoint = new List<int>() { 0, 0 };

//string end = "E";

//TwoDimGrid[0, 4] = end;

//for (int i = 0; i < TwoDimGrid.GetLongLength(0); i++)
//{
//    for (int j = 0; j < TwoDimGrid.GetLongLength(1); j++)
//    {
//        Console.Write($"{TwoDimGrid[i, j],3}");
//    }
//    Console.WriteLine();
//}

//IGrid<string, int> grid = new MultiDim_Grid<string>(TwoDimGrid,
//    (grid, point) => { return grid[point[0], point[1]]; },
//    (grid, point, item) => { grid[point[0], point[1]] = item; }
//    );

//List<int> RVectors = new List<int> { -1, 1, 0, 0 };

//List<int> CVectors = new List<int> { 0, 0, 1, -1 };

//MultiDimGridShortestPathProblem<string> MDGSPP = new MultiDimGridShortestPathProblem<string>(
//    grid,
//    new DirectionVectors<int>(new List<List<int>>() { RVectors, CVectors }),
//    (visitMatrix, point) => { return visitMatrix[point[0], point[1]]; },
//    (visitMatrix, point, item) => { visitMatrix[point[0], point[1]] = item; }
//    );

//var r = MDGSPP.Solve(new List<object>() { startPoint, end,
//    new bool[TwoDimGrid.GetLongLength(0), TwoDimGrid.GetLongLength(1)], obstacle });

//Dictionary<List<int>, List<List<int>>> d = null;

//if (!r.HasError)
//{
//    PrintValue(r.Result[0], "Steps count:");

//    d = (Dictionary<List<int>, List<List<int>>>)r.Result[1];

//    var Keys = d.Keys.ToList();

//    int count = d.Keys.Count;

//    for (int i = 0; i < count; i++)
//    {
//        var cell = Keys[i];

//        PrintMessage($"Cell: ({PrintInLine(cell, 2)})", ConsoleColor.Green);

//        Console.WriteLine();

//        var adjCells = d[cell];

//        var amount = adjCells.Count;

//        for (int j = 0; j < amount; j++)
//        {
//            PrintMessage($"{j + 1}) Adj Cell To ({PrintInLine(cell, 2)}): ({PrintInLine(adjCells[j], 2)})", ConsoleColor.Yellow);

//            Console.WriteLine();
//        }
//    }

//    var p = MultiDimGridShortestPathProblem<string>.ReconstructPath(startPoint, (List<int>)r.Result[2],
//        d);

//    PrintMessage("Shortest Path: ", ConsoleColor.Green);

//    for (int i = 0; i < p.Count; i++)
//    {
//        Console.Write($"({PrintInLine(p[i], 2)}),");
//    }
//}

#endregion

#region Graph using AL

//List<AdjListVertex<string, float>> Verteces = new List<AdjListVertex<string, float>>();

//Verteces.Add(new AdjListVertex<string, float>("A"));

//Verteces.Add(new AdjListVertex<string, float>("B"));

//Verteces.Add(new AdjListVertex<string, float>("C"));

//Verteces.Add(new AdjListVertex<string, float>("D"));

//Verteces.Add(new AdjListVertex<string, float>("E"));

//Verteces.Add(new AdjListVertex<string, float>("F"));

//Verteces.Add(new AdjListVertex<string, float>("G"));

//AdjacentListGraph<string, float> dynamicGraph =
//    new AdjacentListGraph<string, float>(Verteces);

//dynamicGraph.AddEdge("A", "B", 5);

//dynamicGraph.AddEdge("A", "F", 3);

//dynamicGraph.AddEdge("B", "E", 9);

//dynamicGraph.AddEdge("B", "C", 4);

//dynamicGraph.AddEdge("C", "E", 2);

//dynamicGraph.AddEdge("C", "D", 8);

//dynamicGraph.AddEdge("D", "E", 2);

//dynamicGraph.AddEdge("E", "C", 5);

//dynamicGraph.AddEdge("E", "F", 4);

//dynamicGraph.AddEdge("E", "G", 3);

//dynamicGraph.AddEdge("F", "A", 8);

//dynamicGraph.AddEdge("F", "E", 2);

//dynamicGraph.AddEdge("F", "G", 4);

//dynamicGraph.AddEdge("G", "F", 2);

//dynamicGraph.AddEdge("G", "E", 2);

//Console.WriteLine(dynamicGraph);

#region Iter tests

//List<AdjListVertex<string, float>> Verteces = new List<AdjListVertex<string, float>>();

//Verteces.Add(new AdjListVertex<string, float>("A"));

//Verteces.Add(new AdjListVertex<string, float>("B"));

//Verteces.Add(new AdjListVertex<string, float>("C"));

//Verteces.Add(new AdjListVertex<string, float>("D"));

//Verteces.Add(new AdjListVertex<string, float>("E"));

//AdjacentListGraph<string, float> dynamicGraph =
//    new AdjacentListGraph<string, float>(Verteces);

//dynamicGraph.AddEdge("A", "B", 5);

//dynamicGraph.AddEdge("A", "C", 2);

//dynamicGraph.AddEdge("B", "A", 4);

//dynamicGraph.AddEdge("B", "C", 8);

//dynamicGraph.AddEdge("B", "D", 9);

//dynamicGraph.AddEdge("C", "E", 10);

//dynamicGraph.AddEdge("D", "E", 6);

//dynamicGraph.AddEdge("D", "B", 2);

//dynamicGraph.AddEdge("E", "D", 5);

//dynamicGraph.AddEdge("E", "C", 11);

//Console.WriteLine(dynamicGraph);

#region DFS

//var VD = AdjacentListGraph<string, float>.InitializeVisitDicionary(dynamicGraph);

//List<AdjListVertex<string, float>> dfs = new List<AdjListVertex<string, float>>();

//dynamicGraph.DFS("A", VD, (v, visitDic) =>
//{
//    dfs.Add(v);

//    return true;
//});

//PrintMessage("DFS Result:", ConsoleColor.Green);

//PrintInLine(dfs);

#endregion

#region BFS (ShortestPathProblem)

//var startBFS = "A";

//var nullValue = "Empty";

//var SPP1 = new ShortestPath_Problem<AdjListVertex<string, float>, string, float>(dynamicGraph, nullValue);

//var r1 = SPP1.Solve(new ShortestPathProblemArgs<string>(startBFS));

//if (!r1.HasError)
//{
//    PrintMessage($"Prev Path Array if we start from {startBFS}: ", ConsoleColor.Yellow);

//    Console.WriteLine();

//    PrintInLine((Dictionary<AdjListVertex<string, float>, AdjListVertex<string, float>>)r1.Result[0]);

//    Console.WriteLine();

//    List<AdjListVertex<string, float>> Path = null;

//    var verteces = dynamicGraph.GetVerteces();

//    foreach (var item in verteces)
//    {
//        Path = SPP1.ReconstructPath(startBFS, item, (Dictionary<AdjListVertex<string, float>, AdjListVertex<string, float>>)r1.Result[0]);

//        Console.WriteLine();

//        PrintMessage($"Shortest path from{startBFS} to {item} was found: ", ConsoleColor.Green);

//        Console.WriteLine();

//        PrintInLine(Path);

//        Console.WriteLine();

//        Path.Clear();

//    }
//}
//else
//{
//    Console.WriteLine();
//    Console.WriteLine("Error!");
//}

#endregion

#endregion

#region Topological Sorting

//List<AdjListVertex<string, float>> vl = new List<AdjListVertex<string, float>>()
//{ new AdjListVertex<string, float>("A"), new AdjListVertex<string, float>("B"),
//new AdjListVertex<string, float>("C"),new AdjListVertex<string, float>("D"),
//new AdjListVertex<string, float>("E")};

//AdjacentListGraph<string, float> dg1 = new AdjacentListGraph<string, float>(vl);

//dg1.AddEdge("A", "B", 1);

//dg1.AddEdge("A", "C", 1);

//dg1.AddEdge("B", "D", 1);

//dg1.AddEdge("B", "E", 1);

//dg1.AddEdge("C", "D", 1);

//dg1.AddEdge("D", "E", 1);

//PrintMessage("AL:", ConsoleColor.Green);

//Console.WriteLine(dg1);

//PrintMessage("Top Sort Result", ConsoleColor.Yellow);

//var topSortRes = dg1.TopSort();

//PrintInLine(topSortRes);

#endregion

#region ShortestPathOn A WDAG

//var verteces = new List<AdjListVertex<string, float>>() {
//"A", "B", "C", "D", "E", "F", "G", "H"
//};

//var dag = new AdjacentListGraph<string, float>(verteces);

//dag.AddEdge("A", "B", 3);

//dag.AddEdge("A", "C", 6);

//dag.AddEdge("B", "C", 4);

//dag.AddEdge("B", "D", 4);

//dag.AddEdge("B", "E", 11);

//dag.AddEdge("C", "D", 8);

//dag.AddEdge("C", "G", 11);

//dag.AddEdge("D", "E", -4);

//dag.AddEdge("D", "F", 5);

//dag.AddEdge("D", "G", 2);

//dag.AddEdge("E", "H", 9);

//dag.AddEdge("F", "H", 1);

//dag.AddEdge("G", "H", 2);

//Console.WriteLine(dag);

//PrintMessage("Topological Sorting:", ConsoleColor.Green);

//PrintInLine(dag.TopSort());

//ShortestPathProblemForWDAGArgs<string> SPPWDAGArgs =
//    new ShortestPathProblemForWDAGArgs<string>("A");

//ShortestPathProblemOnWDAG<AdjListVertex<string, float>, string, float> SPPWDAG =
//    new ShortestPathProblemOnWDAG<AdjListVertex<string, float>, string, float>(dag, 0);

//var r = SPPWDAG.Solve(SPPWDAGArgs);

//PrintMessage("ShortestPathProblemOnWDAG", ConsoleColor.Green);

//if (!r.HasError)
//{
//    PrintInLine((Dictionary<string, float>)r.Result[0]);
//}

#endregion

#region SHortest Path Problem via Dijkstras

//var verteces = new List<AdjListVertex<string, float>>() {
//"A", "B", "C", "D", "E", "F", "G", "H"
//};

//var dag = new AdjacentListGraph<string, float>(verteces);

//dag.AddEdge("A", "B", 3);

//dag.AddEdge("A", "C", 6);

//dag.AddEdge("B", "C", 4);

//dag.AddEdge("B", "D", 4);

//dag.AddEdge("B", "E", 11);

//dag.AddEdge("C", "D", 8);

//dag.AddEdge("C", "G", 11);

//dag.AddEdge("D", "E", 4);

//dag.AddEdge("D", "F", 5);

//dag.AddEdge("D", "G", 2);

//dag.AddEdge("E", "H", 9);

//dag.AddEdge("F", "H", 1);

//dag.AddEdge("G", "H", 2);

//Console.WriteLine(dag);

//DijkstrasShortestPathProblem<AdjListVertex<string, float>, string, float> DSPP =
//    new DijkstrasShortestPathProblem<AdjListVertex<string, float>, string, float>(
//        dag, "Empty", 0
//        );

//var vert = dag.GetVerteces();

//foreach (var start in vert)
//{
//    var r = DSPP.Solve(new DijkstrasShortestPathProblemArgs<string>(start.VertexKey));

//    if (!r.HasError)
//    {
//        var distDicionary = (Dictionary<string, float>)r.Result[0];

//        var prev = (Dictionary<string, string>)r.Result[1];

//        PrintMessage($"Dist Dictionary if we start from: {start.VertexKey}: ", ConsoleColor.Green);

//        PrintInLine(distDicionary);

//        PrintMessage($"Prev Dictionary if we start from: {start.VertexKey}: ", ConsoleColor.Green);

//        PrintInLine(prev);

//        foreach (var item in vert)
//        {
//            var path = DSPP.ReconsructPath(start.VertexKey, item, prev);

//            PrintMessage($"Path from {start.VertexKey} to {item.VertexKey} with Distance: {distDicionary[item.VertexKey]}: ",
//                ConsoleColor.Yellow);

//            PrintInLine(path);
//        }

//    }
//}



#endregion

#region Bellman-Ford Algorithm



//var graph = new AdjacentListGraph<byte, double>(
//    new List<AdjListVertex<byte, double>>() { 0,1,2,3,4,5,6,7,8,9 }
//    );

//graph.AddEdge(0, 1, 5);
//graph.AddEdge(1, 2, 20);
//graph.AddEdge(1, 5, 30);
//graph.AddEdge(1, 6, 60);
//graph.AddEdge(2, 3, 10);
//graph.AddEdge(3, 2, -15);
//graph.AddEdge(2, 4, 75);
//graph.AddEdge(4, 9, 100);
//graph.AddEdge(5, 4, 25);
//graph.AddEdge(5, 6, 5);
//graph.AddEdge(5, 8, 50);
//graph.AddEdge(6, 7, -50);
//graph.AddEdge(7, 8, -10);


//Console.WriteLine(graph);

//BellmanFord<AdjListVertex<byte, double>, byte, double> BellFord =
//    new BellmanFord<AdjListVertex<byte, double>, byte, double>(graph, double.PositiveInfinity, 
//    double.NegativeInfinity);

//var r = BellFord.Solve();

//if (!r.HasError)
//{
//    var disDict = (Dictionary<byte, double>)r.Result[0];

//    PrintMessage("Distance Dictionary After first Iteration:", ConsoleColor.Green);

//    PrintInLine(disDict);

//    BellFord.FindNegativeCycles(disDict);

//    PrintMessage("Distance Dictionary After Second Iteration:", ConsoleColor.Green);

//    PrintInLine(disDict);

//}

#endregion

#region Floyd-Warshal

//AdjacentMatrixGraph<float> FWgraph = new AdjacentMatrixGraph<float>(4, float.PositiveInfinity);

//FWgraph.AddEdge(0,1,8);
//FWgraph.AddEdge(0,3,1);
//FWgraph.AddEdge(1,2,1);
//FWgraph.AddEdge(2,0,4);
//FWgraph.AddEdge(3,1,2);
//FWgraph.AddEdge(3,2,9);
////Explicitly set condition that there are no self edges
//FWgraph.AddEdge(0,0,0);
//FWgraph.AddEdge(1,1,0);
//FWgraph.AddEdge(2,2,0);
//FWgraph.AddEdge(3,3,0);

//Console.WriteLine(FWgraph);

//FLoydWarshal<int, int, float> fw = new FLoydWarshal<int, int, float>(FWgraph, float.NegativeInfinity);

//var r = fw.Solve(new FloydWarshalArgs(false));

//if (!r.HasError)
//{
//    PrintMessage("Distance Matrix:", ConsoleColor.Green);

//    var dm = r.Result[0] as float[,];

//    Print(dm);

//    PrintMessage("Path Matrix: (i, j - verteces, value - aditional vertex between i and j)", ConsoleColor.Green);

//    var next = r.Result[1] as int[,];

//    Print(next);

//    PrintMessage("Attempt to calculate pathes...", ConsoleColor.Yellow);

//    var verteces = FWgraph.GetVerteces();

//    foreach (var s in verteces)
//    {
//        foreach (var v in verteces)
//        {
//            var path = fw.ReconstructPath(s, v, next, dm);

//            PrintMessage($"Path from {s} to {v} with weight {dm[s,v]}:", ConsoleColor.Yellow);

//            PrintInLine(path);
//        }
//    }

//}

#endregion

#region Bridges Detection 1
//AdjacentListGraph<byte, float> bdGraph = new AdjacentListGraph<byte, float>(
//    new List<AdjListVertex<byte, float>>() { 0, 1, 2, 3, }
//    );

//bdGraph.AddBiDirectionalEdge(0, 1, 1, 1);

//bdGraph.AddBiDirectionalEdge(1, 2, 1, 1);

//bdGraph.AddBiDirectionalEdge(2, 3, 1, 1);

//bdGraph.AddBiDirectionalEdge(1, 3, 1, 1);

//Console.WriteLine(bdGraph);

//BridgesDetector<AdjListVertex<byte, float>, byte, float> BD =
//    new BridgesDetector<AdjListVertex<byte, float>, byte, float>(bdGraph);

//var r = BD.Solve(new BridgeDetectorArgs<byte>((byte)(bdGraph.VertexCount + 1)));

//if (!r.HasError)
//{
//    var bridges = (Dictionary<byte, byte>)r.Result[0];

//    PrintMessage("Bridges in graph are:", ConsoleColor.Green);

//    PrintInLine(bridges);
//}
#endregion

#region Bridges Detection 2

//AdjacentListGraph<byte, float> bdGraph = new AdjacentListGraph<byte, float>(
//    new List<AdjListVertex<byte, float>>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 }
//    );

//bdGraph.AddBiDirectionalEdge(0, 1, 1, 1);

//bdGraph.AddBiDirectionalEdge(0, 2, 1, 1);

//bdGraph.AddBiDirectionalEdge(1, 2, 1, 1);

//bdGraph.AddBiDirectionalEdge(2, 5, 1, 1);

//bdGraph.AddBiDirectionalEdge(2, 3, 1, 1);

//bdGraph.AddBiDirectionalEdge(3, 4, 1, 1);

//bdGraph.AddBiDirectionalEdge(5, 6, 1, 1);

//bdGraph.AddBiDirectionalEdge(6, 7, 1, 1);

//bdGraph.AddBiDirectionalEdge(7, 8, 1, 1);

//bdGraph.AddBiDirectionalEdge(8, 5, 1, 1);

//Console.WriteLine(bdGraph);

//BridgesDetector<AdjListVertex<byte, float>, byte, float> BD =
//    new BridgesDetector<AdjListVertex<byte, float>, byte, float>(bdGraph);

//var r = BD.Solve(new BridgeDetectorArgs<byte>((byte)(bdGraph.VertexCount + 1)));

//if (!r.HasError)
//{
//    var bridges = (Dictionary<byte, byte>)r.Result[0];

//    PrintMessage("Bridges in graph are:", ConsoleColor.Green);

//    PrintInLine(bridges);
//}

#endregion

#region Articulation Point Detection

//AdjacentListGraph<byte, float> ArtPointDetectionGraph = new AdjacentListGraph<byte, float>(
//    new List<AdjListVertex<byte, float>>() { 0, 1, 2, 3, }
//    );

//ArtPointDetectionGraph.AddBiDirectionalEdge(0, 1, 1, 1);

//ArtPointDetectionGraph.AddBiDirectionalEdge(1, 2, 1, 1);

//ArtPointDetectionGraph.AddBiDirectionalEdge(2, 3, 1, 1);

//ArtPointDetectionGraph.AddBiDirectionalEdge(1, 3, 1, 1);

//Console.WriteLine(ArtPointDetectionGraph);

//ArticulationPointDetector<AdjListVertex<byte, float>, byte, float> APD =
//    new ArticulationPointDetector<AdjListVertex<byte, float>, byte, float>(ArtPointDetectionGraph);

//var r = APD.Solve(new ArticulationPointDetectorArgs<byte> ((byte)(ArtPointDetectionGraph.VertexCount + 1)));

//if (!r.HasError)
//{
//    PrintMessage("Articulation Points detected in a graph:", ConsoleColor.Green);

//    var artPoints = (Dictionary<byte, bool>)r.Result[0];

//    PrintInLine(artPoints);
//}

#endregion

#region Strongly connected components detection

//AdjacentListGraph<string, float> SCCGraph = new AdjacentListGraph<string, float>(
//    new List<AdjListVertex<string, float>>() { "A", "B","C", "D", "E", "F", "G", "H", "I" }
//    );

//SCCGraph.AddBiDirectionalEdge("A", "B", 1, 1);

//SCCGraph.AddBiDirectionalEdge("A", "C", 1, 1);

//SCCGraph.AddEdge("B", "D", 1);

//SCCGraph.AddEdge("C", "D", 1);

//SCCGraph.AddBiDirectionalEdge("D", "F", 1, 1);

//SCCGraph.AddEdge("E", "B", 1);

//SCCGraph.AddEdge("E", "I", 1);

//SCCGraph.AddEdge("E", "F", 1);

//SCCGraph.AddEdge("G", "E", 1);

//SCCGraph.AddEdge("H", "G", 1);

//SCCGraph.AddEdge("H", "I", 1);

//SCCGraph.AddEdge("H", "H", 1);

//SCCGraph.AddEdge("I", "F", 1);

//SCCGraph.AddEdge("I", "G", 1);

//Console.WriteLine(SCCGraph);

//StronglyConnectedComponentsDetector<AdjListVertex<string, float>, string, float> SCCC =
//    new StronglyConnectedComponentsDetector<AdjListVertex<string, float>, string, float>(SCCGraph);

//var r = SCCC.Solve();

//if (!r.HasError)
//{
//    PrintMessage("Strongly Connected Components are:", ConsoleColor.Green);

//    var lowlink = (Dictionary<string, int>)r.Result[0];

//    PrintInLine(lowlink);
//}

#endregion

#region Travel SalesMan Problem

AdjacentMatrixGraph<float> TravelSalesManGraph = new AdjacentMatrixGraph<float>(4);

TravelSalesManGraph.AddBiDirectionalEdge(0, 1, 10, 5);

TravelSalesManGraph.AddBiDirectionalEdge(0, 2, 8, 6);

TravelSalesManGraph.AddBiDirectionalEdge(0, 3, 4, 6);

TravelSalesManGraph.AddBiDirectionalEdge(1, 3, 5, 9);

TravelSalesManGraph.AddBiDirectionalEdge(1, 2, 3, 2);

TravelSalesManGraph.AddBiDirectionalEdge(2, 3, 5, 7);

Console.WriteLine(TravelSalesManGraph);

TravelSalesmanProblem<float> TSMP = new TravelSalesmanProblem<float>(TravelSalesManGraph, 
    float.PositiveInfinity);

var r = TSMP.Solve(new TravelSalesmanProblemArgs(0));

if (!r.HasError)
{
    var minPathCost = (float)r.Result[0];

    PrintMessage("Minimum Cost Rout Was found:", ConsoleColor.Green);

    var route = (int[])r.Result[1];

    PrintInLine(route);

    Console.WriteLine();

    PrintValue(minPathCost, "Minimum cost route:", ConsoleColor.Green);    
}

#endregion

#endregion

Console.WriteLine();

Console.WriteLine("Finish");
