using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CollisionDetectionJump : MonoBehaviour
{
    public GameObject _agileForm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Vide"))
        {
            _agileForm.GetComponent<Actif_AgileAttack>()._stopJump = true;
            Debug.Log("Enviro found");
        }
    }
}
