using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTest : MonoBehaviour {

	// Use this for initialization


    private GameObject _testGameObject;



	void Start ()
	{
		_testGameObject=GameObject.CreatePrimitive(PrimitiveType.Capsule);

	    var cube = GameObject.Find("parent");
        cube.GetComponent<MeshRenderer>().material.color=Color.red;

	    var inst = Instantiate(cube);

	    inst.transform.localPosition = Vector3.forward * 5;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
