using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DDDNetCore.Network
{
  /*  public class Network<V,E> : GraphInterface<V,E> {

    private int NumVert;
    private int NumEdge;
    private bool IsDirected;
    private Dictionary<V, Vertex<V,E>> VerticesC;  //all Vertices of the graph

    // Constructs an empty graph (either undirected or directed)
    public Network(bool directed) {
        NumVert=0;
        NumEdge=0;
        IsDirected=directed;
        VerticesC = new Dictionary<V,Vertex<V,E>>();
    }

    public int NumVertices(){
        return NumVert;
    }

    public IList<V> Vertices()
    {
        return VerticesC.Keys.ToList();
    }

    public bool ValidVertex(V vert) {

        if (VerticesC[vert] == null)
            return false;

        return true;
    }

    public int GetKey(V vert)
    {
        return VerticesC[vert].key;
    }

    public IList<V> adjVertices(V vert){

        if (!ValidVertex(vert))
            return null;

        Vertex<V,E> vertex = VerticesC[vert];

        return vertex.getAllAdjVerts();
    }


    public int NumEdges(){
        return NumEdge;
    }


    public IList<Edge<V,E>> Edges()
    {
        IList<Edge<V, E>> listEdges = new List<Edge<V, E>>();

        foreach (Vertex<V,E> vert  in VerticesC.Values) {
            foreach (Edge<V,E> edge in vert.getAllOutEdges()) {
                listEdges.Add(edge);
            }
        }
        return listEdges;
    }

    public Edge<V,E> GetEdge(V vOrig, V vDest){

        if (!ValidVertex(vOrig) || !ValidVertex(vDest))
            return null;

        Vertex<V,E> vorig = VerticesC[vOrig];

        return vorig.getEdge(vDest);
    }

    public V[] EndVertices(Edge<V,E> edge){

        if (edge == null)
            return null;

        if (!ValidVertex(edge.vOrig.) || !ValidVertex(edge.vDest))
            return null;

        Vertex<V,E> vorig = vertices.get(edge.getVOrig());

        if (!edge.equals(vorig.getEdge(edge.getVDest())))
            return null;

        return edge.getEndpoints();
    }

    //gets the vertice that is in the end of the common edge
    public V Opposite(V vert, Edge<V,E> edge){

        if (!validVertex(vert))
            return null;

        Vertex<V,E> vertex = vertices.get(vert);

        return vertex.getAdjVert(edge);
    }

    public int OutDegree(V vert)
    {
        throw new NotImplementedException();
    }

    public int outDegree(V vert){

        if (!validVertex(vert))
            return -1;

        Vertex<V,E> vertex = vertices.get(vert);

        return vertex.numAdjVerts();
    }

    public int InDegree(V vert){

        if (!validVertex(vert))
            return -1;

        int degree=0;
        for (V otherVert : vertices.keySet())
            if (getEdge(otherVert,vert) != null)
                degree++;

        return degree;
    }

    public IEnumerable<Edge<V,E>> OutgoingEdges(V vert){

        if (!validVertex(vert))
            return null;

        Vertex<V,E> vertex = vertices.get(vert);

        return vertex.getAllOutEdges();
    }

    public IEnumerable<Edge<V,E>> IncomingEdges(V vert){
        ArrayList <Edge<V,E>> listIncomingEdges = new ArrayList<>();
        for(Vertex<V,E> verti :  vertices.values()) {
            for (Edge<V,E> edge : verti.getAllOutEdges()) {
                if(edge.getVDest().equals(vert)) {
                    listIncomingEdges.add(edge);
                }
            }
        }
        return listIncomingEdges;
    }

    public bool InsertVertex(V vert){

        if (validVertex(vert))
            return false;

        Vertex<V,E> vertex = new Vertex<>(numVert,vert);
        vertices.put(vert,vertex);
        numVert++;

        return true;
    }

    public bool InsertEdge(V vOrig, V vDest, E eInf, double eWeight){

        if (getEdge(vOrig,vDest) != null)
            return false;

        if (!validVertex(vOrig))
            insertVertex(vOrig);

        if (!validVertex(vDest))
            insertVertex(vDest);

        Vertex<V,E> vorig = vertices.get(vOrig);
        Vertex<V,E> vdest = vertices.get(vDest);

        Edge<V,E> newEdge = new Edge<>(eInf,eWeight,vorig,vdest);
        vorig.addAdjVert(vDest,newEdge);
        numEdge++;

        //if graph is not direct insert other edge in the opposite direction
        if (!isDirected)
            // if vDest different vOrig
            if (getEdge(vDest,vOrig) == null){
                Edge<V,E> otherEdge = new Edge<>(eInf,eWeight,vdest,vorig);
                vdest.addAdjVert(vOrig,otherEdge);
                numEdge++;
            }

        return true ;
    }

    public bool RemoveVertex(V vert){

        if (!validVertex(vert))
            return false;

        //remove all edges that point to vert
        for (Edge<V,E> edge : incomingEdges(vert)){
            V vadj = edge.getVOrig();
            removeEdge(vadj,vert);
        }

        Vertex<V,E> vertex = vertices.get(vert);

        //update the keys of subsequent vertices in the map
        for (Vertex<V,E> v : vertices.values()){
            int keyVert = v.getKey();
            if ( keyVert > vertex.getKey()){
                keyVert = keyVert-1;
                v.setKey(keyVert);
            }
        }
        //The edges that live from vert are removed with the vertex
        vertices.remove(vert);

        numVert--;

        return true;
    }

    public bool RemoveEdge(V vOrig, V vDest) {

        if (!validVertex(vOrig) || !validVertex(vDest))
            return false;

        Edge<V,E> edge = getEdge(vOrig,vDest);

        if (edge == null)
            return false;

        Vertex<V,E> vorig = vertices.get(vOrig);

        vorig.remAdjVert(vDest);
        numEdge--;

        //if graph is not direct
        if (!isDirected){
            edge = getEdge(vDest,vOrig);
            if (edge != null){
                Vertex<V,E> vdest = vertices.get(vDest);
                vdest.remAdjVert(vOrig);
                numEdge--;
            }
        }
        return true;
    }


    //Returns a clone of the graph
    public Network<V,E> clone() {

        Network<V,E> newObject = new Network<V,E>(this.IsDirected);

        //insert all vertices
        foreach (V vert in Vertices.Keys)
            newObject.InsertVertex(vert);

        //insert all edges
        for (V vert1 in Vertices.keySet())
            for (Edge<V,E> e : this.outgoingEdges(vert1))
                if (e != null){
                    V vert2=this.opposite(vert1,e);
                    newObject.insertEdge(vert1, vert2, e.getElement(), e.getWeight());
                }

        return newObject;
    }

    /* equals implementation
     * @param the other graph to test for equality
     * @return true if both objects represent the same graph
     */
/*    public bool Equals(Object otherObj) {

        if (this == otherObj)
            return true;

        if (otherObj == null || this.GetType().Name != otherObj.GetType().Name)
            return false;

        Network<V,E> otherNetwork = (Network<V,E>) otherObj;

        if (NumVert != otherNetwork.NumVertices() || NumEdge != otherNetwork.NumEdges())
            return false;

        //graph must have same vertices
        bool eqvertex;
        for (V v1 in this.VerticesC()){
            eqvertex=false;
            for (V v2 : otherNetwork.Vertices())
                if (v1.equals(v2))
                    eqvertex=true;

            if (!eqvertex)
                return false;
        }
        return true;
    }

    //string representation
    public String toString() {
        String s="" ;
        if (NumVert == 0) {
            s = "\nGraph not defined!!";
        }
        else {
            s = "Graph: "+ NumVert + " vertices, " + NumEdge + " edges\n";
            foreach (Vertex<V,E> vert in VerticesC.Values)
                s += vert + "\n" ;
        }
        return s ;
    } */
}