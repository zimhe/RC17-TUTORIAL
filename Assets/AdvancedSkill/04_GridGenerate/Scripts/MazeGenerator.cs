using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpatialSlur.SlurCore;
using UnityEngine;

namespace GridGenerate
{

    public class MazeGenerator : MonoBehaviour
    {
        private GridObject _grid;

        private VertexObject _currentVertex;

        private List<VertexObject> _vertexObjects;
        private List<EdgeObject> _edgeObjects;

        [SerializeField] private PointEvaluator _evaluator;

        void Start()
        {
            _grid = GetComponent<GridManager>().GetGridObject();
            _vertexObjects = _grid.VertexObject();
            _edgeObjects = _grid.EdgeObject();
        }

        void SelectStartVertex()
        {
            foreach (var vo in _grid.VertexObject())
            {
                if (vo.Hover())
                {
                    if (vo.GetStatus() == VertexStatus.Default)
                    {
                        vo.OnSelect();
                        _currentVertex = vo;
                    }
                    else if (vo.GetStatus() == VertexStatus.Selected)
                    {
                        vo.OnDeselect();
                        _currentVertex = null;
                    }
                }
                else
                {
                     if (vo.GetStatus() == VertexStatus.Selected)
                    {
                        vo.OnDeselect();
                        _currentVertex = null;
                    }
                }
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)&&MouseHoveringVertex())
            {
                SelectStartVertex();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _step = !_step;
            }

            if (_step)
            {
                StepToAdjacent();
            }
        }

        private bool _step;

        void StepToAdjacent()
        {
            List<VertexObject> _validVertexes=new List<VertexObject>(6);
            if (_currentVertex!=null)
            {
                var adj = _currentVertex.Vertex.GetConnectedVertexs();

                foreach (var vi in adj)
                {
                    if (_vertexObjects[vi].GetStatus() != VertexStatus.Visited)
                    {
                        _validVertexes.Add(_vertexObjects[vi]);
                    }
                }

                if (_validVertexes.Count == 0)
                {
                    _currentVertex.Visit();
                    SelectNewCurrent();
                    return;
                }

                var next = _validVertexes.SelectMin(v => { return _evaluator.Evaluate(v.transform.position); });

                int v0 = _currentVertex.Vertex.GetIndex();
                int v1 = next.Vertex.GetIndex();

                int edge = _grid.GetGrid().GetEdge(v0, v1);

                _edgeObjects[edge].SetActive(false);

                _currentVertex.Visit();
                _currentVertex = next;
                _currentVertex.OnSelect();
            }
            else
            {
                _currentVertex = _vertexObjects[Random.Range(0, _vertexObjects.Count)];
                _currentVertex.OnSelect();
            }
        }

        void SelectNewCurrent()
        {
            foreach (var vo in _vertexObjects)
            {
                if (vo.GetStatus() == VertexStatus.Default)
                {
                    vo.OnSelect();
                    _currentVertex = vo;
                    break;
                }
            }
        }

        bool MouseHoveringVertex()
        {
            bool hover = false;

            foreach (var vo in _grid.VertexObject())
            {
                if (vo.Hover())
                {
                    hover = true;
                }
            }
            return hover;
        }
    }
}
