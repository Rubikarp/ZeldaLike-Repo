using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemis;

namespace Game
{
    public class Scr_HeavyAttack_Behavior : MonoBehaviour, Int_Damage
    {
        [SerializeField] private int _damage = 3;
        [SerializeField] private float _stunDuration = 0.3f;
        [SerializeField] private float _knockbackPower = 20f;
        public GameObject _shockWave;
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

        [SerializeField] private Movement_2D_TopDown _PlMovement;

        [SerializeField] private float _LifeTime = 0.8f;
        [SerializeField] private GameObject _heavyAttack = null;


        void Start()
        {
            _PlMovement = GameObject.Find("Physic").GetComponent<Movement_2D_TopDown>();

            if (_heavyAttack == null)
            {
                Debug.Log(this.gameObject + "n'a pas été assigné en tant que stun");
            }
            StartCoroutine(DestroyGameObjectIn(_heavyAttack, _LifeTime));
        }

        IEnumerator DestroyGameObjectIn(GameObject scriptGameobject, float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);

            _PlMovement._canMove = true;

            Destroy(scriptGameobject);
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
           if (collision.gameObject.CompareTag("Ennemis/HurtBox"))
           {
                if (collision.gameObject.GetComponent<Scr_EnnemisLifeSystem>()._isMarked == true)
                {
                    Instantiate(_shockWave, collision.transform.position, collision.transform.rotation);
                }
           }
        }
    }
}