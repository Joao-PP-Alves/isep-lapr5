using System.Collections.Generic;

namespace DDDNetCore.Network
{
    public interface GraphInterface <V,E>
    {

     //returns the number of vertices of the graph
    int NumVertices();

    //returns all the vertices of the graph as an iterable collection
    IList<V> Vertices();

    //returns the number o f edges
    int NumEdges();

    //returns the information of all the esges of the graph as an iterable collection
    IList<Edge<V,E>> Edges();

    /**
     * returns the vertices of the edge e as an array of length two
     * if the graph is directed, the first vertex is the origin, and
     * the second is the destination
     * if the graph is undirected, the order is arbitary
     * @param edge
     * @return array os two vertices or null if the edge doesn't exist
     */
    V[] EndVertices (Edge<V,E> Edge);

    /** Returns the vertex that is opposite vertex v on edge e.
     * @param vert
     * @param edge
     * @return opposite vertex, or null if vertex or edge don't exist
     */
    V Opposite(V vert, Edge<V,E> edge);

    /**
     * Returns the number of edges leaving vertex v
     * For an undirected graph, this is the same result returned by inDegree
     * @param vert
     * @return number of edges leaving vertex v, -1 if vertex doesn't exist
     */
    int OutDegree(V vert) ;

    /**
     * Returns the number of edges for which vertex v is the destination
     * For an undirected graph, this is the same result returned by outDegree
     * @param vert
     * @return number of edges leaving vertex v, -1 if vertex doesn't exist
     */
    int InDegree(V vert) ;

    /** Returns an iterable collection of edges for which vertex v is the origin
     * for an undirected graph, this is the same result returned by incomingEdges
     * @param vert
     * @return iterable collection of edges, null if vertex doesn't exist
     */
    IList<Edge<V,E>> OutgoingEdges (V vert);

    /** Returns an iterable collection of edges for which vertex v is the destination
     * For an undirected graph this is the same result as returned by incomingEdges
     * @param vert
     * @return iterable collection of edges reaching vertex, null if vertex doesn't exist
     */
    IList<Edge<V,E>> IncomingEdges(V vert);

    /** Inserts a new vertex with some specific comparable type
     * @param newVert the vertex contents
     * @return a true if insertion suceeds, false otherwise
     */
    bool InsertVertex(V newVert);

    /** Adds a new edge between vertices u and v, with some
     * specific comparable type. If vertices u, v don't exist in the graph they
     * are inserted
     * @param vOrig Information of vertex source
     * @param vDest Information of vertex destination
     * @param edge edge information
     * @param eWeight edge weight
     * @return true if suceeds, or false if an edge already exists between the two verts.
     */
    bool InsertEdge(V vOrig, V vDest, E edge, double eWeight);


    /** Removes a vertex and all its incident edges from the graph
     * @param vert Information of vertex source
     */
    bool RemoveVertex(V vert);

    /**Removes the edge between two vertices
     *
     * @param vOrig Information of vertex source
     * @param vDest Information of vertex destination
     */
    bool RemoveEdge(V vOrig, V vDest);

}

    }