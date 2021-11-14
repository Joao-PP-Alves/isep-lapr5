using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DDDNetCore.Network
{
    public class Network<V, E> : GraphInterface<V, E>
    {

        private int NumVert;
        private int NumEdge;
        private bool IsDirected;
        private Dictionary<V, Vertex<V, E>> VerticesC; //all Vertices of the graph

        // Constructs an empty graph (either undirected or directed)
        public Network(bool directed)
        {
            NumVert = 0;
            NumEdge = 0;
            IsDirected = directed;
            VerticesC = new Dictionary<V, Vertex<V, E>>();
        }

        public int NumVertices()
        {
            return NumVert;
        }

        public IList<V> Vertices()
        {
            return VerticesC.Keys.ToList();
        }

        public bool ValidVertex(V vert)
        {

            if (VerticesC[vert] == null)
                return false;

            return true;
        }

        public int GetKey(V vert)
        {
            return VerticesC[vert].key;
        }

        public IList<V> adjVertices(V vert)
        {

            if (!ValidVertex(vert))
                return null;

            Vertex<V, E> vertex = VerticesC[vert];

            return vertex.getAllAdjVerts();
        }


        public int NumEdges()
        {
            return NumEdge;
        }


        public IList<Edge<V, E>> Edges()
        {
            IList<Edge<V, E>> listEdges = new List<Edge<V, E>>();

            foreach (Vertex<V, E> vert in VerticesC.Values)
            {
                foreach (Edge<V, E> edge in vert.getAllOutEdges())
                {
                    listEdges.Add(edge);
                }
            }

            return listEdges;
        }

        public Edge<V, E> GetEdge(V vOrig, V vDest)
        {

            if (!ValidVertex(vOrig) || !ValidVertex(vDest))
                return null;

            Vertex<V, E> vorig = VerticesC[vOrig];

            return vorig.getEdge(vDest);
        }


        public V[] EndVertices(Edge<V, E> edge)
        {

            if (edge == null)
                return null;

            if (!ValidVertex(edge.vOrig.element) || !ValidVertex(edge.vDest.element))
                return null;

            Vertex<V, E> vorig = VerticesC[edge.vOrig.element];

            if (!edge.Equals(vorig.getEdge(edge.vDest.element)))
                return null;

            return edge.GetEndpoints();
        }

        //gets the vertice that is in the end of the common edge
        public object Opposite(V vert, Edge<V, E> edge)
        {

            if (!ValidVertex(vert))
                return null;

            Vertex<V, E> vertex = VerticesC[vert];

            return (V) vertex.GetAdjVert(edge);
        }

        public int OutDegree(V vert)
        {
            throw new NotImplementedException();
        }

        public int outDegree(V vert)
        {

            if (!ValidVertex(vert))
                return -1;

            Vertex<V, E> vertex = VerticesC[vert];

            return vertex.numAdjVerts();
        }

        public int InDegree(V vert)
        {

            if (!ValidVertex(vert))
                return -1;

            int degree = 0;
            foreach (V otherVert in VerticesC.Keys)
                if (GetEdge(otherVert, vert) != null)
                    degree++;

            return degree;
        }

        public IList<Edge<V, E>> OutgoingEdges(V vert)
        {

            if (!ValidVertex(vert))
                return null;

            Vertex<V, E> vertex = VerticesC[vert];

            return (IList<Edge<V, E>>) vertex.getAllOutEdges();
        }

        public IList<Edge<V, E>> IncomingEdges(V vert)
        {
            IList<Edge<V, E>> listIncomingEdges = new List<Edge<V, E>>();
            foreach (Vertex<V, E> verti in VerticesC.Values)
            {
                foreach (Edge<V, E> edge in verti.getAllOutEdges())
                {
                    if (edge.vDest.equals(vert))
                    {
                        listIncomingEdges.Add(edge);
                    }
                }
            }

            return listIncomingEdges;
        }

        public bool InsertVertex(V vert)
        {

        /*    if (ValidVertex(vert))
                return false; */

            Vertex<V, E> vertex = new Vertex<V, E>(NumVert, vert);
            VerticesC[vert] = vertex;
            NumVert++;

            return true;
        }

        public bool InsertEdge(V vOrig, V vDest, E eInf, double eWeight)
        {

            if (GetEdge(vOrig, vDest) != null)
                return false;

            if (!ValidVertex(vOrig))
                InsertVertex(vOrig);

            if (!ValidVertex(vDest))
                InsertVertex(vDest);

            Vertex<V, E> vorig = VerticesC[vOrig];
            Vertex<V, E> vdest = VerticesC[vDest];

            Edge<V, E> newEdge = new Edge<V, E>(eInf, eWeight, vorig, vdest);
            vorig.AddAdjVert(vDest, newEdge);
            NumEdge++;

            //if graph is not direct insert other edge in the opposite direction
            if (!IsDirected)
                // if vDest different vOrig
                if (GetEdge(vDest, vOrig) == null)
                {
                    Edge<V, E> otherEdge = new Edge<V, E>(eInf, eWeight, vdest, vorig);
                    vdest.AddAdjVert(vOrig, otherEdge);
                    NumEdge++;
                }

            return true;
        }

        public bool RemoveVertex(V vert)
        {

            if (!ValidVertex(vert))
                return false;

            //remove all edges that point to vert
            foreach (Edge<V, E> edge in IncomingEdges(vert))
            {
                V vadj = edge.vOrig.element;
                RemoveEdge(vadj, vert);
            }

            Vertex<V, E> vertex = VerticesC[vert];

            //update the keys of subsequent vertices in the map
            foreach (Vertex<V, E> v in VerticesC.Values)
            {
                int keyVert = v.key;
                if (keyVert > vertex.key)
                {
                    keyVert = keyVert - 1;
                    v.key = keyVert;
                }
            }

            //The edges that live from vert are removed with the vertex
            VerticesC.Remove(vert);

            NumVert--;

            return true;
        }

        public bool RemoveEdge(V vOrig, V vDest)
        {

            if (!ValidVertex(vOrig) || !ValidVertex(vDest))
                return false;

            Edge<V, E> edge = GetEdge(vOrig, vDest);

            if (edge == null)
                return false;

            Vertex<V, E> vorig = VerticesC[vOrig];

            vorig.RemAdjVert(vDest);
            NumEdge--;

            //if graph is not direct
            if (!IsDirected)
            {
                edge = GetEdge(vDest, vOrig);
                if (edge != null)
                {
                    Vertex<V, E> vdest = VerticesC[vDest];
                    vdest.RemAdjVert(vOrig);
                    NumEdge--;
                }
            }

            return true;
        }


        //Returns a clone of the graph
        public Network<V, E> clone()
        {

            Network<V, E> newObject = new Network<V, E>(this.IsDirected);

            //insert all vertices
            foreach (V vert in VerticesC.Keys)
                newObject.InsertVertex(vert);

            //insert all edges
            foreach (V vert1 in VerticesC.Keys)
            foreach (Edge<V, E> e in this.OutgoingEdges(vert1))
                if (e != null)
                {
                    V vert2 = (V) this.Opposite(vert1, e);
                    newObject.InsertEdge(vert1, vert2, e.element, e.weight);
                }

            return newObject;
        }

        /* equals implementation
         * @param the other graph to test for equality
         * @return true if both objects represent the same graph
         */
        public bool Equals(Object otherObj)
        {

            if (this == otherObj)
                return true;

            if (otherObj == null || this.GetType().Name != otherObj.GetType().Name)
                return false;

            Network<V, E> otherNetwork = (Network<V, E>) otherObj;

            if (NumVert != otherNetwork.NumVertices() || NumEdge != otherNetwork.NumEdges())
                return false;

            //graph must have same vertices
            bool eqvertex;
            foreach (KeyValuePair<V, Vertex<V, E>> v1 in this.VerticesC)
            {
                eqvertex = false;
                foreach (KeyValuePair<V, Vertex<V, E>> v2 in otherNetwork.VerticesC)
                    if (v1.Equals(v2))
                        eqvertex = true;

                if (!eqvertex)
                    return false;
            }

            return true;
        }

        //string representation
        public String toString()
        {
            String s = "";
            if (NumVert == 0)
            {
                s = "\nGraph not defined!!";
            }
            else
            {
                s = "Graph: " + NumVert + " vertices, " + NumEdge + " edges\n";
                foreach (Vertex<V, E> vert in VerticesC.Values)
                    s += vert + "\n";
            }

            return s;
        }
    }
}