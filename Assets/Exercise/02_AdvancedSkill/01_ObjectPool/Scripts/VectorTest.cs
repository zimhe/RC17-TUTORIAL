using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{

    [SerializeField] private Transform _object;

    public Vector3 _vector=new Vector3(3f,3f,3f);

	// Use this for initialization
	void Start ()
	{
	    _object.position = _vector;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _object.position = _vector;

        print(_vector.magnitude);
    }
}
