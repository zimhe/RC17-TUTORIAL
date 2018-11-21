using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{

    [SerializeField] private GameObject _vertexPrefab;

    [SerializeField]private int _countX = 5;
    [SerializeField]private int _countY = 5;
    [SerializeField]private int _countZ = 5;
    [SerializeField] private float _strength = 1f;
    [SerializeField] private float _space = 1f;

    private float timer;



    private List<GameObject> _vertexes;
    private List<Vector3> _positions;
    private List<Vector3> _changingPositions;

	// Use this for initialization
	void Start ()
	{
		_vertexes=new List<GameObject>();
        _positions=new List<Vector3>();
	    _changingPositions = new List<Vector3>();

	    timer = Time.time;
	    SetPositions(_countX,_countY,_countZ);
	}

    void SetPositions(int countX, int countY, int countZ)
    {
        for (int y = 0; y < countY; y++)
        {
            for (int z = 0; z < countZ; z++)
            {
                for (int x = 0; x < countX; x++)
                {
                    Vector3 p = new Vector3(x * _space, y * _space, z * _space);
                    _positions.Add(p);
                }
            }
        }
    }

    void SetVertex(Vector3 pos)
    {
        GameObject v = Instantiate(_vertexPrefab, transform);
        v.transform.localPosition = pos;
        _vertexes.Add(v);
    }

    void CreateGrid(int countX, int countY, int countZ)
    {
        for (int y = 0; y < countY; y++)
        {
            for (int z = 0; z < countZ; z++)
            {
                for (int x = 0; x < countX; x++)
                {
                    Vector3 p=new Vector3(x*_space,y*_space,z*_space);
                    GameObject v = Instantiate(_vertexPrefab, transform);
                    v.transform.localPosition = p;

                    _vertexes.Add(v);
                    _positions.Add(p);
                }
            }
        }
    }

    

    void UpdateGrid()
    {
        foreach (var v in _vertexes)
        {
           var cp = v.transform.localPosition + GetRandom()*_strength;

            v.transform.localPosition = Vector3.Lerp(v.transform.localPosition, cp, Time.deltaTime);
        }
    }

    Vector3 GetRandom()
    {
        return Random.insideUnitSphere;
    }

    private int count = 0;
	// Update is called once per frame
	void Update ()
	{

	    if (Time.time >= timer + 1f)
	    {
	        if (count < _positions.Count)
	        {
                SetVertex(_positions[count]);

	            count++;
	        }
	        timer = Time.time;
	    }
	}
}
