// See https://aka.ms/new-console-template for more information
using CommonFunctions;

Console.WriteLine("Stack!");

Stack.Stack<string> stack = new Stack.Stack<string>(5);

for (int i = 0; i < 5; i++)
{
    stack.Push($"element{i+1}");
}

for (int i = 0; i < 5; i++)
{
    string s = String.Empty;

    stack.Pop(ref s);

    CF.PrintValue(s);
}

Console.WriteLine("Finished");