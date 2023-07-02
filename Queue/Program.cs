// See https://aka.ms/new-console-template for more information
using CommonFunctions;
using Queue;

Console.WriteLine("Queue!");

CustomQueue<int> q = new CustomQueue<int>(3);

for (int i = 0; i < q.Size+1; i++)
{
    q.Enqueue(i+1);
}

for (int i = 0; i < q.Size; i++)
{
    CF.PrintValue(q.Dequeue());
}

q.Restore();

Console.WriteLine("Finished!");
