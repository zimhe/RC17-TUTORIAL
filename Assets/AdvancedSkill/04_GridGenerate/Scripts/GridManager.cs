using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boo.Lang;
using SpatialSlur.SlurCore;
using SpatialSlur.SlurMesh;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GridGenerate
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridObject _grid;
        [SerializeField] private VertexObject _vertexPrefab;
        [SerializeField] private EdgeObject _wallEdgePrefab;
        [SerializeField] private EdgeObject _floorEdgePrefab;
        [SerializeField] bool _initialState = true;

        public bool GetInitialState()
        {
            return _initialState;
        }

        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;
        [SerializeField] private int _countZ = 5;
        [SerializeField] private float _scale = 1f;
        [SerializeField] private float _planeHeight = 0f;
        [SerializeField] private float _positionChangeSpeed = 5f;
        [Range(0, 1)] [SerializeField] private float _positionChangeRate = 0.5f;
        private bool changePosition;

        public GridObject GetGridObject()
        {
            return _grid;
        }

        private Mesh _mesh;

        private System.Collections.Generic.List<Vector3> _savedPositions;

        private System.Collections.Generic.List<Vector3> _currentPositions;

        void Awake()
        {
            InitializeGrid();
        }

        void InitializeGrid()
        {
            _savedPositions=new System.Collections.Generic.List<Vector3>();
            _currentPositions = new System.Collections.Generic.List<Vector3>();
            _grid=new GridObject();
            _grid.Initialize(Grid.Factory.CreateTriangleGrid3D(_countX,_countY,_countZ));

            _savedPositions.AddRange(SetVertexPosition());
            _currentPositions.AddRange(_savedPositions);
            _grid.VertexObject().AddRange(CreateVertexObject());
            _grid.EdgeObject().AddRange(CreateHexagonEdgeObject());
        }

        IEnumerable<Vector3> SetVertexPosition()
        {
            for (int y = 0; y < _countY; y++)
            {
                for (int z = 0; z < _countZ; z++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        float w;
                        if (z % 2 == 0)
                        {
                            w = x * _scale;
                        }
                        else
                        {
                            w = x * _scale + 0.5f * _scale;
                        }

                        float h = z * Mathf.Sqrt(Mathf.Pow(_scale, 2) - Mathf.Pow(_scale * 0.5f, 2));

                        float t = y * _scale * _planeHeight;

                        yield return new Vector3(w, t, h);

                    }
                }
            }
        }

        IEnumerable<VertexObject> CreateVertexObject()
        {

            int vertexIndex = 0;
            foreach (var pos in _savedPositions)
            {
                var vo = Instantiate(_vertexPrefab, transform);
                vo.transform.localPosition = pos;
                vo.Vertex = _grid.GetGrid().GetVertexes().ToArray()[vertexIndex];

                vertexIndex++;
                yield return vo;
            }
        }

        IEnumerable<EdgeObject> CreateTriangleEdgeObject()
        {
            foreach (var e in _grid.GetGrid().GetEdges())
            {
                int start = e.Start();
                int end = e.End();

                var eo = Instantiate(_wallEdgePrefab, transform);

                var p = (_savedPositions[start] + _savedPositions[end]) * 0.5f;
                var d = _savedPositions[end] - _savedPositions[start];
                var s = d.magnitude;

                eo.transform.localPosition = p;
                eo.transform.localRotation = Quaternion.FromToRotation(eo.transform.forward, d);
                var scale = eo.transform.localScale;
                scale.z = s;
                eo.transform.localScale = scale;

                yield return eo;
            }
        }

        IEnumerable<EdgeObject> CreateHexagonEdgeObject()
        {
            var gridTriangle = _grid.GetGrid().GetTriangles().ToArray();
            foreach (var e in _grid.GetGrid().GetEdges())
            {
                if (IsFloor(e))
                {
                    var eo = Instantiate(_floorEdgePrefab, transform);

                    var start = _grid.GetGrid().GetVertexes().ToArray()[e.Start()];

                    if (IsBoundVertex(start))
                    {
                        eo.SetActive(false);

                        yield return eo;
                        continue;
                    }
                    else
                    {
                        var tris = start.GetConnectedTriangles();
                        var h = 0.5f * (_savedPositions[e.Start()] + _savedPositions[e.End()]);

                        print(tris.Count());
                      
                        var floor = eo.GetComponent<FloorEdgeObject>();


                        foreach (var tri in tris)
                        {
                            var p = GetSavedCircumCenter(gridTriangle[tri]);
                            p.y = h.y;
                            floor.AddPoint(p);
                        }
                        floor.SetFloorMesh();
                        eo.SetActive(true);
                        yield return eo;
                    }
                }
                else
                {
                    var eo = Instantiate(_wallEdgePrefab, transform);
                    eo.Edge = e;

                    if (e.GetConnectedTriangles().Count <= 1 || IsIsolate(e))
                    {
                        eo.SetActive(false);
                        yield return eo;
                        continue;
                    }

                    eo.SetActive(true);

                    var tris = e.GetConnectedTriangles();
                    var t0 = gridTriangle[tris[0]];
                    var t1 = gridTriangle[tris[1]];

                    var c0 = GetSavedCircumCenter(t0);
                    var c1 = GetSavedCircumCenter(t1);

                    int start = e.Start();
                    int end = e.End();



                    var p = (_savedPositions[start] + _savedPositions[end]) * 0.5f;
                    var d = c1 - c0;
                    var s = d.magnitude;

                    eo.transform.localPosition = p;
                    eo.transform.localRotation = Quaternion.FromToRotation(eo.transform.forward, d);
                    var scale = eo.transform.localScale;
                    scale.z = s;
                    scale.y = _planeHeight;
                    eo.transform.localScale = scale;

                    yield return eo;
                }
            }
        }

        bool IsBoundVertex(Vertex vert)
        {
            bool bound;
            if (vert.GetConnectedTriangles().Count() == 6)
            {
                bound = false;
            }
            else
            {
                bound = true;
            }

            return bound;
        }

        bool IsFloor(Edge edge)
        {
            var s = edge.Start();
            var e = edge.End();

            if (s - _countX * _countZ == e)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IsIsolate(Edge edge)
        {
            var tris = edge.GetConnectedTriangles();
            int vc = _grid.GetGrid().VertexCount;

            bool isEnd = false;

            foreach (var t in tris)
            {
                var T = _grid.GetGrid().GetTriangles().ToArray()[t];

                if (T.GetVertices().Contains(GetIsolateTriangleVertice(T,0)))
                {
                    isEnd = true;
                }

                if (_countZ % 2 == 0)
                {
                    if (T.GetVertices().Contains(GetIsolateTriangleVertice(T,_countX*_countZ-1)))
                    {
                        isEnd = true;
                    }
                }
                else
                {
                    if (T.GetVertices().Contains(GetIsolateTriangleVertice(T,(_countZ-1)*_countX)))
                    {
                        isEnd = true;
                    }
                }
            }
            return isEnd;
        }

        int GetIsolateTriangleVertice(Triangle tri, int value)
        {
            int vert = -1;

            foreach (var v in tri.GetVertices())
            {
                if (v %( _countX*_countZ) == value)
                {
                    vert = v;
                }
            }

            return vert;
        }

        void SetMesh()
        {
            _mesh=new Mesh();
            _mesh.vertices = _savedPositions.ToArray();
            _mesh.triangles = GetMeshTriangle();

            _mesh.name = "TestTriangleMesh";

            GetComponent<MeshFilter>().sharedMesh = _mesh;
        }

        void SetEdgeMesh(int edge)
        {
            _mesh = new Mesh();
            _mesh.vertices = _savedPositions.ToArray();
            _mesh.triangles = GetEdgeMeshTriangle(edge);

            _mesh.name = "TestTriangleMesh";

            GetComponent<MeshFilter>().sharedMesh = _mesh;
        }

        int[] GetMeshTriangle()
        {
            System.Collections.Generic.List<int> tri=new System.Collections.Generic.List<int>();

            foreach (var t in _grid.GetGrid().GetTriangles())
            {
                tri.AddRange(t.GetVertices());
            }

            return tri.ToArray();
        }

        int[] GetEdgeMeshTriangle(int edge)
        {
            System.Collections.Generic.List<int> tri = new System.Collections.Generic.List<int>();

            var gridTriangles = _grid.GetGrid().GetTriangles().ToArray();

            foreach (var t in _grid.GetGrid().GetEdges().ToArray()[edge].GetConnectedTriangles())
            {
                tri.AddRange(gridTriangles[t].GetVertices());
            }

            return tri.ToArray();
        }

        // Use this for initialization
        void Start()
        {
            //DrawTestMesh();
        }

        private int EdgeIndex = 0;
        // Update is called once per frame
        void Update()
        {
           //DrawTestMesh();

            foreach (var edge in _grid.GetGrid().GetEdges())
            {
                if (edge.GetConnectedTriangles().Count == 2)
                {
                    Debug.DrawLine(_savedPositions[edge.Start()], _savedPositions[edge.End()], Color.green);
                    //SetEdgeMesh(edge.index);
                }
                else if(edge.GetConnectedTriangles().Count == 1)
                {
                    Debug.DrawLine(_savedPositions[edge.Start()], _savedPositions[edge.End()], Color.red);
                }
                else
                {
                    Debug.DrawLine(_savedPositions[edge.Start()], _savedPositions[edge.End()], Color.grey);
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                changePosition = !changePosition;
            }

            if (changePosition)
            {
                UpdateVertex();
                UpdateEdges();
            }

        }

        void DrawTestMesh()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (EdgeIndex < _grid.GetGrid().GetEdgeCount)
                {
                    SetEdgeMesh(EdgeIndex);
                    EdgeIndex++;
                }
                else
                {
                    EdgeIndex = 0;
                }
            }

            if (EdgeIndex > 0)
            {
                var currentEdge = _grid.GetGrid().GetEdges().ToArray()[EdgeIndex - 1];

                Debug.DrawLine(_savedPositions[currentEdge.Start()], _savedPositions[currentEdge.End()], Color.red);
            }
        }

        Vector3 GetSavedCircumCenter(Triangle triangle)
        {
            var v = triangle.GetVertices();
            var pos = _savedPositions;

            var p0 =(Vec3d) pos[v[0]];
            var p1 =(Vec3d) pos[v[1]];
            var p2 = (Vec3d)pos[v[2]];
            var cc = SpatialSlur.SlurCore.GeometryUtil.GetCircumcenter(p0, p1, p2);
            return (Vector3) cc;
        }

        Vector3 GetCurrentCircumCenter(Triangle triangle)
        {
            var v = triangle.GetVertices();
            var vert = _grid.VertexObject();

            var p0 = (Vec3d)vert[v[0]].transform.localPosition;
            var p1 = (Vec3d)vert[v[1]].transform.localPosition;
            var p2 = (Vec3d)vert[v[2]].transform.localPosition;
            var cc = SpatialSlur.SlurCore.GeometryUtil.GetCircumcenter(p0, p1, p2);
            return (Vector3)cc;
        }

        Vector3 GetCurrentRandomPosition(float rate)
        {
            var v= Random.insideUnitCircle * rate;

            return new Vector3(v.x,0f,v.y);
        }
        
        void UpdateCurrenPosition()
        {
            for (int i = 0; i < _currentPositions.Count; i++)
            {
                var p = _savedPositions[i] + GetCurrentRandomPosition(0.3f)*_positionChangeRate;

                _currentPositions[i] = p;
            }
        }

        void UpdateVertex()
        {
            if (!VertexPositionClose())
            {
                for (int i = 0; i < _currentPositions.Count; i++)
                {
                    var vo = _grid.VertexObject()[i];
                    vo.transform.localPosition = Vector3.Lerp(vo.transform.localPosition, _currentPositions[i],
                        Time.deltaTime * _positionChangeSpeed);
                }
            }
            else
            {
                UpdateCurrenPosition();
            }
        }

        void UpdateEdges()
        {
            foreach (var eo in _grid.EdgeObject())
            {
                if (eo.GetComponent<WallEdgeObject>())
                {
                    if (eo.Edge.GetConnectedTriangles().Count == 2)
                    {
                     

                    
                       

                        var t0 = eo.Edge.GetConnectedTriangles()[0];
                        var t1 = eo.Edge.GetConnectedTriangles()[1];

                        var p = 0.5f * (GetCurrentCircumCenter(_grid.GetGrid().GetTriangles().ToArray()[t1]) +
                                        GetCurrentCircumCenter(_grid.GetGrid().GetTriangles().ToArray()[t0]));

                        var d = GetCurrentCircumCenter(_grid.GetGrid().GetTriangles().ToArray()[t1]) -
                                GetCurrentCircumCenter(_grid.GetGrid().GetTriangles().ToArray()[t0]);

                        var r = Quaternion.LookRotation(d.normalized);

                        eo.UpdatePosition(p);
                        eo.UpdateRotation(r);
                        var scale = eo.transform.localScale;
                        scale.z = d.magnitude;
                        eo.transform.localScale = scale;
                    }
                }

                if (eo.GetComponent<FloorEdgeObject>())
                {

                }
            }

        }

        bool VertexPositionClose()
        {
            bool close = true;
            for (int i = 0; i < _currentPositions.Count; i++)
            {
                if (Vector3.Distance(_currentPositions[i], _grid.VertexObject()[i].transform.localPosition) >= 0.05f)
                {
                    close = false;
                }
            }

            return close;
        }
    }

}

