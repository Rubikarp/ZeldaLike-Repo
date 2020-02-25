using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject _otherPortal;
    public GameObject Player;
    public bool _canTP;
    public float _TPDelay;

    // Start is called before the first frame update
    void Start()
    {
        _canTP = true;
        _TPDelay = _otherPortal.GetComponent<PortalScript>()._TPDelay;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
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
