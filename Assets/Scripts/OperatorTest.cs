using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorTest : MonoBehaviour {

	// Use this for initialization
    public int _count = 10;


	void Start ()
	{
	    print(_count >> 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
