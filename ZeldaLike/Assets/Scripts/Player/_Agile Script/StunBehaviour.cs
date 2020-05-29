using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
            gameObject.LeanScale(new Vector3(2, 2, 1), _LifeTime>0.2f? _LifeTime - 0.2f : _LifeTime);

            if(_Stun == null)
            {
                Debug.Log(this.gameObject + "n'a pas été assigné en tant que stun");
            }
            Destroy(_Stun, _LifeTime);
        }

        private void Update()
        {
            if (_LifeTime > 0)
            {
                _LifeTime -= Time.deltaTime;
            }
            else if (_LifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}