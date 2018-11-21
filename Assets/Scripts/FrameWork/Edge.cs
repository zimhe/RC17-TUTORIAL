using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class Edge
{
    private int _start;
    private int _end;

    private List<int> _triangles;

    public int index { get; set; }

    public Edge(int v0,int v1)
    {
        _start = v0;
        _end = v1;
        _triangles=new List<int>(2);
    }

    public void SetConnectedTriangle(int triangle)
    {
        _triangles.Add(triangle);
    }

    public List<int> GetConnectedTriangles()
    {
        return _triangles;
    }

    public int Start()
    {
        return _start;
    }

    public int End()
    {
        return _end;
    }

    public int GetOppositeVertex(int vertex)
    {
        return vertex == _start ? _end : vertex == _end ? _start : -1;
    }
}
