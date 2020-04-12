using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class Movement_2D_TopDown : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null;
        [SerializeField] private InputManager _input = null;
        [Space(10)]
        public Scr_FormeHandler _forme = null;
        [Space(10)]
        [SerializeField] private float _activeSpeed = 0f;

        private float _accTimer = 0f;
        private float _decTimer = 0f;
        private float _RunDeadZone = 0.5f;

        public bool _canMove = true;
        public bool _isBoosted = false;

        void Update()
        {
            if (_canMove)
            {
                Run();
            }
        }

        void Run()
        {
            if (_input._stickMagnitude > _RunDeadZone && _canMove)
            {
                //incrémentation du timer en fonction du temps
                _accTimer += Time.deltaTime;


                //determine la vitesse
                if (_accTimer < _forme._actualForm._accelerationCurve.keys[_forme._actualForm._accelerationCurve.length - 1].time) //regarde le temps de la dernière key
                {
                    _activeSpeed = _forme._actualForm._maxSpeed * _forme._actualForm._accelerationCurve.Evaluate(_accTimer);
                }
                else
                {
                    _activeSpeed = _forme._actualForm._maxSpeed * _forme._actualForm._topSpeedCurve.Evaluate(_accTimer);
                }

                //applique la vitesse
                _body.velocity = _input._CharacterDirection * _activeSpeed;

                //Reset decceleration timer
                if (_decTimer != 0)
                {
                    _decTimer = 0f;
                }
            }
            else if(_canMove)
            {
                //incrémentation du timer en fonction du temps
                _decTimer += Time.deltaTime;

                //determine la vitesse
                _activeSpeed = _forme._actualForm._maxSpeed * _forme._actualForm._deccelerationCurve.Evaluate(_decTimer);

                //applique la vitesse
                _body.velocity = _input._CharacterDirection * _activeSpeed;

                //Reset decceleration timer
                if (_accTimer != 0)
                {
                    _accTimer = 0f;
                }
            }
        }

        public IEnumerator SpeedBoostCoroutine(float boostPourcentage, float boostDuration, Data_PlayerForme _boostedForm)
        {
            float basedSpeed = _boostedForm._maxSpeed;
            float boostedSpeed = basedSpeed + ((basedSpeed / 100) * boostPourcentage);

            _isBoosted = true;
            _boostedForm._maxSpeed = boostedSpeed;

            yield return new WaitForSeconds(boostDuration);

            _boostedForm._maxSpeed = basedSpeed;
            _isBoosted = false;

            yield return null;
        }
    
        public void Immobilize()
        {
            _body.velocity = Vector2.zero;
        }
    }
}