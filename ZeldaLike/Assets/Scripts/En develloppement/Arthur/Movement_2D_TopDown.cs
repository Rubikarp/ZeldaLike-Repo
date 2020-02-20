using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class Movement_2D_TopDown : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body = null;
        [SerializeField] private InputManager input = null;

        [SerializeField] Vector2 _CharacterDirection = Vector2.zero;
        public float _maxSpeed = 4f;
        [SerializeField] private float _activeSpeed = 0f;

        public float _accelerationTime = 0.5f;
        public float _deccelerationTime = 0.2f;

        [SerializeField]
        private float _accTimer = 0f, _decTimer = 0f;
        private float _RunDeadZone = 0.5f;

        /*[SerializeField]*/ AnimationCurve _accelerationCurve = null;
        /*[SerializeField]*/ AnimationCurve _topSpeedCurve = null;
        /*[SerializeField]*/ AnimationCurve _deccelerationCurve = null;
        
        void Start()
        {
            if (_accelerationCurve == null && _topSpeedCurve == null && _deccelerationCurve == null)
            {
                RefreshRunCurve(_accelerationTime, _maxSpeed, _deccelerationTime);
            }
        }
        
        void Update()
        {
            if(input._stickDirectionNorm != Vector2.zero)
            {
                _CharacterDirection = input._stickDirectionNorm;
            }

            Run();
        }

        void RefreshRunCurve(float accelTime, float maxSpeed, float deccelTime)
        {
            _accelerationCurve = AnimationCurve.EaseInOut(0, 0, accelTime, maxSpeed);
            _topSpeedCurve = AnimationCurve.Constant(0, 1, maxSpeed);
            _deccelerationCurve = AnimationCurve.EaseInOut(0, maxSpeed, deccelTime, 0);
        }

        void Run()
        {
            if (input._stickMagnitude > _RunDeadZone)
            {
                //incrémentation du timer en fonction du temps
                _accTimer += Time.deltaTime;


                //determine la vitesse
                if (_accTimer < _accelerationTime)
                {
                    _activeSpeed = _accelerationCurve.Evaluate(_accTimer);
                }
                else
                {
                    _activeSpeed = _topSpeedCurve.Evaluate(_accTimer);
                }

                //applique la vitesse
                body.velocity = _CharacterDirection * _activeSpeed;

                //Reset decceleration timer
                if (_decTimer != 0)
                {
                    _decTimer = 0f;
                }
            }
            else
            {
                //incrémentation du timer en fonction du temps
                _decTimer += Time.deltaTime;

                //determine la vitesse
                _activeSpeed = _deccelerationCurve.Evaluate(_decTimer);

                //applique la vitesse
                body.velocity = _CharacterDirection * _activeSpeed;

                //Reset decceleration timer
                if (_accTimer != 0)
                {
                    _accTimer = 0f;
                }
            }
        }
    }
}