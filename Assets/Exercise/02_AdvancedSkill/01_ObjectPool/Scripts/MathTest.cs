using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MathTest : MonoBehaviour
{
    private float value1 = 1f;
    private float value2 = 6f;
    [Range(0,1f)][SerializeField] private float degree;
    private float clampMin = 2f;
    private float clampMax = 5f;
    
    private float _mathTest;
	// Use this for initialization
	void Start ()
	{
	    _mathTest = Mathf.Lerp(value1, value2, 0.5f);
        print(_mathTest);

	}
	
	// Update is called once per frame

	void Update ()
	{
	    _mathTest = Mathf.Lerp(value1, value2, degree);
	    _mathTest = Mathf.Clamp(_mathTest, clampMin, clampMax);

	    _mathTest = Mathf.Pow(_mathTest, 2);
	    _mathTest = Mathf.Abs(_mathTest);
	    _mathTest = Mathf.Sqrt(_mathTest);

        

	    print(_mathTest);
    }
}
