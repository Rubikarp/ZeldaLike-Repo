using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_NoBrain : MonoBehaviour
    {
        [Header("Statistique")]
        public float _movementSpeed = 5f;

        [Header("Target")]
        public Transform target = null;

        [Header("Detection")]
        public float _detectionRange = 20f;
        public float _farRange = 10f;
        public float _attackRange = 8f;
        public float _nearRange = 5f;


        void Awake()
        {
            
        }
        
        void Start()
        {
            
        }
        
        void Update()
        {
            
        }
        
        
        
    }
}