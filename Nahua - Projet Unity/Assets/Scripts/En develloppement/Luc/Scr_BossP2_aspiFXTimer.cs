using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossP2_aspiFXTimer : MonoBehaviour
{
    public float _time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else if (_time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
