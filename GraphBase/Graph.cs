using System;
using System.Collections.Generic;
using System.IO;

namespace DDDNetCore.GraphBase
{

    public class Graph
    {
        private int vertices { get; set; }

        private int edges { get; set; }
        private string text { get; set; }
        private int[,] adjacenceMatrix { get; set; }

        private int[,] incidencyMatrix { get; set; }

        private int[,] mstMatrix { get; set; }
        private int[][] distanceMatrix { get; set; }
        public Graph()
        {
            LoadFile();
            PrintAM();
            PrintIM();
            PrintDM();
            PrintMSTM();
        }

        private void LoadFile()
        {
            Console.Write("Entre com o nome do arquivo: ");
            string file = Console.ReadLine();
            try
            {
                using (StreamReader sr = new StreamReader(file + ".txt"))
                {
                    text = sr.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error in load file.");
                Console.WriteLine(e.ToString());
            }
            if (text != null)
            {
                string[] verticesStrings = text.Split(';');
                vertices = Convert.ToInt16(verticesStrings[0]);
                adjacenceMatrix = new int[vertices, vertices];
                int count = 1;
                for (int line = 0; line < vertices; line++)
                {
                    for (int col = 0; col < vertices; col++)
                    {
                        if (verticesStrings[count] != "\n")
                            adjacenceMatrix[line, col] = Convert.ToInt16(verticesStrings[count]);
                        else
                            col--;
                        count++;
                    }
                }
                CalculateEdges();
                CreateIM();
                CreateDM();
                CreateMSTM();
            }

        }

        private void CreateIM()
        {
            incidencyMatrix = new int[vertices, edges];
            int edgeIndex = 0;
            for (int line = 0; line < vertices; line++)
            {
                for (int col = 0; col < vertices; col++)
                {
                    if (adjacenceMatrix[line, col] == 1)
                    {
                        incidencyMatrix[line, edgeIndex] = 1;
                        incidencyMatrix[col, edgeIndex] = 1;
                        edgeIndex++;
                    }
                }
            }
        }

        private void CreateDM()
        {
            distanceMatrix = new int[vertices][];
            for (int i = 0; i < vertices; i++)
            {
                distanceMatrix[i] = Dijkstra(i);
            }
        }

        private void CreateMSTM()
        { // Cria a matriz da arvore geradora minima
            mstMatrix = new int[vertices, vertices];
            int[] pred = Prim();
            for (int i = 1; i < pred.Length; i++)
            {
                mstMatrix[i, pred[i]] = distanceMatrix[i][pred[i]];
                mstMatrix[pred[i], i] = distanceMatrix[pred[i]][i];
            }
        }

        private int TakeMin(List<int> queue, int[] dist)
        {
            int lesser = 0;
            foreach (int vertex in queue)
            {
                lesser = vertex;
                break;
            }

            foreach (int vertex in queue)
            {
                if (dist[lesser] > dist[vertex])
                    lesser = vertex;
            }
            return lesser;
        }
        private int EdgeWeight(int a, int b)
        {
            if (a == -1 || b == -1)
            {
                return 9999;
            }
            else
                return adjacenceMatrix[a, b];
        }

        private int ExtractDictionary(Dictionary<int, int> dict)
        {
            int lesser = 0;

            List<int> list = new List<int>(dict.Keys);

            foreach (int vertex in list)
            {
                lesser = vertex;
                break;
            }


            foreach (int vertex in list)
            {
                if (dict[lesser] > dict[vertex])
                    lesser = vertex;
            }

            return lesser;
        }

        public void PrintAM()
        {
            for (int line = 0; line < vertices; line++)
            {
                for (int col = 0; col < vertices; col++)
                {
                    Console.Write(adjacenceMatrix[line, col].ToString() + ";");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public void PrintMSTM()
        {
            for (int line = 0; line < vertices; line++)
            {
                for (int col = 0; col < vertices; col++)
                {
                    Console.Write(mstMatrix[line, col].ToString() + ";");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public void PrintIM()
        {
            for (int line = 0; line < vertices; line++)
            {
                for (int col = 0; col < edges; col++)
                {
                    Console.Write(incidencyMatrix[line, col].ToString() + ";");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public void PrintDM()
        {
            for (int line = 0; line < vertices; line++)
            {
                for (int col = 0; col < vertices; col++)
                {
                    Console.Write(distanceMatrix[line][col].ToString() + ";");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public void CalculateEdges()
        {
            edges = 0;
            for (int line = 0; line < vertices; line++)
            {
                for (int col = 0; col < vertices; col++)
                {
                    if (adjacenceMatrix[line, col] == 1)
                        edges++;
                }
            }
        }

        public int[,] GetMatrix()
        {
            return adjacenceMatrix;
        }

        public int[] GetGraph()
        {
            int[] graph = new int[2];
            graph[0] = vertices;
            graph[1] = edges;
            return graph;
        }

        public int[] Dijkstra(int start)
        {
            int infinite = 9999;
            List<int> queue = new List<int>();
            int[] pred = new int[vertices];
            int[] dist = new int[vertices];
            for (int vertex = 0; vertex < pred.Length; vertex++)
            {
                pred[vertex] = -1;
                if (vertex != start)
                    dist[vertex] = infinite;
                queue.Add(vertex);
            }
            while (queue.Count > 0)
            {
                int u = TakeMin(queue, dist);
                queue.Remove(u);
                int[] neighbors = ReturnNeighbors(u);
                for (int v = 0; v < neighbors.Length; v++)
                {
                    if (neighbors[v] >= 1)
                    {
                        int aux = neighbors[v] + dist[u];
                        if (aux < dist[v])
                        {
                            dist[v] = aux;
                            pred[v] = u;
                        }
                    }
                }
            }
            return dist;

        }

        public int[] ReturnNeighbors(int vertex)
        {
            int[] neighbors = new int[vertices];
            for (int i = 0; i < neighbors.Length; i++)
            {
                neighbors[i] = adjacenceMatrix[vertex, i];
            }
            return neighbors;
        }

        public int[] Prim()
        { // Só funciona para grafos conexos / Porque o VERTICE escolhido eh o 0, entao se o 0 nao estiver conexo.../ 
            Dictionary<int, int> queue = new Dictionary<int, int>();
            List<int> explored = new List<int>();
            int[] pred = new int[vertices];
            for (int vertex = 0; vertex < pred.Length; vertex++)
            {
                pred[vertex] = -1;
            }
            queue.Add(0, 0); // Adicionando qualquer véritice. Peso inicial 0. Dic(vertex,weight) ~~ (key,value)

            while (queue.Count > 0)
            {
                int v = ExtractDictionary(queue);
                queue.Remove(v);
                explored.Add(v);
                int[] neighbors = ReturnNeighbors(v);
                for (int u = 0; u < neighbors.Length; u++)
                {
                    if (neighbors[u] >= 1)
                    {
                        if (!explored.Contains(u) && EdgeWeight(pred[u], u) > EdgeWeight(v, u))
                        {
                            if (queue.ContainsKey(u))
                                queue[u] = EdgeWeight(v, u);
                            else
                                queue.Add(u, EdgeWeight(v, u));
                            pred[u] = v;
                        }
                    }
                }
            }

            /*			Console.WriteLine ("Predecessores:  ");
                        for (int vertex = 0; vertex < pred.Length; vertex++) {
                            Console.WriteLine ("Predecessor de " + vertex.ToString () + " eh " + pred [vertex]);
                        }*/
            return pred;
        }

    }
}