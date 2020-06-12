using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class Scr_DebugCutsceneMorio : MonoBehaviour
{
    public GameObject _combatManager;
    private bool _isReseting;
    private float _delay;
    // Start is called before the first frame update
    void Start()
    {
        _isReseting = false;
        _delay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isReseting == true)
        {
            if (_delay > 0)
            {
                _delay -= Time.deltaTime;
                GetComponent<ButtonEliminate>().container = GameObject.Find("EnnemisVD").transform;
            }
            else if (_delay <= 0)
            {
                _isReseting = false;
                _delay = 1;
            }
        }
    }

    public void ResetCutsceneTrigger()
    {
        _isReseting = true;
    }
}
