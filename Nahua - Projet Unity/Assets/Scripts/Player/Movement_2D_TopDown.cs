﻿using System.Collections;
using UnityEngine;
using Management;

namespace Game
{
    public class Movement_2D_TopDown : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body = null;
        [SerializeField] private InputManager _input = null;
        [SerializeField] private Scr_PlayerLifeSystem lifeSyst = null;
        [SerializeField] private SoundManager sound;

        [Space(10)]
        public Scr_FormeHandler _forme = null;
        [Space(10)]
        public float _activeSpeed = 0f;
        private float _accTimer = 0f;
        private float _decTimer = 0f;
        private float _RunDeadZone = 0.5f;

        public bool _canMove = true;
        public bool _isBoosted = false;

        private float horloge = 0.3f;
        private float horlogeDecompte = 0.3f;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

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
                _body.velocity = _input._CharacterDirection * _activeSpeed * _input._stickMagnitude;


                if(horlogeDecompte > 0)
                {
                    horlogeDecompte -= Time.deltaTime;
                }
                else
                {
                    GroundSound();
                    horlogeDecompte = horloge;
                }

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
                _body.velocity = _input._CharacterDirection * _activeSpeed * _input._stickMagnitude;

                //Reset decceleration timer
                if (_accTimer != 0)
                {
                    _accTimer = 0f;
                }
            }
        }
    
        void GroundSound()
        {
            if (_forme._switchForm == Scr_FormeHandler.Forme.Humain)
            {
                sound.PlaySound("PasHuman");
            }
            else
            if (_forme._switchForm == Scr_FormeHandler.Forme.Agile)
            {
                sound.PlaySound("PasFeline");
            }
            else
            if (_forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                sound.PlaySound("PasDino");
            }
        }

        public void Immobilize()
        {
            _body.velocity = Vector2.zero;
        }
    }
}