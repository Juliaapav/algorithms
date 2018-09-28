using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{

    public class Graph: GraphA
    {

        private int countEdge;
        private int countNode;
        private List<Edge> edges;
        private List<Edge> ostovTree;

        private enum AlgoritmMethods { Boruvki, Kruskal }

        private class ListNode
        {
            public ListNode parent;
            public int indexNodeGrap;

            public ListNode(ListNode node, int index)
            {
                parent = node;
                indexNodeGrap = index;
            }
        }

        private ListNode FindLeader(int indexListNode)
        {
            if (collectionTrees[indexListNode].parent == null)
            {
                return collectionTrees[indexListNode];
            }
            else
            {
                return collectionTrees[indexListNode] =
                    FindLeader(collectionTrees[indexListNode].parent.indexNodeGrap);
            }
        }

        private ListNode Merge(int indexFirstList, int indexSecondList)
        {
            FindLeader(indexFirstList).parent = collectionTrees[indexSecondList];
            return collectionTrees[indexFirstList];
        }

        private class Edge : IComparable
        {
            public int firstNode;
            public int secondNode;
            public int weight;

            public Edge(int firstNode, int secondNode, int weight)
            {
                this.firstNode = firstNode;
                this.secondNode = secondNode;
                this.weight = weight;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                    return 1;

                if (obj is Edge otherEdge)
                    return weight.CompareTo(otherEdge.weight);
                else
                    throw new ArgumentException("Object is not Edge");
            }
        }

        public Graph()
        {

        }

        ListNode[] collectionTrees;

        private void Boruvki()
        {
            ostovTree = new List<Edge>();
            collectionTrees = new ListNode[countNode];
            int[] sizeTrees = new int[countNode];
            int[] minEdgeForEveryCollection = new int[countNode];

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees[i] = new ListNode(null, i);
                sizeTrees[i] = 1;
                minEdgeForEveryCollection[i] = -1;
            }

            int firstNode;
            int secondNode;
            int nameLiderFirstCollection;
            int nameLiderSecondCollection;

            while (FindIndexMinEdgeForEveryTree(ref minEdgeForEveryCollection))
            {
                for (int s = 0; s < countNode; s++)
                {
                    if (minEdgeForEveryCollection[s] > -1)
                    {
                        firstNode = edges[minEdgeForEveryCollection[s]].firstNode;
                        secondNode = edges[minEdgeForEveryCollection[s]].secondNode;
                        nameLiderFirstCollection = FindLeader(collectionTrees[firstNode].indexNodeGrap).indexNodeGrap;
                        nameLiderSecondCollection = FindLeader(collectionTrees[secondNode].indexNodeGrap).indexNodeGrap;
                        if (nameLiderFirstCollection != nameLiderSecondCollection)
                        {
                            ostovTree.Add(edges[minEdgeForEveryCollection[s]]);
                            if (ostovTree.Count == countNode - 1)
                                return;
                            if (sizeTrees[firstNode] < sizeTrees[secondNode])
                            {
                                collectionTrees[firstNode] = Merge(
                                    collectionTrees[firstNode].indexNodeGrap, collectionTrees[secondNode].indexNodeGrap);
                                sizeTrees[firstNode] += sizeTrees[secondNode];
                            }
                            else
                            {
                                collectionTrees[secondNode] = Merge(
                                    collectionTrees[secondNode].indexNodeGrap, collectionTrees[firstNode].indexNodeGrap);
                                sizeTrees[secondNode] += sizeTrees[firstNode];
                            }
                        }
                        minEdgeForEveryCollection[s] = -1;
                    }
                }
            }
        }

        private bool FindIndexMinEdgeForEveryTree(ref int[] result)
        {
            bool isFindMinEdge = false;
            int firstNode;
            int secondNode;
            int nameLiderFirstCollection;
            int nameLiderSecondCollection;

            for (int i = 0; i < edges.Count; i++) //проходим по ребрам
            {
                firstNode = edges[i].firstNode;
                secondNode = edges[i].secondNode;
                nameLiderFirstCollection = FindLeader(collectionTrees[firstNode].indexNodeGrap).indexNodeGrap;
                nameLiderSecondCollection = FindLeader(collectionTrees[secondNode].indexNodeGrap).indexNodeGrap;
                if (nameLiderFirstCollection != nameLiderSecondCollection)
                {
                    if (result[nameLiderFirstCollection] == -1)
                    {
                        result[nameLiderFirstCollection] = i;
                        isFindMinEdge = true;
                    }
                    else if (edges[result[nameLiderFirstCollection]].weight > edges[i].weight)
                    {
                        result[nameLiderFirstCollection] = i;
                    }
                    if (result[nameLiderSecondCollection] == -1)
                    {
                        result[nameLiderSecondCollection] = i;
                        isFindMinEdge = true;
                    }
                    else if (edges[result[nameLiderSecondCollection]].weight > edges[i].weight)
                    {
                        result[nameLiderSecondCollection] = i;
                    }
                }
            }
            return isFindMinEdge;
        }

        private void Kruskal()
        {
            ostovTree = new List<Edge>();
            collectionTrees = new ListNode[countNode];
            int[] sizeTrees = new int[countNode];
            edges.Sort();

            for (int i = 0; i < countNode; i++)
            {
                collectionTrees[i] = new ListNode(null, i);
                sizeTrees[i] = 1;
            }

            int firstNode;
            int secondNode;
            int nameLiderFirstCollection;
            int nameLiderSecondCollection;
            for (int i = 0; i < edges.Count; i++)
            {
                firstNode = edges[i].firstNode;
                secondNode = edges[i].secondNode;
                nameLiderFirstCollection = FindLeader(collectionTrees[firstNode].indexNodeGrap).indexNodeGrap;
                nameLiderSecondCollection = FindLeader(collectionTrees[secondNode].indexNodeGrap).indexNodeGrap;
                if (nameLiderFirstCollection != nameLiderSecondCollection)
                {
                    ostovTree.Add(edges[i]);
                    if (ostovTree.Count == countNode - 1)
                        return;
                    if (sizeTrees[firstNode] < sizeTrees[secondNode])
                    {
                        collectionTrees[firstNode] = Merge(
                            collectionTrees[firstNode].indexNodeGrap, collectionTrees[secondNode].indexNodeGrap);
                        sizeTrees[firstNode] += sizeTrees[secondNode];
                    }
                    else
                    {
                        collectionTrees[secondNode] = Merge(
                            collectionTrees[secondNode].indexNodeGrap, collectionTrees[firstNode].indexNodeGrap);
                        sizeTrees[secondNode] += sizeTrees[firstNode];
                    }
                }
            }
        }


        private Random random = new Random();

        private List<int> FirstExperiment(AlgoritmMethods algoritmMethods)
        {
            edges = new List<Edge>();
            countNode = countNodeForExperiments;
            List<int> timeWork = new List<int>();
            for (int i = 0; i < 40; i++)
            {
                countNode += Step;
                for (int j = 0; j < countNode*35; j++)
                {
                    int firstNode;
                    int secondNode;
                    do
                    {

                        firstNode = random.Next(0, countNode - 1);
                        secondNode = random.Next(0, countNode - 1);
                    } while (firstNode == secondNode);
                    edges.Add(new Edge(firstNode, secondNode,
                        random.Next(minWeightOfEdge, maxWeightOfEdge)));
                }
                DateTime start = DateTime.Now;
                if (algoritmMethods == AlgoritmMethods.Boruvki)
                    Boruvki();
                else
                    Kruskal();
                DateTime end = DateTime.Now;
                timeWork.Add((end - start).Milliseconds + (end - start).Seconds * 1000);
            }
            return timeWork;
        }

        private List<int> SecondExperiment(AlgoritmMethods algoritmMethods)
        {
            edges = new List<Edge>();
            countNode = countNodeForExperiments;
            List<int> timeWork = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                countNode += Step;
                for (int j = 0; j < countNode * 1000; j++)
                {
                    int firstNode;
                    int secondNode;
                    do
                    {
                        firstNode = random.Next(0, countNode - 1);
                        secondNode = random.Next(0, countNode - 1);
                    } while (firstNode == secondNode);
                    edges.Add(new Edge(firstNode, secondNode,
                        random.Next(minWeightOfEdge, maxWeightOfEdge)));
                }
                DateTime start = DateTime.Now;
                if (algoritmMethods == AlgoritmMethods.Boruvki)
                    Boruvki();
                else
                    Kruskal();
                DateTime end = DateTime.Now;
                timeWork.Add((end - start).Milliseconds + (end - start).Seconds * 1000);
            }
            return timeWork;
        }

        public override List<int> FirstExperimentForFirstMethod()
        {
            return FirstExperiment(AlgoritmMethods.Boruvki);
        }

        public override List<int> FirstExperimentForSecondMethod()
        {
            return FirstExperiment(AlgoritmMethods.Kruskal);
        }

        public override List<int> SecondExperimentForFirstMethod()
        {
            return SecondExperiment(AlgoritmMethods.Boruvki);
        }

        public override List<int> SecondExperimentForSecondMethod()
        {
            return SecondExperiment(AlgoritmMethods.Kruskal);
        }
    }
}