using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "GridFramework/Evaluation/RandomEvaluator")]
public class RandomEvaluator : PointEvaluator
{
    public override float Evaluate(Vector3 point)
    {
        return Random.Range(0f, 1f);
    }
}