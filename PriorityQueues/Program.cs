// See https://aka.ms/new-console-template for more information
Console.WriteLine("Priority Queues!");

PriorityQueues.PriorityQueue<string, int> priorityQueue = 
    new PriorityQueues.PriorityQueue<string, int>(2);

priorityQueue.Enqueue("A", 7);
priorityQueue.Enqueue("B", 2);

priorityQueue.Enqueue("C", 5);

priorityQueue.Enqueue("D", 8);

priorityQueue.Enqueue("F", 0);

priorityQueue.Enqueue("G", 4);

priorityQueue.Enqueue("H", 3);

while (priorityQueue.QueueSize != 0)
{
    Console.WriteLine($"Item: {priorityQueue.Dequeue()}");
}

Console.WriteLine("Finish");

