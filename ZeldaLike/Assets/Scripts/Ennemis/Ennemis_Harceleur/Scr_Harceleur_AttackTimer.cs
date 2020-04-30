using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Harceleur_AttackTimer : MonoBehaviour
{
    public float _attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackTimer > 0)
        {
            _attackTimer -= Time.deltaTime;
        }
        else if (_attackTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
