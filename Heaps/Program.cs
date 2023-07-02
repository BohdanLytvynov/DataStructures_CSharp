// See https://aka.ms/new-console-template for more information
using Heaps;
using static CommonFunctions.CF;

Console.WriteLine("Heaps!");

Heap<int> heap = new Heap<int>(2);

heap.Add(60);

heap.Add(20);

heap.Add(5);

heap.Add(10);

heap.Add(5);

heap.Add(3);

heap.Add(5);

while (!heap.IsEmpty())
{
    Console.Write($"{heap.Poll(), 5}");
}

heap.Add(60);

heap.Add(20);

heap.Add(10);

heap.Add(5);

heap.Add(5);

heap.Add(5);

heap.Add(3);

heap.Remove(5);


PrintMessage("After remove", ConsoleColor.Green);

while (!heap.IsEmpty())
{
    Console.Write($"{heap.Poll(),5}");
}
