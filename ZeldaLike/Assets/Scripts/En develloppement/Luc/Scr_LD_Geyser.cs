using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_Geyser : MonoBehaviour
{
    public float _repulseDelay;
    public float _repulseSpeed;
    public float _repulseDuration;
    public float _repulseRange;
    public LayerMask _whatIsPlayer;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Repulse(collision.gameObject.transform.parent.parent.position));
            Debug.Log("Target");
        }
    }

    /*void OnTriggerExit2D(Collider2D collision)
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
    }*/

    /*IEnumerator Boom()
    {
        yield return new WaitForSeconds(_repulseDelay);
        for (int i = 0; i < _enterGuys.Count; i++)
        {
            StartCoroutine(Repulse(i));
        }
        Debug.Log("Repulse !" + _enterGuys[0]);
        _startRepulse = false;
    }*/

    IEnumerator Repulse (Vector2 position)
    {
        float duration = _repulseDuration;

        Debug.Log("StartCoroutine");
        yield return new WaitForSeconds(_repulseDelay);

        Collider2D[] playerToRepulse = Physics2D.OverlapCircleAll(transform.position, _repulseRange, _whatIsPlayer);
        for (int i = 0; i < playerToRepulse.Length; i++)
        {
            while (duration > 0)
            {
                playerToRepulse[i].gameObject.transform.parent.parent.position = Vector2.MoveTowards(position, transform.position, _repulseSpeed);
                duration -= Time.deltaTime;
                Debug.Log("Repulse");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _repulseRange);
    }
}
