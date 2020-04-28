using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_SpawnEnnemies : MonoBehaviour
{
    public List<GameObject> _ennemiesToSpawn;
    public float _spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _ennemiesToSpawn.Count; i++)
        {
            _ennemiesToSpawn[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int j = 0; j < _ennemiesToSpawn.Count; j++)
            {
                _ennemiesToSpawn[j].SetActive(true);
            }
        }
    }
}
