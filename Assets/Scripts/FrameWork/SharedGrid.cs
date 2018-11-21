using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedGrid<V,E> : ScriptableObject

where V: VertexObject
where E: EdgeObject
{
    private Grid _grid;
    private List<V> _vertexObject;
    private List<E> _edgeObject;


    public Grid GetGrid()
    {
        return _grid;
    }

    public List<V> VertexObject()
    {
        return _vertexObject;
    }

    public List<E> EdgeObject()
    {
        return _edgeObject;
    }

    public void Initialize(Grid grid)
    {
        _grid = grid;
        _vertexObject=new List<V>();
        _edgeObject=new List<E>();
    }

}
