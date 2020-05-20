using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_Reset_Objects : MonoBehaviour
{
    public List<Transform> _objectsToReset;
    public List<Vector2> _objectsInitialPositions;

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
                _objectsToReset[i].position = _objectsInitialPositions[i];
            }
        }
    }
}
