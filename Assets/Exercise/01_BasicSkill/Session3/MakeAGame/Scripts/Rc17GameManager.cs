using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rc17GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private int _enemyCount = 5;

   private GameObject[] _player=new GameObject[1];

    private List<GameObject> enemys;

	// Use this for initialization
	void Start () {
		enemys=new List<GameObject>();

	    for (int i = 0; i < _enemyCount; i++)
	    {
	        PopEnemty();
	    }

        PopPlayer();

	}
	
	// Update is called once per frame
	void Update ()
	{
	    var enemysGO = GameObject.FindGameObjectsWithTag("Enemy");
	    if (enemysGO.Length < _enemyCount)
	    {
            PopEnemty();
	    }

	    Vector3 dir = Vector3.zero;

	    if (Input.GetKey(KeyCode.W))
	    {
          dir+=Vector3.forward*_speed;
	    }
	    if (Input.GetKey(KeyCode.A))
	    {
	        dir += Vector3.left * _speed;
        }
	    if (Input.GetKey(KeyCode.S))
	    {
	        dir += Vector3.back * _speed;
        }
	    if (Input.GetKey(KeyCode.D))
	    {
	        dir += Vector3.right * _speed;
        }

	    if (dir.magnitude > 0f&&_player[0]!=null)
	    {
	        _player[0].GetComponent<Rc17Player>().MoveToward(dir);
        }


        if (_player[0].GetComponent<Rc17Player>().IsDead())
        {
            StartCoroutine(Restart());
        }
    }


    IEnumerator Restart()
    {
        PlayerDead();
        yield return new WaitForSeconds(1f);

        PopPlayer();
    }

    void PlayerDead()
    {
        Destroy(_player[0].gameObject);
    }

    private void PopPlayer()
    {
        _player[0] = Instantiate(_playerPrefab, transform);
    }

    void PopEnemty()
    {
        var en = Instantiate(_enemyPrefab, transform);

        var d = Random.Range(2f, 50f);
        var p = Random.insideUnitCircle;

        var ep = p * d;

        en.transform.localPosition = new Vector3(ep.x,en.transform.localPosition.y,ep.y);

        enemys.Add(en);

        var type = Random.Range(0f, 2f);

        if (type <= 1f)
        {
            en.GetComponent<Rc17Enemy>().SetType(EnemyType.good);
        }
        else
        {
            en.GetComponent<Rc17Enemy>().SetType(EnemyType.bad);
        }
    }
}
