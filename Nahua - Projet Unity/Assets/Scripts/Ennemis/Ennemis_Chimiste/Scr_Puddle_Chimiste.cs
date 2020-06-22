using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Puddle_Chimiste : MonoBehaviour
{
    [Header("Data")]
    public Transform _puddle = null;

    [Header("Statistiques")]
    public float _timer = 1.2f;

    void Start()
    {
        _puddle = this.transform;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
