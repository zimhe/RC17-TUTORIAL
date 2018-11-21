using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWhileLoop : MonoBehaviour {

    // Use this for initialization
    [SerializeField] int speed = 10;
    int addCount = 100;
    int num = 0;
    int timer = 0;
    float second;
	void Start () {
        second = Time.time + 1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > second)
        {
            second = Time.time + 1f;
            timer++;
            print(timer);
        }

        if (timer > speed)
        {
            timer = 0;
        }
        while (num < addCount&&timer==speed)
        {
            num++;
            print("number:"+num);
        }
       
	}
}
