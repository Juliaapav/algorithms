using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        static void matrixRandom()
        {
            System.Console.WriteLine("Рандомные значения:");
            int[,] circuit = new int[6, 6];
            int[,] circ = new int[6, 6];
            Random random = new Random();
            int edge= random.Next(1, 100);
            circuit[0, 1] = edge;
            circ[0, 1] = edge;
            edge = random.Next(1, 100);
            circuit[0, 2] = edge;
            circ[0, 2] = edge;
            edge = random.Next(1, 100);
            circuit[0, 4] = edge;
            circ[0, 4] = edge;
            edge = random.Next(1, 100);
            circuit[1, 3] = edge;
            circ[1, 3] = edge;
            circ[3, 1] = edge;
            circuit[3, 1] = edge;
            edge = random.Next(1, 100);
            circuit[1, 5] = edge;
            circ[1, 5] = edge;
            edge = random.Next(1, 100);
            circuit[2, 4] = edge;
            circ[2, 4] = edge;
            circ[4, 2] = edge;
            circuit[4, 2] = edge;
            edge = random.Next(1, 100);
            circuit[2, 5] = edge;
            circ[2, 5] = edge;
            edge = random.Next(1, 100);
            circuit[3, 4] = edge;
            circ[3, 4] = edge;
            circ[4, 3] = edge;
            circuit[4, 3] = edge;
            edge = random.Next(1, 100);
            circuit[4, 5] = edge;
            circ[4, 5] = edge;
            int answer = maxFlow(circuit, 0, 5);
            System.Console.WriteLine("Максимальный поток = {0} ", answer);
            int MIN = Int32.MaxValue;
            bool[] used = new bool[6];
            used[0] = true;
            MIN = Int32.MaxValue;
            System.Console.WriteLine("Минимальный разрез = {0} ", minCat(circ, MIN, used,answer));

        }

        static void matrix()
        {
            System.Console.WriteLine("Свои значения:");
            int[,] circuit = new int[6, 6] { {  0,9, 8, 0, 7, 0 }, { 0, 0, 0, 6, 0, 4 }, { 0, 0, 0, 0, 2, 5 }, { 0, 6, 0, 0, 8, 10 }, { 0, 0, 2, 8, 0, 3 }, { 0, 0, 0, 0, 0, 0 } };
            int MaxFlow = maxFlow(circuit, 0, 5);
            System.Console.WriteLine("Максимальный поток = {0}",MaxFlow);
            int MIN = Int32.MaxValue;
            bool[] used = new bool[6];
            used[0] = true;
            int[,] circ = new int[6, 6] { { 0, 9, 8, 0, 7, 0 }, { 0, 0, 0, 6, 0, 4 }, { 0, 0, 0, 0, 2, 5 }, { 0, 6, 0, 0, 8, 10 }, { 0, 0, 2, 8, 0, 3 }, { 0, 0, 0, 0, 0, 0 } };
            System.Console.WriteLine("Минимальный разрез = {0} ",minCat(circ,MIN,used, MaxFlow));
           //System.Console.Read();

        }
        static int minCat(int[,] cir, int MIN, bool[] used,int MaxFlow)
        {
            int sum = 0;
            for (int i=0; i<5; i++)
            {
                sum = 0;
                if (used[i])
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (cir[i, j] != 0 && !used[j])
                                sum = sum + cir[i, j];
                        }
                    }
                    else
                    for (int k = 0; k <= i; k++)
                    {
                            for (int j = 0; j < 6; j++)
                            {
                                if (cir[k, j] != 0 && !used[j])
                                    sum = sum + cir[k, j];
                            }
                    }
                    
                    
                }
                else
                {
                    used[i] = true;
                    i--;
                    continue;
                }
                MIN = Math.Min(MIN, sum);                                                                                                                                                                              
            }                                                                                                                                                                                       if (MIN != MaxFlow) MIN = MaxFlow;
            return MIN;


        }
        static int maxFlow(int[,] cir, int s, int t)
        {
            for (int flow=0; ;)
            {
                int df = findPath(cir, new bool[6], s, t, Int32.MaxValue);
                if (df == 0) return flow;
                flow += df;
            }
        }

        static int findPath(int[,] cir, bool[] vis, int u, int t, int f)
        {
            if (u == t) return f;
            vis[u] = true;
            for (int v=0; v<vis.Length; v++)
                if (!vis[v] && cir[u, v] > 0)
                {
                    int df = findPath(cir, vis, v, t, Math.Min(f, cir[u, v]));
                    if (df > 0)
                    {
                        cir[u, v] -= df;
                        cir[v, u] += df;
                        return df;
                    }
                }
            return 0;
        }

        static void Main(string[] args)
        {

            matrix();
            matrixRandom();
            System.Console.ReadKey();
            
        }
    }
}
