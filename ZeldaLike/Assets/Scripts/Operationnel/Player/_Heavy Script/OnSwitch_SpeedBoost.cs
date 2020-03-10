using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class OnSwitch_SpeedBoost : MonoBehaviour
    {
        [Header("Information")]
        public Data_PlayerForme _boostedForm = null;
        public Movement_2D_TopDown Movement = null;

        [Header("Variable")]
        [Range(0, 200)]
        public float _boostPourcentage = 40;
        public float _boostDuration = 3;

        private void OnEnable()
        {
            if (!Movement._isBoosted)
            {
                Movement.StartCoroutine(Movement.SpeedBoostCoroutine(_boostPourcentage, _boostDuration, _boostedForm));
            }
        }      
        
    }
}