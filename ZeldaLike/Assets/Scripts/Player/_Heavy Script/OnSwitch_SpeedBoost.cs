﻿using UnityEngine;

namespace Game
{
    public class OnSwitch_SpeedBoost : MonoBehaviour
    {
        [Header("Information")]
        public Data_PlayerForme _boostedForm = null;
        public Movement_2D_TopDown Movement = null;
        [SerializeField] private SoundManager sound;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

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

        public void SpeedBoost()
        {
            sound.PlaySound("OnSwitchCharge");

            if (!Movement._isBoosted)
            {
                Movement.StartCoroutine(Movement.SpeedBoostCoroutine(_boostPourcentage, _boostDuration, _boostedForm));
            }
        }
    }
}