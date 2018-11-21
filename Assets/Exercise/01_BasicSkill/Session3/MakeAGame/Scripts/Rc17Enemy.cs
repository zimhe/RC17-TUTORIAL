using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rc17Enemy : MonoBehaviour
{
    private EnemyType _type;

    [SerializeField]private Color goodColor;
    [SerializeField] private Color badColor;

    public EnemyType GetType()
    {
        return _type;
    }

    public void SetType(EnemyType type)
    {
        _type = type;

        switch (type)
        {
            case EnemyType.good:
                GetComponent<MeshRenderer>().material.color = goodColor;
                break;
            case EnemyType.bad:
                GetComponent<MeshRenderer>().material.color = badColor;
                break;
        }
    }
}


public enum EnemyType
{
    good,
    bad,
}
