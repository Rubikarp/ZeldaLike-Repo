﻿using System.Collections;
using UnityEngine;
using Management;

namespace Game
{
    public class Actif_HeavyAttack : MonoBehaviour
    {
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;
        [SerializeField] private InputManager _input = null;
        [SerializeField] private AnimatorManager_Player _animator = null;


        public GameObject _attackObj;
        public Transform _attackContainer;
        public Transform _attackPos;
        [SerializeField] private float _animDecal = 0.2f;

        public bool _canAttack;

        public float _attackCooldown;

        void Start()
        {
            _canAttack = true;
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

        }

        void Update()
        {
            if (_input._attack && _canAttack == true)
            {
                _canAttack = false;
                _PlMovement._canMove = false;

                StartCoroutine(Attaque(_animDecal));
            }
        }

        IEnumerator Attaque(float _animDecal)
        {
            _animator.TriggerAttack();
            _input.DesactivateControl();

            while (0 < _animDecal) // boucle durant la durée du dash
            {
                _animDecal -= Time.deltaTime;

                yield return new WaitForEndOfFrame();   // Retour à la prochaine frame
            }

            _input.ReActivateControl();

            Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackPos.transform);

            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }

        private void OnEnable()
        {
            _canAttack = true;
        }
    }
}