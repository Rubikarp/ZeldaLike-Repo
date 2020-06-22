using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class CineControlDesactivate : MonoBehaviour
    {
        [SerializeField] private InputManager _input;

        private void OnEnable()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            _input.DesactivateControl();
        }

        private void OnDisable()
        {
            _input.ReActivateControl();
        }        
    }
}