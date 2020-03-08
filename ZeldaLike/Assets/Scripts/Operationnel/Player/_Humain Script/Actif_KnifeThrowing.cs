using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actif_KnifeThrowing : MonoBehaviour
{
    public GameObject _knife;
    public Transform _attackContainer;

    [SerializeField] private bool _goodToShoot;
    public float _throwRecup;

    // Start is called before the first frame update
    void Start()
    {
        _goodToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && _goodToShoot == true)
        {
            Instantiate(_knife, transform.position, Quaternion.identity, _attackContainer);
            _goodToShoot = false;
            StartCoroutine(ThrowDelay());
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(_throwRecup);
        _goodToShoot = true;
    }
}
