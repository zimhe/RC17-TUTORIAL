using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTest : MonoBehaviour {

	// Use this for initialization



    void Awake()
    {
        print("awake");
    }

	void Start ()
	{
		print("start");

        DoSomething();
        DoSomething(10f);
	}
	
   
	// Update is called once per frame
	void Update ()
	{
		//print("update");
	}

    void FixedUpdate()
    {

    }



    void DoManyThings()
    {
        DoSomething();
        DoSomething();
        DoSomething(10);
    }

    void DoSomething()
    {
        print(50);
    }

    void DoSomething(float value)
    {
        print(50*value);
        DoSomething();
    }
   

}
