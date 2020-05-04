using System.Collections;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject _otherPortal = null;
    public GameObject Player = null;
    public bool _canTP = true;
    public bool _canTPBlocks;
    public float _TPDelay = 1f;
    public float _TPBlocksDelay;

    void Start()
    {
        _canTP = true;
        _canTPBlocks = true;
        _TPDelay = _otherPortal.GetComponent<PortalScript>()._TPDelay;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canTP == true)
        {
            Player.transform.position = _otherPortal.transform.position;
            _otherPortal.GetComponent<PortalScript>()._canTP = false;
            StartCoroutine(OtherPortalLockPlayer());
        }
        else if (collision.gameObject.CompareTag("Environment") && _canTPBlocks)
        {
            collision.gameObject.transform.position = _otherPortal.transform.position;
            _otherPortal.GetComponent<PortalScript>()._canTPBlocks = false;
            StartCoroutine(OtherPortalLockBlocks());
        }
    }

    IEnumerator OtherPortalLockPlayer()
    {
        yield return new WaitForSeconds(_TPDelay);
        _otherPortal.GetComponent<PortalScript>()._canTP = true;
    }

    IEnumerator OtherPortalLockBlocks()
    {
        yield return new WaitForSeconds(_TPBlocksDelay);
        _otherPortal.GetComponent<PortalScript>()._canTPBlocks = true;
    }
}

