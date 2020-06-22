using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_BondAttack_Behavior : MonoBehaviour, Int_Damage
    {
        [SerializeField] private int _damage = 3;
        [SerializeField] private float _stunDuration = 0.3f;
        [SerializeField] private float _knockbackPower = 20f;
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
                _knockbackPower = value;
            }
        }

        [SerializeField] private float _LifeTime = 4f;
        [SerializeField] private GameObject _bondAttack = null;


        void Start()
        {
            if (_bondAttack == null)
            {
                Debug.Log(this.gameObject + "n'a pas été assigné en tant que stun");
            }

            Destroy(_bondAttack, _LifeTime);
        }
    }
}