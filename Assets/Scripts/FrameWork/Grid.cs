using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : IGrid
{
    public static readonly GridFactory Factory=new GridFactory();
    private const int _defualtCapacity = 4;


    private List<Vertex> _verts;
    private List<Edge> _edges;
    private List<Triangle> _triangles;

    public Grid(int capacity = _defualtCapacity)
    {
        _verts=new List<Vertex>(capacity);
        _edges = new List<Edge>(capacity<<1);
        _triangles=new List<Triangle>(_defualtCapacity);
    }

    public int VertexCount
    {
        get { return _verts.Count; }
    }

    public int GetEdgeCount
    {
        get { return _edges.Count >> 1; }
    }

    public int GetDegree(int vertex)
    {
        return _verts[vertex].GetConnectedVertexs().Count();
    }

    public void AddEdge(int v0, int v1)
    {
        var e=new Edge(v0,v1);
       _edges.Add( e);

        e.index = _edges.Count - 1;

        _verts[v0].AddConnectedVertex(v1);
        _verts[v1].AddConnectedVertex(v0);
        _verts[v0].AddConnectedEdges(e.index);
        _verts[v1].AddConnectedEdges(e.index);

    }

    public void AddTriangle(int v0, int v1, int v2)
    {
        var t=new Triangle(v0,v1,v2);
        _triangles.Add(t);

        t.index = _triangles.Count - 1;

        var e0 = GetEdge(v0, v1);
        var e1 = GetEdge(v1, v2);
        var e2 = GetEdge(v0, v2);

        _verts[v0].AddConnectedTriangles(t.index);
        _verts[v1].AddConnectedTriangles(t.index);
        _verts[v2].AddConnectedTriangles(t.index);

        if (e0 != -1)
        {
            _edges[e0].SetConnectedTriangle(t.index);
        }
        else
        {
            Debug.Log("no edge : "+$"{v0} , {v1}");
        }

        if (e1 != -1)
        {
            _edges[e1].SetConnectedTriangle(t.index);
        }
        else
        {
            Debug.Log("no edge : " + $"{v1} , {v2}");
        }

        if (e2 != -1)
        {
            _edges[e2].SetConnectedTriangle(t.index);
        }
        else
        {
            Debug.Log("no edge : " + $"{v0} , {v2}");
        }
    }

   public  int GetEdge(int v0, int v1)
    {
        int index = -1;
        foreach (var e in _edges)
        {
            if (e.GetOppositeVertex(v0) == v1)
            {
                index= e.index;
                break;
            }
        }

        return index;
    }

    public IEnumerable<Vertex> GetVertexes()
    {
        return _verts;
    }

    public IEnumerable<Edge> GetEdges()
    {
        return _edges;
    }

    public IEnumerable<Triangle> GetTriangles()
    {
        return _triangles;
    }

  
    public void AddVertex(int index)
    {
        _verts.Add(new Vertex(index));
    }

    public IEnumerable<int> GetConnectedVertices(int vertex)
    {
        return _verts[vertex].GetConnectedVertexs();
    }



    public int GetStartVertex(int edge)
    {
        return _edges[edge].Start();
    }

    public int GetEndVertex(int edge)
    {
        return _edges[edge].End();
    }

   public  int GetOppositeVertex(int edge, int vertex)
   {
       return _edges[edge].GetOppositeVertex(vertex);
   }

    public int GetIncidentEdge(int vertex, int index)
    {
        return _verts[vertex].GetConnectedEdges().ToList()[index];
    }

    public IEnumerable<int> GetIncidentEdges(int vertex)
    {
        return _verts[vertex].GetConnectedEdges();
    }

}
