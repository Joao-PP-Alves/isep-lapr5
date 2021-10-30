using System.Collections;
using System.Collections.Generic;

namespace DDDNetCore.GraphBase{
    public interface GraphInterface<V,E> {
        //returns the number of vertices of the graph
        int numVertices();

        IEnumerable<V> vertices();
    }
}