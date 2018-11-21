using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    private List<int> _vertices;

    public int index { get; set; }

    public Triangle(int v0, int v1, int v2)
    {
        _vertices = new List<int>();

        _vertices.Add(v0);
        _vertices.Add(v1);
        _vertices.Add(v2);
    }

    public List<int> GetVertices()
    {
        return _vertices;
    }

}
