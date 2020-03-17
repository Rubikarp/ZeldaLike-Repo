using System.Collections;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject _otherPortal = null;
    public GameObject Player = null;
    public bool _canTP = true;
    public float _TPDelay = 1f;

    void Start()
    {
        _canTP = true;
        _TPDelay = _otherPortal.GetComponent<PortalScript>()._TPDelay;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canTP == true)
        {
            Player.transform.position = _otherPortal.transform.position;
            _otherPortal.GetComponent<PortalScript>()._canTP = false;
            StartCoroutine(OtherPortalLock());
        }
    }

    IEnumerator OtherPortalLock()
    {
        yield return new WaitForSeconds(_TPDelay);
        _otherPortal.GetComponent<PortalScript>()._canTP = true;
    }
}
