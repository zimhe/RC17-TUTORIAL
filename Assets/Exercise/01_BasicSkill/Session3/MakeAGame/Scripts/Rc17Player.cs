using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rc17Player : MonoBehaviour
{
     private float _health = 100;
    private float _scale = 1f;
   // [SerializeField] private float rewardValue = 0.5f;
    [SerializeField] private float punishValue = 10f;
    [SerializeField] private Color LiveColor = Color.yellow;
    [SerializeField] private Color DeadColor = Color.red;

    private void OnEnable()
    {
        UpdateColor();
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug .Log("into something");
        var en = collider.gameObject.GetComponent<Rc17Enemy>();
        if (collider.gameObject.GetComponent<Rc17Enemy>())
        {
            print("found enemy");

            if (en.GetType() == EnemyType.good)
            {
                _scale += 0.05f;
                _health += punishValue;
                UpdateScale();
                UpdateColor();
            }
            else
            {
                _health -= punishValue;
                UpdateColor();
            }
            Destroy(en.gameObject);
        }
    }

    public void MoveToward(Vector3 dir)
    {
        GetComponent<Rigidbody>().MovePosition(transform.localPosition+dir);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(dir.normalized));
    }

    public bool IsDead()
    {
        if (_health > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void UpdateColor()
    {
        var color = Color.Lerp(DeadColor, LiveColor, _health / 100f);

        GetComponent<MeshRenderer>().material.color = color;
    }

    void UpdateScale()
    {
        transform.localScale *= _scale;
    }
}
