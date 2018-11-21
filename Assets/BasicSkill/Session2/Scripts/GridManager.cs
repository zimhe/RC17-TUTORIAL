using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

   public  GameObject _voxelPrefab;
    public GameObject _voxelRBPrefab;

    public int _length = 10;
    public int _width = 10;
    public int _hight = 20;
    public float _space = 1f;

    List<GameObject> _voxels;
    private List<GameObject> _voxelRB;
    List<Vector3> _pisitions;

    [SerializeField]private Texture2D seedImage;



	// Use this for initialization
	void Start ()
	{
	    _length = seedImage.width;
	    _width = seedImage.height;
        _voxels = new List<GameObject>();
        _voxelRB=new List<GameObject>();
        _pisitions = new List<Vector3>();

        GenerateGrid();
        //PutVoxel();



        StartCoroutine(ProduceSomething(30));



    }

    IEnumerator ProduceSomething(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            yield return new WaitForSeconds(2f);

            var v = Instantiate(_voxelRBPrefab, transform);
            v.transform.localPosition = 30f * Vector3.up;

            _voxelRB.Add(v);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        UpdateVoxelPosition();
	}

    void UpdateVoxelPosition()
    {

        for (int i=0, y = 0; y < _hight; y++)
        {
            for (int z = 0; z < _width; z++)
            {
                for (int x = 0; x < _length; x++,i++)
                {
                    Vector3 p = new Vector3(x * _space, y * _space, z * _space);

                    _voxels[i].transform.localPosition = p;

                }
            }
        }
    }

    void GenerateGrid()
    {
        if (_pisitions.Count != 0)
        {
            _pisitions.Clear();
        }
        for (int y = 0; y < _hight; y++)
        {
            for(int z = 0; z < _width; z++)
            {
                for(int x = 0; x < _length; x++)
                {
                    Vector3 p = new Vector3(x*_space, y*_space, z*_space);
                    _pisitions.Add(p);

                    var v = Instantiate(_voxelPrefab, transform);

                    v.transform.localPosition = p;

                    _voxels.Add(v);

                    //if (seedImage.GetPixel(x, z).grayscale > 0.5f)
                    //{
                    //    v.SetActive(false);
                    //}
                    float gs = seedImage.GetPixel(x, z).grayscale;

                    v.transform.localScale =v.transform.localScale* Mathf.Clamp(gs, 0.1f, 0.8f);
                }
            }
        }
    }

    void GenerateGrid(int length,int width,int hight)
    {
        if (_pisitions.Count != 0)
        {
            _pisitions.Clear();
        }
        for (int y = 0; y < hight; y++)
        {
            for (int z = 0; z < width; z++)
            {
                for (int x = 0; x < length; x++)
                {
                    Vector3 p = new Vector3(x, y, z);
                    _pisitions.Add(p);
                }
            }
        }
    }

    void PutVoxel()
    {
        foreach(var p in _pisitions)
        {
            var v = Instantiate(_voxelPrefab, transform);

            v.transform.localPosition = p;

            _voxels.Add(v);
        }
    }
}
