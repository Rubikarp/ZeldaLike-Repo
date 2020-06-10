using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class CineControlDesactivate : MonoBehaviour
    {

        [SerializeField] private InputManager _input;


        void Awake()
        {
            
        }
        
        void Start()
        {
           _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

        }

        void Update()
        {
            
        }
        
        
        
    }
}