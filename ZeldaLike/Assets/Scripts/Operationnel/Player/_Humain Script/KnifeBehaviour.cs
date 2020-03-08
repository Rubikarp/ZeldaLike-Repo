using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class KnifeBehaviour : MonoBehaviour
{
    public float _Speed = 35f;
    public float _Damage = 2f;
    private Rigidbody2D _Body;
    private bool _ephemerate;
    public float _Lifetime = 0.4f;
    GameObject _player;
    Vector2 _playerOrientation;

    // Start is called before the first frame update
    void Start()
    {
        _Body = GetComponent<Rigidbody2D>();
        _ephemerate = true;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerOrientation = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>()._CharacterDirection;
    }

    // Update is called once per frame
    void Update()
    {
        _Body.velocity = _playerOrientation * _Speed;

        if (_ephemerate == true)
        {
            _ephemerate = false;
            StartCoroutine(DestroyKnife());
        }
    }

    IEnumerator DestroyKnife()
    {
        yield return new WaitForSeconds(_Lifetime);
        Destroy(gameObject);
    }
}
