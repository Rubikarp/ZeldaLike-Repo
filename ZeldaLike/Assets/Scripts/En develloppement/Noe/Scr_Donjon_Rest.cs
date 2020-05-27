using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Donjon_Rest : MonoBehaviour
{
    public GameObject _puzzle;
    public Transform _puzzlePosition;
    private Vector3 _spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        _spawnPosition = _puzzlePosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset_Puzzle ()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Instantiate(_puzzle, _spawnPosition, Quaternion.identity, transform);
    }
}
