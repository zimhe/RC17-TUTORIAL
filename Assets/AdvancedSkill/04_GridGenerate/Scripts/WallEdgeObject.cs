using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEdgeObject : EdgeObject {

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

    }
}
