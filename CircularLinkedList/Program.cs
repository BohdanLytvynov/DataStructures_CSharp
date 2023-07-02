// See https://aka.ms/new-console-template for more information
using CircularLinkedList;
using static CommonFunctions.CF;

Console.WriteLine("Circular Linked List!");

Circular_Single_Linked_List<string> CSLL = new Circular_Single_Linked_List<string>();

TaskShow("Printing CSLL: After Add First");

CSLL.AddFirst("start");
CSLL.AddFirst("v1");
CSLL.AddFirst("v2");
CSLL.AddFirst("v3");
CSLL.AddFirst("end");

Print(CSLL.GetData());

CSLL.Clear();

TaskShow("Printing CSLL: After Add Last");

CSLL.AddLast("start");
CSLL.AddLast("v1");
CSLL.AddLast("v2");
CSLL.AddLast("v3");
CSLL.AddLast("end");

Print(CSLL.GetData());

TaskShow("Searching for element v2");

PrintValue(CSLL.Search("v2"));

TaskShow("Deleting element from first position");

CSLL.Clear();

CSLL.AddLast("start");
CSLL.AddLast("v1");
CSLL.AddLast("v2");
CSLL.AddLast("v3");
CSLL.AddLast("end");

TaskShow("Before deleting");

Print(CSLL.GetData());

TaskShow("After deleting");

CSLL.Delete("start");

Print(CSLL.GetData());

TaskShow("Deleting element from mid  (v2) position");

CSLL.Clear();

CSLL.AddLast("start");
CSLL.AddLast("v1");
CSLL.AddLast("v2");
CSLL.AddLast("v3");
CSLL.AddLast("end");

TaskShow("Before deleting");

Print(CSLL.GetData());

TaskShow("After deleting");

CSLL.Delete("v2");

Print(CSLL.GetData());

Console.WriteLine();

Console.WriteLine("Finished");
