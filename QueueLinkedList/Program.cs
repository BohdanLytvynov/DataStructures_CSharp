// See https://aka.ms/new-console-template for more information
using CommonFunctions;
using QueueLinkedList;

Console.WriteLine("Queue Linked List");

QueueLL<int> q = new QueueLL<int>();

for (int i = 0; i < 5; i++)
{
    q.Enqueue(i + 1);
}

for (int i = 0; i < 5; i++)
{
    CF.PrintValue(q.Dequeue());
}



Console.WriteLine("Finished!");
