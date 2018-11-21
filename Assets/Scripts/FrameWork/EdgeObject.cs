using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EdgeObject : MonoBehaviour
{
    public Edge Edge { get; set; }

    private bool _active;


    public void SetActive(bool active)
    {
        _active = active;
        GetComponent<MeshRenderer>().enabled = active;
    }

    public bool IsActive()
    {
        return _active;
    }

    public abstract void UpdatePosition(Vector3 position);

    public abstract void UpdateRotation(Quaternion rotation);

    public abstract void UpdateMesh();

}
