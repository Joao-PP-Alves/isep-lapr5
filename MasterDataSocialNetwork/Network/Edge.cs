using System;
using System.Collections;
using System.Collections.Generic;

namespace DDDNetCore.Network
    {
        public class Edge<V, E>: IComparable, IEnumerable
        {
            public E element {  get; set; }
            public double weight { get; set; }
            public Vertex<V, E> vOrig { get; set; }
            public Vertex<V,E> vDest { get; set; }

            public Edge()
            {
                element = default(E);
                weight = 0;
                vOrig = null;
                vDest = null;
            }

            public Edge(E eInf, double weight, Vertex<V, E> vo, Vertex<V, E> vd)
            {
                element = eInf;
                weight = this.weight;
                vo = vOrig;
                vd = vDest;
            }
            
            public V[] GetEndpoints() {
                //retorna os vértices na extremidade do ramo

                V oElem = default(V), dElem=default(V), typeElem=default(V);

                if (this.vOrig != null)
                    oElem = vOrig.element;

                if (this.vDest != null)
                    dElem = vDest.element;

                if (oElem == null && dElem == null)
                    return null;

                if (oElem != null)          // To get type
                    typeElem = oElem;

                if (dElem != null)
                    typeElem = dElem;

                V[] endverts = (V []) Array.CreateInstance(typeElem.GetType(), 2);

                endverts[0]= oElem;
                endverts[1]= dElem;

                return endverts;
            }
            
            public override bool Equals(Object obj)
            {
                if (this == obj)
                {
                    return true;
                }

                if (obj == null || obj.GetType().Name == this.GetType().Name)
                {
                    return false;
                }

                Edge<V, E> otherEdge = (Edge<V, E>) obj;

                if ((this.vDest == null && otherEdge.vDest!=null) ||
                    (this.vDest != null && otherEdge.vDest == null))
                    return false;
        
                if (this.vOrig != null && otherEdge.vOrig != null && 
                    !this.vOrig.Equals(otherEdge.vOrig))
                    return false;
        
                if (this.vDest != null && otherEdge.vDest!=null && 
                    !this.vDest.Equals(otherEdge.vDest))
                    return false;
      
                if (this.weight != otherEdge.weight)
                    return false;
        
                if (this.element != null && otherEdge.element != null) 
                    return this.element.Equals(otherEdge.element);
        
                return true;
            }

            public IEnumerator GetEnumerator()
            {
                // falta implementar
                throw new NotImplementedException();
            }


            public int CompareTo(Object otherObject) {
        
                Edge<V,E> other = (Edge<V,E>) otherObject ;
                if (this.weight < other.weight)  return -1;
                if (this.weight == other.weight) return 0;
                return 1;
            }
       
            public Edge<V,E> clone() {
        
                Edge<V,E> newEdge = new Edge<V,E>();
        
                newEdge.element = element;
                newEdge.weight = weight;
                newEdge.vOrig = vOrig;
                newEdge.vDest = vDest;
        
                return newEdge;
            }
        }
    }