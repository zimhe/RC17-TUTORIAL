using UnityEngine;
using UnityEditor;



public abstract class PointEvaluator : ScriptableObject
{
    public abstract  float Evaluate(Vector3 point);

}