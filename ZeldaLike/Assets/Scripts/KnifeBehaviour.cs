using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    public float _knifeSpeed;
    public float _knifeDamage;
    private Rigidbody2D _knifeBody;
    private bool _ephemerate;
    public float _knifeLifetime;
    GameObject _player;
    Vector2 _playerOrientation;

    // Start is called before the first frame update
    void Start()
    {
        _knifeBody = GetComponent<Rigidbody2D>();
        _ephemerate = true;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerOrientation = _player.GetComponent<CharacterOrientationDetection>()._lastOrientation.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        _knifeBody.velocity = _playerOrientation * _knifeSpeed;

        if (_ephemerate == true)
        {
            _ephemerate = false;
            StartCoroutine(DestroyKnife());
        }
    }

    IEnumerator DestroyKnife()
    {
        yield return new WaitForSeconds(_knifeLifetime);
        Destroy(gameObject);
    }
}
