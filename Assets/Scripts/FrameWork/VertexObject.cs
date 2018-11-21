using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexObject : MonoBehaviour,ISelection {

    public Vertex Vertex { get; set; }

    private Color _defaultColor;

    Color _visitedColor=Color.blue;

    private VertexStatus _vertexStatus;

    private bool isHovering;

    public bool Hover()
    {
        return isHovering;
    }

    public void SetStatus(VertexStatus status)
    {
        _vertexStatus = status;
    }

    public VertexStatus GetStatus()
    {
        return _vertexStatus;
    }

    public void OnSelect()
    {
        SetStatus(VertexStatus.Selected);
        if (_defaultColor == default(Color))
            _defaultColor = GetComponent<MeshRenderer>().material.color;

        GetComponent<MeshRenderer>().material.color=Color.red;
    }

    public void OnDeselect()
    {
        SetStatus(VertexStatus.Default);
        if (_defaultColor != default(Color))
            GetComponent<MeshRenderer>().material.color = _defaultColor;
    }

    public void Visit()
    {
        SetStatus(VertexStatus.Visited);
        GetComponent<MeshRenderer>().material.color = _visitedColor;
    }

    //void OnMouseDown()
    //{
    //    if (_vertexStatus == VertexStatus.Selected)
    //    {
    //        _vertexStatus = VertexStatus.Default;
    //        OnDeselect();
    //    }
    //    else
    //    {
    //        _vertexStatus = VertexStatus.Selected;
    //        OnSelect();
    //    }
    //}

    void OnMouseEnter()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
    }

}
