using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_SpawnEnnemies : MonoBehaviour
{
    public List<GameObject> _ennemiesToSpawn;
    public float[] _spawnDelay;
    public GameObject _limitTilemap;
    public GameObject _limitCollider;
    [HideInInspector] public bool _fightStarted;
    private int _killCount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _ennemiesToSpawn.Count; i++)
        {
            _ennemiesToSpawn[i].SetActive(false);
        }

        _fightStarted = false;
        _killCount = 0;
        _limitCollider.SetActive(false);
        _limitTilemap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_fightStarted == true)
        {
            for (int k = 0; k < _ennemiesToSpawn.Count; k++)
            {
                if (_ennemiesToSpawn[k] == null && _killCount == k)
                {
                    _killCount += 1;
                }
            }

            if (_killCount == _ennemiesToSpawn.Count)
            {
                _limitCollider.SetActive(false);
                _limitTilemap.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _fightStarted == false)
        {
            StartCoroutine(EnemySpawn());
        }
    }

    private IEnumerator EnemySpawn()
    {
        _fightStarted = true;
        _limitCollider.SetActive(true);
        _limitTilemap.SetActive(true);

        for (int j = 0; j < _ennemiesToSpawn.Count; j++)
        {
            yield return new WaitForSeconds(_spawnDelay[j]);
            _ennemiesToSpawn[j].SetActive(true);
        }
    }
}
