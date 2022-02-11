﻿using UnityEngine;

namespace Game
{
    public class StunBehaviour : MonoBehaviour, Int_Damage
    {
        [SerializeField] private int _damage = 0;
        [SerializeField] private float _stunDuration = 2f;
        [SerializeField] private float _knockbackPower = 0.1f;
        public int Damage
        {
            get
            {
                return _damage;
            }
            set
            {
                _damage = value;
            }
        }
        public float StunDuration
        {
            get
            {
                return _stunDuration;
            }
            set
            {
                _stunDuration = value;
            }
        }
        public float KnockbackPower
        {
            get
            {
                return _knockbackPower;
            }
            set
            {
                _stunDuration = value;
            }

        }

        [SerializeField] private float _LifeTime = 0.3f;
        [SerializeField] private GameObject _Stun = null;

        void Start()
        {
            if(_Stun == null)
            {
                Debug.Log(this.gameObject + "n'a pas été assigné en tant que stun");
            }
            else
            {
                Destroy(_Stun, _LifeTime);
            }
        }
    }
}