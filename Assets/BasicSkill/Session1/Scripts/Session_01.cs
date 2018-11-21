using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session_01 : MonoBehaviour {
    public bool _boolean;
    public float _float;
    public int _int;
    public string _string;

    string[] _stringArray_0 = new string[10];
    string[] _stringArray_1 = new[] { "aa", "bb", "cc" };
    int[] _intArray = new int[5];

    List<int> _intList_0 = new List<int> ();
    List<int> _intList_1 = new List<int> { 0, 0, 1, 1, 3, 3, 2, 2, };

    Dictionary<int, string> _myDic = new Dictionary<int, string>();

    // Use this for initialization
    private void Awake()
    {
        
    }

    void Start ()
    {
        //Debug.Log(_string);
        //Debug.Log(_boolean);
        //Debug.Log(_float);
        //Debug.Log(_int);
        for(int i = 0; i < _stringArray_0.Length; i++)
        {
            _stringArray_0 [i]= "Array Member" + i;
        }

      

        for(int i = 0; i < 30; i++)
        {
            _intList_0.Add(i);
            _intList_1.Add(i);
        }

        for(int i = 0; i < 30; i++)
        {
            _myDic.Add(i, "value ");
        }

        //foreach (var s in _stringArray_1)
        //{
        //    Debug.Log(s);
        //}

        foreach (var s in _intList_0)
        {
            Debug.Log("list_0: "+s);
        }
        foreach (var s in _intList_1)
        {
            Debug.Log("list_1: "+s);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_boolean)
        {
            Debug.Log("bool is true");
        }
        
	}
}
