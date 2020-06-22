using UnityEngine;
using Management;

namespace Game
{
    public class Scr_AttackPosManipulator : MonoBehaviour
    {
        [SerializeField]
        private InputManager _input = null;
        [SerializeField]
        private Transform _attackPos = null;
        [SerializeField]
        private Transform _avatar = null;

        public float _allonge = 1f;
        public Vector3 _correctionPos = Vector3.up;

        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }
        
        void Update()
        {
            Vector3 _decalage = new Vector3(_input._CharacterDirection.x, _input._CharacterDirection.y, 0).normalized * _allonge;
            _attackPos.position = _avatar.transform.position + _correctionPos + _decalage;
        }
    }
}