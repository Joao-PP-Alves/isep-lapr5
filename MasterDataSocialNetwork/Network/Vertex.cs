using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DDDNetCore.Network
{
    public class Vertex<V, E>
    {
        public int key { get; set; }
        public V element { get; set; }
        public Dictionary<V, Edge<V, E>> outVerts;

        public Vertex () {
            key = -1; element = default(V); outVerts = new Dictionary<V, Edge<V, E>>();
        }

        public Vertex (int k, V vInf) {
            key = k; element = vInf; outVerts = new Dictionary<V, Edge<V, E>>();
        }

        public Vertex (Vertex<V,E> v) {
            key = v.key;
            element = v.element;
            outVerts = new Dictionary<V, Edge<V, E>>();
            foreach (V vert in v.outVerts.Keys){
                Edge<V,E> edge = v.outVerts[vert];
                outVerts[vert]=edge;
            }
        }

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

        public IList getAllOutEdges()
        {
            return outVerts.Values.ToList();
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
            IList it1 = this.getAllOutEdges();
            foreach (object edge in it1)
            {
                IList it2 = otherVertex.getAllOutEdges();
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
        
        public object GetAdjVert(Edge<V,E> edge){

            foreach (V vert in outVerts.Keys )
            if (edge.Equals(outVerts[vert]))
                return vert;

            return null;
        }

    }
}