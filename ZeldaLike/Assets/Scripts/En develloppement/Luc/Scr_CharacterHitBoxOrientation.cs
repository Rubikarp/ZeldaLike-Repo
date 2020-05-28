using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class Scr_CharacterHitBoxOrientation : MonoBehaviour
    {
        public Transform _mySelf;
        private float _rotZ;
        private Vector2 _playerDir;
        private InputManager _input;
        public GameObject _formHandler;

        private void Awake()
        {
            _input = InputManager.Instance;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_formHandler.GetComponent<Scr_FormeHandler>()._switchForm == Scr_FormeHandler.Forme.Humain)
            {
                _mySelf.localScale = new Vector3(0.5f, 0.5f, 1);
            }
            else if (_formHandler.GetComponent<Scr_FormeHandler>()._switchForm == Scr_FormeHandler.Forme.Agile)
            {
                _mySelf.localScale = new Vector3(1, 0.5f, 1);
            }
            else if (_formHandler.GetComponent<Scr_FormeHandler>()._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                _mySelf.localScale = new Vector3(1.25f, 0.75f, 1);
            }
            _playerDir = _input._CharacterDirection;
            _rotZ = Mathf.Atan2(_playerDir.y, _playerDir.x) * Mathf.Rad2Deg;
            _mySelf.rotation = Quaternion.Euler(0f, 0f, _rotZ);
        }
    }
}

