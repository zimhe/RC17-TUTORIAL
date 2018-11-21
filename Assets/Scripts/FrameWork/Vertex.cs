using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpatialSlur.SlurCore;
using SpatialSlur.SlurData;
using UnityEngine;

public class Vertex
{
    private int _index;

    private List<int> _connectedVertices;

    private List<int> _connectedEdges;

    private List<int> _connectecTriangles;

    public Vertex(int index)
    {
        _index = index;

        _connectedVertices=new List<int>(4);
        _connectedEdges=new List<int>(4);
        _connectecTriangles=new List<int>(6);
    }

    public int GetIndex()
    {
        return _index;
    }

    public void AddConnectedVertex(int vertex)
    {
        _connectedVertices.Add(vertex);
    }

    public void AddConnectedEdges(int edge)
    {
        _connectedEdges.Add(edge);
    }

    public void AddConnectedTriangles(int triangle)
    {
        _connectecTriangles.Add(triangle);
    }

    public IEnumerable<int> GetConnectedVertexs()
    {
        foreach (var v in _connectedVertices)
        {
            yield return v;
        }
    }

    public IEnumerable<int> GetConnectedEdges()
    {
        foreach (var e in _connectedEdges)
        {
            yield return e;
        }
    }

    public IEnumerable<int> GetConnectedTriangles()
    {
        return _connectecTriangles;
    }
	
}
