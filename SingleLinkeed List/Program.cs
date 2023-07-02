// See https://aka.ms/new-console-template for more information
using SingleLinkeed_List;
using System.Linq.Expressions;
using static CommonFunctions.CF;

Console.WriteLine("Single linked list!");

LinkedSingleList<int> lsl = new LinkedSingleList<int>();

lsl.AddFirst(10);

lsl.AddFirst(20);

lsl.AddFirst(30);

var r = lsl.GetData();

//Printing data from Linked List

TaskShow("Printing data from Linked List!");

Print(r);

//Inserting new element at the end of Linked List

TaskShow("Inserting new element at the end of Linked List");

var lsl2 = new LinkedSingleList<string>();

lsl2.AddLast("end1");

lsl2.AddLast("end2");

Print(lsl2.GetData());

//Searching the element

TaskShow("Searching for the Node with Value end2");

var v = lsl2.Search("end3");

Console.WriteLine(v?.ToString() ?? "There is no such element in a list");

//Deleting an element

TaskShow("Delete the node with key ");

LinkedSingleList<string> sls3 = new LinkedSingleList<string>();

sls3.AddFirst("elem1");

sls3.AddFirst("elem2");

sls3.AddFirst("elem3");

sls3.AddFirst("target");

sls3.AddFirst("elem4");

sls3.AddFirst("elem5");

Console.WriteLine("Before delete");

Print(sls3.GetData());

sls3.Delete("target");

Console.WriteLine("After delete");

Print(sls3.GetData());


Console.WriteLine("Finished!");
