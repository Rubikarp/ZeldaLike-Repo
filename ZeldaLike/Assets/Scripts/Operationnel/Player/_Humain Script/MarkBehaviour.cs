using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class MarkBehaviour : MonoBehaviour
{
    private GameObject _player;
    private Rigidbody2D _markBody;

    [SerializeField]
    private InputManager _input = null;

    void Start()
    {
        _markBody = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    }

    void Update()
    {
        if (_input._interaction)
        {
            _player.transform.position = _markBody.transform.position;
        }
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
