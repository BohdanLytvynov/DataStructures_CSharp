// See https://aka.ms/new-console-template for more information
using CommonFunctions;
using DoubledLinkedList;

Console.WriteLine("Doubled Linked List!");

CF.TaskShow("Add new Nodes to the Doubled Linked List");

DoubledLinkedList<string> dll1 = new DoubledLinkedList<string>();

dll1.AddFirst("tail");

dll1.AddFirst("first2");

dll1.AddFirst("first3");

dll1.AddFirst("first4");

CF.Print(dll1.GetDataForward());

CF.TaskShow("Add new Nodes to the Doubled Linked List Add Last");

dll1.Clear();

dll1.AddLast("tail");

dll1.AddLast("first2");

dll1.AddLast("first3");

dll1.AddLast("first4");

CF.Print(dll1.GetDataForward());

CF.TaskShow("Reverse printing of the doubled Linked List");

CF.Print(dll1.GetDataBackward());

CF.TaskShow("Search of element target");

dll1.Clear();

dll1.AddLast("tail");

dll1.AddLast("target");

dll1.AddLast("first2");

dll1.AddLast("first3");

dll1.AddLast("first4");

var r = dll1.Search("target");

var str = r!=null? r.ToString() : "Sorry No Elements Was found!";

Console.WriteLine($"Search result: {str}");

CF.TaskShow("Remove head!");

dll1.Clear();

dll1.AddFirst("tail");

dll1.AddFirst("first2");

dll1.AddFirst("target");

dll1.AddFirst("first4");

dll1.AddFirst("head");

CF.PrintMessage("(Forward) List before head remove:", ConsoleColor.Red);

CF.Print(dll1.GetDataForward());

CF.PrintMessage("(Forward) List after head remove:", ConsoleColor.Red);

dll1.Remove("head");

CF.Print(dll1.GetDataForward());

CF.TaskShow("Remove middle!");

dll1.Clear();

dll1.AddFirst("tail");

dll1.AddFirst("first2");

dll1.AddFirst("target");

dll1.AddFirst("first4");

dll1.AddFirst("head");

CF.PrintMessage("(Forward) List before target remove:", ConsoleColor.Red);

CF.Print(dll1.GetDataForward());

CF.PrintMessage("(Forward) List after target remove:", ConsoleColor.Red);

dll1.Remove("target");

CF.Print(dll1.GetDataForward());

CF.TaskShow("Remove tail!");

dll1.Clear();

dll1.AddFirst("tail");

dll1.AddFirst("first2");

dll1.AddFirst("target");

dll1.AddFirst("first4");

dll1.AddFirst("head");

CF.PrintMessage("(Forward) List before tail remove:", ConsoleColor.Red);

CF.Print(dll1.GetDataForward());

CF.PrintMessage("(Forward) List after tail remove:", ConsoleColor.Red);

dll1.Remove("tail");

CF.Print(dll1.GetDataForward());

Console.WriteLine();

Console.WriteLine("Finished!");





