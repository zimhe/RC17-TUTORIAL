using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    private List<Human> _humanList;

    [SerializeField]private Human _andy;
    [SerializeField] private float _strength=10f;

    [SerializeField] private float _changeColorHeight = 1f;
    [SerializeField] private float _changeColorDist = 3f;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _changeColor;

    void Awake()
    {

    }

	// Use this for initialization
	void Start ()
	{
	    _andy.Initialize(180f, 60f, 30, "Male", "Andy");

        _humanList =new List<Human>();

        _humanList.Add(new Human(180f,60f,30,"Male","Tom"));
	    _humanList.Add(new Human(180f, 60f, 30, "Male", "Bob"));
        _humanList.Add(_andy);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
            _andy.Jump(_strength);
	    }

	    if (Input.GetKeyDown(KeyCode.W))
	    {
            _andy.Move(Vector3.forward);
	    }

	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        _andy.Move(Vector3.back);
	    }
	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        _andy.Move(Vector3.left);
	    }
	    if (Input.GetKeyDown(KeyCode.D))
	    {
	        _andy.Move(Vector3.right);
	    }

	    if (GetHeight() >= _changeColorHeight )
	    {
	        if (GetDistance() >= _changeColorDist)
	        {
	            _andy.GetComponent<MeshRenderer>().material.color = _changeColor;
            }

	        _andy.transform.localScale = Vector3.one * 3f;
	    }
	    else
	    {
	        _andy.transform.localScale = Vector3.one * 0.5f;
        }


    }

    void DoSomething()
    {

    }

    float GetHeight()
    {
        return _andy.transform.localPosition.y;
    }

    float GetDistance()
    {
        return _andy.transform.position.magnitude;
    }

}
