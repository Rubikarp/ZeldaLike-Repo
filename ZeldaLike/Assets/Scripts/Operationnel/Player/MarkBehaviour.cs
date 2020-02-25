using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBehaviour : MonoBehaviour
{
    GameObject _player;
    Rigidbody2D _markBody;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        _markBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            _player.transform.position = transform.position;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
