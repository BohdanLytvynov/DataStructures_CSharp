// See https://aka.ms/new-console-template for more information
using CommonFunctions;

Console.WriteLine("Stack using Linked List");

StackLinkedList.StackLL<string> stack = new StackLinkedList.StackLL<string>();

for (int i = 0; i < 5; i++)
{
    stack.Push($"element{i + 1}");
}

for (int i = 0; i < 5; i++)
{        
    CF.PrintValue(stack.Pop());
}

Console.WriteLine("Finished");