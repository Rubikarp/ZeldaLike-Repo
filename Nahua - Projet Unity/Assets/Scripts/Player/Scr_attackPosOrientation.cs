using UnityEngine;
using Management;

namespace Game
{
    public class Scr_attackPosOrientation : MonoBehaviour
    {
        [SerializeField] 
        private InputManager _input = null;
        [SerializeField] 
        private Vector2 _playerDirection = Vector2.zero;
        public float _angleCorrection = 0;

        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        void Update()
        {
            _playerDirection = _input._CharacterDirection;

            FacePlayerDirection(_playerDirection);
        }

        private void FacePlayerDirection(Vector2 playerDirection)
        {
            //calcul l'angle pour faire face au joueur
            float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg + _angleCorrection;
            //oriente l'object pour faire face au joueur
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }

    }
}