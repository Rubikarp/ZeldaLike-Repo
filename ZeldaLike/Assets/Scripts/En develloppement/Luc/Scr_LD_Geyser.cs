using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_Geyser : MonoBehaviour
{
    public float _repulseDelay;
    public float _repulseSpeed;
    public float _repulseTime;
    private bool _startRepulse;

    private List<Transform> _enterGuys = new List<Transform>();
    private List<Vector3> _enterPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        _startRepulse = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ennemis"))
        {
            _enterGuys.Add(collision.gameObject.transform.parent.parent);
            _enterPositions.Add(collision.gameObject.transform.parent.parent.position);
            Debug.Log(_enterGuys);
            Debug.Log(_enterPositions[0]);

            if (_startRepulse == false)
            {
                _startRepulse = true;
                StartCoroutine(Boom());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ennemis"))
        {
            for (int j = 0; j < _enterGuys.Count; j++)
            {
                if (collision.gameObject == _enterGuys[j])
                {
                    _enterGuys.RemoveAt(j);
                    _enterPositions.RemoveAt(j);
                    Debug.Log(_enterGuys);
                }
            }
        }
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(_repulseDelay);
        for (int i = 0; i < _enterGuys.Count; i++)
        {
            StartCoroutine(Repulse(i));
        }
        Debug.Log("Repulse !" + _enterGuys[0]);
        _startRepulse = false;
    }

    IEnumerator Repulse (int i)
    {
        float duration = _repulseTime;

        while (duration > 0)
        {
            _enterGuys[i].transform.position = Vector2.MoveTowards(_enterGuys[i].transform.position, _enterPositions[i], _repulseSpeed * Time.deltaTime);
            duration -= Time.deltaTime;
        }

        yield return new WaitForSeconds(1);
    }
}
