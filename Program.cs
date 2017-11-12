using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DSC_to_DSK
{
    class Program
    {
        public static LinkedList<int>[] dsc;
        public static LinkedList<Tuple<int, int>>[] dsk;
        public static int[,] matrix;
        public static int n;
        public static int soCanh;
        public static int xp;
        public static int kt;
        public static void SetUp()
        {
            matrix = new int[n, n];
            dist = new int[n];
            pre = new int[n];
            label = new bool[n];
            q = new Queue<int>();
            visited = new bool[n];
            CreateMatrix();
        }
        public static void Input()
        {
            StreamReader sr = new StreamReader("DIJKSTRA.INP");
            string[] chuoi = sr.ReadLine().Trim().Split(' ');
            n = int.Parse(chuoi[0]);
            soCanh = int.Parse(chuoi[1]);
            xp = int.Parse(chuoi[2]);
            kt = int.Parse(chuoi[3]);
            dsc = new LinkedList<int>[soCanh];
            SetUp();
            for (int i = 0; i < soCanh; i++)
            {
                string[] s = sr.ReadLine().Trim().Split(' ');
                dsc[i] = new LinkedList<int>();
                for (int j = 0; j < s.Count(); j++)
                {
                    dsc[i].AddLast(int.Parse(s[j]));
                }
                int d = int.Parse(s[0]) - 1;
                int c = int.Parse(s[1]) - 1;
                int ts = int.Parse(s[2]);
                matrix[d, c] = matrix[c, d] = ts;
            }
            sr.Close();
        }
        public static void Output()
        {
            for (int i = 0; i < soCanh; i++)
            {
                foreach (var item in dsc[i])
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }
        public static void CreateMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    matrix[i, j] = matrix[j, i] = -1;
                }
            }
        }
        public static void OutPutMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == -1 || matrix[i, j] > 9)
                        Console.Write(matrix[i, j] + " ");
                    else
                        Console.Write(" " + matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static void Transfer()
        {
            dsk = new LinkedList<Tuple<int, int>>[n];
            for (int i = 0; i < n; i++)
            {
                dsk[i] = new LinkedList<Tuple<int, int>>();
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] != -1)
                        dsk[i].AddLast(new Tuple<int, int>(j + 1, matrix[i, j]));
                }
            }
        }
        public static void OP()
        {
            for (int i = 0; i < n; i++)
            {
                foreach (var item in dsk[i])
                    Console.Write(item.Item1 + " " + item.Item2 + " ");
                Console.WriteLine();
            }
        }

        public static bool[] visited;
        public static Queue<int> q;
        public static int[] dist;
        public static int[] pre;
        public static bool[] label;
        public static void DFS(int x)
        {
            if (visited[x]) return;
            visited[x] = true;
            q.Enqueue(x);
            foreach (var item in dsk[x])
            {
                DFS(item.Item1 - 1);
            }
        }
        public static void PrintDFS()
        {
            Console.Write(">> DFS list: ");
            while (q.Count() != 0)
                Console.Write(q.Dequeue() + 1 + " ");
            Console.WriteLine();
        }
        public static void SetInfiniteAllDistElement()
        {
            for (int i = 0; i < n; i++)
            {
                pre[i] = -1;
                dist[i] = int.MaxValue;
            }
        }
        public static void Solve()
        {
            DFS(xp - 1);
            PrintDFS();
            if (visited[kt - 1] == false)
                Console.WriteLine("No road from {0} to {1}!", xp, kt);
            else
            {
                Dijkstra();
                ShortestRoad(kt - 1);
            }
            Console.WriteLine();
        }
        public static int Min()
        {
            int min = -1;
            for (int i = 0; i < n; i++)
            {
                if (min == -1)
                {
                    if (label[i] == true)
                        continue;
                    else
                        min = i;
                }
                if (dist[i] < dist[min] && label[i] == false)
                    min = i;
            }
            return min;
        }
        public static void Dijkstra()
        {
            SetInfiniteAllDistElement();
            dist[xp - 1] = 0;
            int start = -1;
            while (label[kt - 1] == false)
            {
                start = Min() + 1;
                foreach (var item in dsk[start - 1])
                {
                    if (label[item.Item1 - 1] == false)
                    {
                        if (dist[item.Item1 - 1] > dist[start - 1] + item.Item2)
                        {
                            dist[item.Item1 - 1] = dist[start - 1] + item.Item2;
                            pre[item.Item1 - 1] = start - 1;
                        }
                    }
                }
                label[Min()] = true;
            }
        }
        public static void ShortestRoad(int finish)
        {
            Console.Write("Shortest road are [" + dist[finish] + "] with this road : ");
            Stack<int> road = new Stack<int>();
            while (true)
            {
                road.Push(finish + 1);
                if (pre[finish] == -1)
                    break;
                finish = pre[finish];
            }
            while (road.Count != 0)
                Console.Write(road.Pop() + " ");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Input();
            Output();
            Console.WriteLine("===========================");
            OutPutMatrix();
            Console.WriteLine("===========================");
            Transfer();
            OP();
            Console.WriteLine("===========================");
            Solve();
        }
    }
}
