using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
    /// <summary>
    /// 
    /// </summary>
    int VertexCount { get; }

    int GetDegree(int vertex);

    void AddEdge(int v0, int v1);

    void AddVertex(int index);

    void AddTriangle(int v0, int v1, int v2);

    

    int GetEdgeCount { get; }

    int GetStartVertex(int edge);

    int GetEndVertex(int edge);

    int GetOppositeVertex(int edge, int vertex);

    int GetIncidentEdge(int vertex, int index);

    IEnumerable<int> GetConnectedVertices(int vertex);
    IEnumerable<int> GetIncidentEdges(int vertex);


}
