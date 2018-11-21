using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEdgeObject : EdgeObject
{
    private List<Vector3> _meshVertexes;

    private int[] _meshTriangles = new[] {0, 2, 1, 0, 5, 2, 0, 3, 5, 3, 4, 5};

    private Mesh _mesh;

    public void AddPoint(Vector3 point)
    {
        if (_meshVertexes == null)
        {
            _meshVertexes=new List<Vector3>(6);
        }
        _meshVertexes.Add(point);
    }

    public void UpdatePoint(int index, Vector3 position)
    {
        _meshVertexes[index] = position;
    }

    public int GetVertexCount()
    {
        return _meshVertexes.Count;
    }

    public void SetFloorMesh()
    {
        _mesh = new Mesh
        {
            vertices = _meshVertexes.ToArray(),
            triangles = _meshTriangles
        };
        _mesh.RecalculateNormals();

        //_mesh.name = $"Floor Mesh {Edge.index}";

        GetComponent<MeshFilter>().mesh = _mesh;
    }

    public override void UpdatePosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public override void UpdateRotation(Quaternion rotation)
    {
        transform.localRotation = rotation;
    }

    public override void UpdateMesh()
    {
        _mesh.vertices = _meshVertexes.ToArray();
    }
}
