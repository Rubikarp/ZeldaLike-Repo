using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_Reset_Objects : MonoBehaviour
{
    public List<GameObject> _objectsToReset;
    public List<Vector2> _objectsPositions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < _objectsToReset.Count; i++)
            {
                Destroy(_objectsToReset[i]);
                _objectReseted = Instantiate(_objectsToReset[i], _objectsPositions[i], Quaternion.identity);
            }
        }
    }
}
