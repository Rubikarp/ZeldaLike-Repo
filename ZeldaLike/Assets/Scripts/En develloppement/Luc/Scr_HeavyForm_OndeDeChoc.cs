using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_HeavyForm_OndeDeChoc : MonoBehaviour
{
    public float _timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
