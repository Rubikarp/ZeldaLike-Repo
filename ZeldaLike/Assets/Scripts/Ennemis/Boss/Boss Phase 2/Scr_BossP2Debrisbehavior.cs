using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossP2Debrisbehavior : MonoBehaviour
{
    public Vector3 _target;
    private bool _targetAttained;
    public float _moveSpeed;
    private Transform _mySelf;
    public GameObject _hitBox;
    public float _damageLife;
    public GameObject _debris;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform.position;
        _mySelf = transform;
        _targetAttained = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetAttained == false)
        {
            _mySelf.position = Vector2.MoveTowards(_mySelf.position, _target, _moveSpeed * Time.deltaTime);

            if (_mySelf.position == _target)
            {
                _targetAttained = true;
            }
        }
        else if (_targetAttained == true)
        {
            _hitBox.SetActive(true);

            if (_damageLife > 0)
            {
                _damageLife -= Time.deltaTime;
            }
            else if (_damageLife <= 0)
            {
                Instantiate(_debris, _mySelf.position, _mySelf.rotation);
                Destroy(gameObject);
            }
        }
    }
}
