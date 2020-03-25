using UnityEngine;

namespace Management
{
    /// <summary>
    /// Fait par Arthur Deleye
    /// 
    /// private InputManager _input;        
    /// 
    /// _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    /// </summary>
    public class InputManager : Singleton<InputManager>
    {
        [Space(10)]
        public bool _360Controller = true;

        [Header("Stick")]
        //Direction du stick
        public Vector2 _stickDirection;
        public float _stickMagnitude;

        [Header("Interaction Button")]
        public bool _interaction;

        [Header("Attack Button")]
        public bool _attack;

        [Header("Left Switch Button")]
        public bool _leftSwitch;

        [Header("Right Switch Button")]
        public bool _rightSwitch;

        [Header("Avatar facing direction")]
        //Dernière direction
        public Vector2 _CharacterDirection = Vector2.zero;

        private void Update()
        {
            //changement de controller
            if (Input.GetButtonDown("SwitchController"))
            {
                _360Controller = !_360Controller;
            }

            if (_360Controller)
            {
                //Je prends les valeurs du stick
                _stickDirection = new Vector2(Input.GetAxis("LStickAxisX"), Input.GetAxis("LStickAxisY"));
                _stickMagnitude = _stickDirection.magnitude;

                //Je prends les buttons
                _interaction = Input.GetButton("A/Cross");
                _attack = Input.GetButton("B/Circle");
                _leftSwitch = Input.GetButton("LB/L1");
                _rightSwitch = Input.GetButton("RB/R1");

                //update de la direction du joueur
                _CharacterDirection = characterDirection(_stickDirection.normalized, _CharacterDirection);
            }
            else
            {
                //Je prends les valeurs du stick
                _stickDirection = new Vector2(Input.GetAxis("Keyboard-AxisX"), Input.GetAxis("Keyboard-AxisY"));
                _stickMagnitude = _stickDirection.magnitude;

                //Je prends les buttons
                _interaction = Input.GetButton("Keyboard-InteractionButton");
                _attack = Input.GetButton("Keyboard-AttackButton");
                _leftSwitch = Input.GetButton("Keyboard-LeftSwitch");
                _rightSwitch = Input.GetButton("Keyboard-RightSwitch");

                //update de la direction du joueur
                _CharacterDirection = characterDirection(_stickDirection.normalized, _CharacterDirection);

            }
        }

        Vector2 characterDirection(Vector2 directionBrut, Vector2 _CharacterDirection)
        {
            if (directionBrut != Vector2.zero)
            {
                return directionBrut.normalized;
            }
            else //Remain last character direction
            {
                return _CharacterDirection;
            }
        }
    
    }
}