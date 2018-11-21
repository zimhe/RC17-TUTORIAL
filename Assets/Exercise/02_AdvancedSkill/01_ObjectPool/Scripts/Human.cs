using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human:MonoBehaviour
{
    public Human(float height, float weight, int age, string sex, string name)
    {
        _height = height;
        _weight = weight;
        _age = age;
        _sex = sex;
        _name = name;
    }

    public void Initialize(float height, float weight, int age, string sex, string name)
    {
        _height = height;
        _weight = weight;
        _age = age;
        _sex = sex;
        _name = name;
    }

    public float _height;
    public float _weight;
    public int _age;
    public string _sex;
    public string _name;


    public void Jump(float force)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up*force,ForceMode.Impulse);
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction);
    }

}
