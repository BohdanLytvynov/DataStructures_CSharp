using CircularDoubledLinkedList;
using CommonFunctions;
using static CommonFunctions.CF;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Circular Doubled Linked List!");

Circular_DoubledLinkedList<string> CDLL = new Circular_DoubledLinkedList<string>();

TaskShow("Adding first element");

CDLL.AddFirst("tail");

CDLL.AddFirst("elem1");

CDLL.AddFirst("elem2");

CDLL.AddFirst("elem3");

CDLL.AddFirst("head");

Print(CDLL.GetDataForward());

TaskShow("Printing backward");

Print(CDLL.GetDataBackward());

TaskShow("Adding to end");

CDLL.Clear();

CDLL.AddLast("head");

CDLL.AddLast("elem1");

CDLL.AddLast("elem2");

CDLL.AddLast("elem3");

CDLL.AddLast("tail");

Print(CDLL.GetDataForward());

CDLL.Clear();

TaskShow("Search at the begining");

CDLL.AddLast("target");

CDLL.AddLast("elem1");

CDLL.AddLast("tail");

var r = CDLL.Search("target");

var str = r != null ? r.ToString() : "Sorry No Elements Was found!";

Console.WriteLine($"Search result: {str}");

CDLL.Clear();

TaskShow("Search in the middle");

CDLL.AddLast("head");

CDLL.AddLast("elem1");

CDLL.AddLast("target");

CDLL.AddLast("elem2");

CDLL.AddLast("tail");

 r = CDLL.Search("target");

 str = r != null ? r.ToString() : "Sorry No Elements Was found!";

Console.WriteLine($"Search result: {str}");

CDLL.Clear();

TaskShow("Search at the end");

CDLL.AddLast("head");

CDLL.AddLast("elem1");

CDLL.AddLast("target");

r = CDLL.Search("target");

str = r != null ? r.ToString() : "Sorry No Elements Was found!";

Console.WriteLine($"Search result: {str}");

CDLL.Clear();

TaskShow("Remove 1 st element");

CDLL.AddLast("head");

CDLL.AddLast("elem1");

CDLL.AddLast("tail");

PrintMessage("Before remove", ConsoleColor.Green);

Print(CDLL.GetDataForward());

CDLL.Remove("head");

PrintMessage("After remove", ConsoleColor.Red);

Print(CDLL.GetDataForward());

TaskShow("Remove middle element");

CDLL.Clear();

CDLL.AddLast("head");

CDLL.AddLast("elem1");

CDLL.AddLast("tail");

PrintMessage("Before remove", ConsoleColor.Green);

Print(CDLL.GetDataForward());

CDLL.Remove("elem1");

PrintMessage("After remove", ConsoleColor.Red);

Print(CDLL.GetDataForward());

TaskShow("Remove last element");

CDLL.Clear();

CDLL.AddLast("head");

CDLL.AddLast("elem1");

CDLL.AddLast("tail");

PrintMessage("Before remove", ConsoleColor.Green);

Print(CDLL.GetDataForward());

CDLL.Remove("tail");

PrintMessage("After remove", ConsoleColor.Red);

Print(CDLL.GetDataForward());

Console.WriteLine("\nFinished");
