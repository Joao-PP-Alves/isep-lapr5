using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DDDNetCore.Network
{
    public class Vertex<V, E>
    {
        public int key { get; set; }
        private V element { get; set; }
        private Dictionary<V, Edge<V, E>> outVerts;

        public void AddAdjVert(V vAdj, Edge<V, E> edge)
        {
            outVerts.Add(vAdj, edge);
        }

        public void RemAdjVert(V vAdj)
        {
            outVerts.Remove(vAdj);
        }

        public Edge<V, E> getEdge(V vAdj)
        {
            return outVerts[vAdj];
        }

        public int numAdjVerts()
        {
            return outVerts.Count;
        }

        public IList<V> getAllAdjVerts()
        {
            return outVerts.Keys.ToList();
        }

        public ICollection<Edge<V, E>> getAllOutEdges()
        {
            return outVerts.Values;
        }


        public bool equals(Object otherObj)
        {

            if (this == otherObj)
            {
                return true;
            }

            if (otherObj == null || this.GetType().Name != otherObj.GetType().Name)
                return false;

            Vertex<V, E> otherVertex = (Vertex<V, E>) otherObj;

            if (this.key != otherVertex.key)
                return false;

            if (this.element != null && otherVertex.element != null &&
                !this.element.Equals(otherVertex.element))
                return false;

            //adjacency vertices should be equal
            if (this.numAdjVerts() != otherVertex.numAdjVerts())
                return false;

            //and edges also
            ICollection<Edge<V, E>> it1 = this.getAllOutEdges();
            foreach (object edge in it1)
            {
                ICollection<Edge<V, E>> it2 = otherVertex.getAllOutEdges();
                bool exists = false;
                foreach (var edge2 in it2)
                {
                    if (it1.Equals(it2))
                        exists = true;
                }

                if (!exists)
                    return false;
            }

            return true;
        }

        public Vertex<V, E> clone()
        {

            Vertex<V, E> newVertex = new Vertex<V, E>();


            newVertex.key = key;
            newVertex.element = element;

            foreach (V vert in outVerts.Keys)
                newVertex.AddAdjVert(vert, this.getEdge(vert));

            return newVertex;
        }

    }
}