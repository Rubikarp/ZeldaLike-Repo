using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Management;

public class Scr_Cine_Trigger : MonoBehaviour
{

    public PlayableDirector _playableDirector;
    private bool _isLaunched;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isLaunched == false)
        {
            _playableDirector.Play();
            FindObjectOfType<InputManager>().DesactivateControl();
            _isLaunched = true;
        }

    }
}
