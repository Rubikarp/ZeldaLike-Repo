using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Ennemis;

namespace Game
{
    public class Actif_AgileAttack : MonoBehaviour
    {
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;
        [SerializeField] private InputManager _input = null;
        [SerializeField] private GameObject _Avatar = null;
        private Transform _myTranfo = null;
        public float _jumpRange = 5f;

        [Header("Attaque data")]
        public Transform _attackContainer;
        public Transform _attackPos;
        [SerializeField] private bool _canAttack;

        [Header("Attaque classique")]
        public GameObject _attackObj;

        [Header("Bond")]
        public GameObject _bondObj;
        public GameObject _playerHurtbox;


        public float _bondCooldown = 1;
        public float _attackCooldown = 1;

        void Start()
        {
            _canAttack = true;
            _Avatar = this.gameObject;
            _myTranfo = _Avatar.transform;
        }

        void Update()
        {
            if (Input.GetButtonDown("Attack") && _canAttack == true)
            {
                _canAttack = false;

                RaycastHit2D isBleedingEnnemis = Physics2D.Raycast(_myTranfo.position, _input._CharacterDirection, _jumpRange);
                bool _isEnnemis = isBleedingEnnemis.collider.gameObject.CompareTag("Ennemis");
                bool _isBleeding = false;

                if (_isEnnemis)
                {
                    _isBleeding = isBleedingEnnemis.collider.gameObject.GetComponentInChildren<Int_EnnemisLifeSystem>().IsBleeding;
                }
                
                
                if (_isBleeding)
                {
                    //attaque classique

                    Debug.Log("attaque bond");
                    //Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackContainer);

                    StartCoroutine(AttaqueDelay(_bondCooldown));
                }
                else
                {
                    //attaque classique
                    _PlMovement._canMove = false;
                    _PlMovement.Immobilize();
                    Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackContainer);
                    StartCoroutine(AttaqueDelay(_attackCooldown));
                }
            }
        }

        IEnumerator AttaqueDelay(float Cooldown)
        {
            yield return new WaitForSeconds(Cooldown);
            _canAttack = true;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_myTranfo.position, _input._CharacterDirection.normalized * _jumpRange, Color.blue);
        }
    }
}