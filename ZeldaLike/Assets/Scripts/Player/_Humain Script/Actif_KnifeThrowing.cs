using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actif_KnifeThrowing : MonoBehaviour
{
    public GameObject _knife;
    public Transform _Container;
    public Transform _attackPos;

    [SerializeField] private bool _goodToShoot;
    public float _throwRecup = 0.2f;
    public float _recupTimer = 0f;

    void Start()
    {
        _goodToShoot = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && _goodToShoot == true)
        {
            Instantiate(_knife, _attackPos.position, _attackPos.rotation, _Container);
            _goodToShoot = false;
            _recupTimer = _throwRecup;
        }
        else
        {
            _recupTimer -= Time.deltaTime;

            if(_recupTimer <= 0)
            {
                _goodToShoot = true;
            }
        }
    }
}
