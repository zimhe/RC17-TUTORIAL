using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public int _publicInt = 0;
    public  int _intger = 0;
    public float _float = 0.5f;
    public string _string = "string";

    public bool _bool = false;

    private int[] _intArray=new int[10];

    private float[] _floatArray = new float[10];

    string[] _stringArray=new string[10];

    private List<int> _intLIst=new List<int>();

    Dictionary<string,int> _dictionary=new Dictionary<string, int>();


	// Use this for initialization
    // 
    void Start()
    {
        _dictionary.Add("First",0);
        _dictionary.Add("Second",1);

        Debug.Log(_dictionary["First"]);

        //Giving value to array
        _intArray[0] = 0;
        //Giving value to List
        _intLIst.Add(0);
        _intLIst[0] = 0;

        for (int i = 0; i < _intArray.Length; i = i + 1)
        {
            Debug.Log("Before" + _intArray[i]);
        }

        for (int i = 0; i < _intArray.Length; i = i + 1)
        {
            _intArray[i] = i;
            Debug.Log("After" + _intArray[i]);
            //print("");
        }
        
   
        // for loop

        for (int i = 0; i < 10; i++)
        {
            //do something
        }

        for (int i = 0; i < 10; i += 2)
        {
            //do something
        }

        for (int i = -10; i > -30; i--)
        {
            //do something
        }

        for (float f = 0f; f < 3f; f += 0.1f)
        {
            //do something
        }

        //foreach loop

        foreach (var i in _intArray)
        {
            Debug.Log(i);
            print(i);
        }

        //Debug.Log(_publicInt*_intger);
        //Debug.Log(_string);
    }

    // Update is called once per frame
	void Update ()
	{
	    if (_bool == true)
	    {
            Debug.Log(_bool);
	    }
	    else
	    {
	        Debug.Log("_bool is not true");
	    }
    }
}
