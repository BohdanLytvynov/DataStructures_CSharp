// See https://aka.ms/new-console-template for more information
using BinarySearchTree;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static CommonFunctions.CF;

Console.WriteLine("Binary Search Tree!");

Random rand = new Random();

TaskShow("Creating a Binary Search Tree And performing sorting");

int n = 10;

var l = CreateNumbArray(n, -n, n);

TaskShow("Array of randon numbers:");

PrintInLine(l);

BinSearchTree<int> bst = new BinSearchTree<int>();

Stopwatch w = new Stopwatch();

w.Start();

foreach (int i in l)
{
    bst.Insert(i);
}

var s = bst.GetInorderTransversal();

w.Stop();

var t = w.ElapsedMilliseconds;

Console.WriteLine();

Console.WriteLine();

float r = t / 1000;

PrintMessage($"{r} sec.", ConsoleColor.Yellow);

TaskShow("Array of sorted numbers: (Binary Heap sort)");

PrintInLine(s);

Console.WriteLine();

TaskShow("Searching test");

for (int i = 0; i < 3; i++)
{
    int value = l[rand.Next(0, l.Count)];

    PrintValue(value, "Rendom selected value is:");

    var sr = bst.Search(bst.Root, value);

    PrintValue(sr == null ? "Not found!" : $"{sr.Data}", "Search Result");
}

int v = 1000;

PrintValue(v, "Rendom selected value is:");

var sr1 = bst.Search(bst.Root, v);

PrintValue(sr1 == null ? "Not found!" : $"{sr1.Data}", "Search Result");

#region Classic remove test

//TaskShow("Remove test");

//bst.Clear();

//l = CreateNumbArray(n, -n, n);

//PrintMessage("Array of randon numbers:", ConsoleColor.Green);

//PrintInLine(l);

//foreach (var item in l)
//{
//    bst.Insert(item);
//}

//Console.WriteLine();

//PrintMessage("Array of Sorted numbers:", ConsoleColor.Green);

//PrintInLine(bst.GetInorderTransversal());

//for (int i = 0; i < 3; i++)
//{
//    Console.WriteLine("Enter number");

//    var str = Console.ReadLine();

//    Console.WriteLine();

//    bst.RemoveNode(bst.Root, int.Parse(str));

//    Console.WriteLine();

//    PrintMessage("Inorder transversal after deletion:", ConsoleColor.Yellow);

//    Console.WriteLine();

//    PrintInLine(bst.GetInorderTransversal());
//}

#endregion

#region Manual Remove test

l = new List<int>() {3, 1, 7, -9, 2, 5 };

bst.Clear();

foreach (var item in l)
{
    bst.Insert(item);
}

PrintMessage("Sorted list before deletion of 1", ConsoleColor.Green);

PrintInLine(bst.GetInorderTransversal());

Console.WriteLine();

bst.RemoveNode(bst.Root, 1);

PrintMessage("Sorted list after deletion of 1", ConsoleColor.Yellow);

PrintInLine(bst.GetInorderTransversal());

#endregion

Console.WriteLine();

Console.WriteLine("Finished!");



