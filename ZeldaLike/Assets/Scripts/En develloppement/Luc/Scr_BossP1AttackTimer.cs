using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossP1AttackTimer : MonoBehaviour
{
    public float _lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime;
        }
        else if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
