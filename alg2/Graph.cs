using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alg2
{
        public class Graph
        {
            private const int sizeHeap = 3;
            private const int infinity = 1000001;
            private const int minWeightOfEdge = 1;
            private const int maxWeightOfEdge = 1000000;
            private const int countNode = 1000;
            private const int Step = 1000;
            private const int firstStep = 100000;
            private const int secondStep = 1000;

            private class Node
            {
                public int name;
                public int weight;
                public Node next;
                public Node(int name, int weight, Node next)
                {
                    this.name = name;
                    this.weight = weight;
                    this.next = next;
                }
            }

            private class Result
            {
                public int[] dist;

                public int[] up;

                public Result(int length)
                {
                    dist = new int[length];
                    up = new int[length];
                }
            }

            private Node[] graph;

            private Result result;

            internal HeapD heap;

            private Random random = new Random();

            public Graph()
            {

            }

            private Node AddEdge(Node node, int countEdge, int maxIndexNode)
            {
                if (countEdge != 0)
                {
                    if (node != null)
                    {
                        node.next = AddEdge(node.next, countEdge, maxIndexNode);
                    }
                    else
                    {
                        int weight = random.Next(minWeightOfEdge, maxWeightOfEdge);
                        int name = random.Next(0, maxIndexNode);
                        node = new Node(name, weight, null);
                        countEdge--;
                        node.next = AddEdge(node.next, countEdge, maxIndexNode);
                    }
                }
                return node;
            }
            private Node CustomAddEdge(Node node, int countEdge, int maxIndexNode, int q, int r)
            {
                if (countEdge != 0)
                {
                    if (node != null)
                    {
                        node.next = CustomAddEdge(node.next, countEdge, maxIndexNode, q, r);
                    }
                    else
                    {
                        int weight = random.Next(q, r);
                        int name = random.Next(0, maxIndexNode);
                        node = new Node(name, weight, null);
                        countEdge--;
                        node.next = CustomAddEdge(node.next, countEdge, maxIndexNode, q, r);
                    }
                }
                return node;
            }
            public List<int>[] getResult1()
            {
                List<int> timeWork = new List<int>();
                List<int> timeWork1 = new List<int>();
                List<int>[] FullTime = new List<int>[2];
                int count = countNode;


                for (int i = 0; i < 100; i++)
                {
                    result = new Result(count);
                    graph = new Node[count];
                    heap = new HeapD(sizeHeap, count);
                    int countEdge = 100;
                    for (int j = 0; j < graph.Length; j++)
                    {
                        graph[j] = AddEdge(graph[j], countEdge, count - 1);
                    }
                    count += Step;
                    DateTime start = DateTime.Now;
                    DijkstraDHeap(0);
                    DateTime end = DateTime.Now;
                    timeWork.Add((end - start).Milliseconds + (end - start).Seconds * 1000);
                    DateTime start1 = DateTime.Now;
                    FordBellman(0);
                    DateTime end1 = DateTime.Now;

                    timeWork1.Add((end1 - start1).Milliseconds + (end1 - start1).Seconds * 1000);

                }
                FullTime[0] = timeWork;
                FullTime[1] = timeWork1;
                return FullTime;
            }
            public List<int>[] getResult2()
            {
                List<int> timeWork = new List<int>();
                List<int> timeWork1 = new List<int>();
                List<int>[] FullTime = new List<int>[2];
                int count = countNode;


                for (int i = 0; i < 100; i++)
                {
                    result = new Result(count);
                    graph = new Node[count];
                    heap = new HeapD(sizeHeap, count);
                    int countEdge = 1000;
                    for (int j = 0; j < graph.Length; j++)
                    {
                        graph[j] = AddEdge(graph[j], countEdge, count - 1);
                    }
                    count += Step;
                    DateTime start = DateTime.Now;
                    DijkstraDHeap(0);
                    DateTime end = DateTime.Now;
                    timeWork.Add((end - start).Milliseconds + (end - start).Seconds * 1000);
                    DateTime start1 = DateTime.Now;
                    FordBellman(0);
                    DateTime end1 = DateTime.Now;

                    timeWork1.Add((end1 - start1).Milliseconds + (end1 - start1).Seconds * 1000);

                }
                FullTime[0] = timeWork;
                FullTime[1] = timeWork1;
                return FullTime;
            }



            public int CustomBellman(int n, int m, int q, int r)
            {
                int timeWork = 0;
                int count = n;
                result = new Result(count);
                graph = new Node[count];
                int countEdge = m;
                for (int j = 0; j < graph.Length; j++)
                {
                    graph[j] = CustomAddEdge(graph[j], countEdge, count - 1, q, r);
                }
                DateTime start = DateTime.Now;
                FordBellman(0);
                DateTime end = DateTime.Now;
                timeWork = (end - start).Milliseconds + (end - start).Seconds * 1000;

                return timeWork;
            }
            public int CustomDeykstra(int n, int m, int q, int r)
            {

                int timeWork = 0;
                int count = n;


                    result = new Result(count);
                    graph = new Node[count];
                    heap = new HeapD(sizeHeap, count);
                    int countEdge = m;
                    for (int j = 0; j < graph.Length; j++)
                    {
                        graph[j] = CustomAddEdge(graph[j], countEdge, count - 1, q, r);
                    }
                    DateTime start = DateTime.Now;
                    DijkstraDHeap(0);
                    DateTime end = DateTime.Now;
                    timeWork = (end - start).Milliseconds + (end - start).Seconds * 1000;
                
                return timeWork;
            }


            public void DijkstraDHeap(int indexNode)
            {
                heap.countNotVisit = heap.names.Length;
                for (int i = 0; i < heap.countNotVisit; i++)
                {
                    result.up[i] = 0;
                    result.dist[i] = infinity;
                    heap.names[i] = i;
                    heap.keys[i] = infinity;
                    heap.index[i] = i;
                }
                if (indexNode >= result.dist.Length)
                {
                    return;
                }
                heap.keys[indexNode] = 0;
                heap.Emersion(indexNode);
                while (heap.countNotVisit > 0)
                {
                    int i = heap.names[0];
                    result.dist[i] = heap.keys[0];
                    heap.DeliteMin();
                    Node node = graph[i];
                    while (node != null)
                    {
                        int j = node.name;
                        int index = heap.index[j];
                        if (result.dist[j] == infinity)
                        {
                            if (heap.keys[index] > result.dist[i] + node.weight)
                            {
                                heap.keys[index] = result.dist[i] + node.weight;
                                heap.Emersion(index);
                                result.up[j] = i;
                            }
                        }
                        node = node.next;
                    }
                }
            }

            public void FordBellman(int indexNode)
            {
                for (int i = 0; i < result.dist.Length; i++)
                {
                    result.dist[i] = infinity;
                    result.up[i] = 0;
                }
                if (indexNode >= result.dist.Length)
                {
                    return;
                }
                result.dist[indexNode] = 0;
                for (; ; )
                {
                    bool any = false;
                    for (int i = 0; i < result.dist.Length; i++)
                    {
                        Node node = graph[i];
                        while (node != null)
                        {
                            int j = node.name;
                            if (result.dist[i] < infinity)
                            {
                                if (j != indexNode)
                                {
                                    if (result.dist[j] > result.dist[i] + node.weight)
                                    {
                                        result.dist[j] = result.dist[i] + node.weight;
                                        result.up[j] = i;
                                        any = true;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                            node = node.next;
                        }
                    }
                    if (!any) break;
                }
            }
        }
    }
