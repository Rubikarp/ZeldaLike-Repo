using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowing : MonoBehaviour
{
    public GameObject _knife;
    private bool _goodToShoot;
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
            Instantiate(_knife, transform.position, Quaternion.identity);
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
