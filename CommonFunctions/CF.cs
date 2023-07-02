namespace CommonFunctions
{
    public static class CF
    {
        public static void Print<T>(T[,] matrix)
        { 
            long r = matrix.GetLength(0);

            long c = matrix.GetLength(1);

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write($"{matrix[i,j],5}");
                }
                Console.WriteLine();
            }
        }

        public static void Print<T>(IEnumerable<T> col)
        {
            foreach (var item in col)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static void PrintInLine<T>(IEnumerable<T> col)
        {
            foreach (var item in col)
            {
                Console.Write($"{item, 4}");
            }
        }

        public static string PrintInLine<T>(IEnumerable<T> col, int dist)
        {
            var str = String.Empty;

            var distance = String.Empty;

            for (int i = 0; i < dist; i++)
            {
                distance += " ";
            }

            foreach (var item in col)
            {
                str += item + distance;
            }

            return str;
        }

        public static void PrintValue<T>(T v, string str = "")
        {

            Console.WriteLine($"{str} {v}");

        }

        public static void TaskShow(string str)
        {
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(str);

            Console.ResetColor();
        }

        public static void PrintMessage(string str, ConsoleColor c)
        {
            Console.WriteLine();

            Console.ForegroundColor = c;

            Console.WriteLine(str);

            Console.ResetColor();
        }

        public static List<int> CreateNumbArray(int size, int start = -200, int end = 200)
        {
            Random r = new Random();

            var t = new List<int>();

            for (int i = 0; i < size; i++)
            {
                t.Add(r.Next(start, end));
            }

            return t;
        }
    }
}