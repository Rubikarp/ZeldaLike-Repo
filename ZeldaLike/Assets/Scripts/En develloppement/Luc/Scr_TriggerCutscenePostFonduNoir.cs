using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Narration;

namespace Narration
{
    public class Scr_TriggerCutscenePostFonduNoir : MonoBehaviour
    {
        private bool _cutsceneActive;
        public float _cutsceneDuration;
        public GameObject _ancien;

        // Start is called before the first frame update
        void Start()
        {
            _ancien.SetActive(true);
            _cutsceneActive = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_cutsceneActive == true && _cutsceneDuration > 0)
            {
                _cutsceneDuration -= Time.deltaTime;
            }
            else if (_cutsceneActive == true && _cutsceneDuration <= 0)
            {
                FindObjectOfType<InputManager>().ReActivateControl();
            }
        }
    }
}
