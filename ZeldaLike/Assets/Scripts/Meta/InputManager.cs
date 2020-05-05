using UnityEngine;

namespace Management
{
    /// Fait par Arthur Deleye
    ///
    /// [SerializeField] private InputManager _input;
    /// _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    [ExecuteInEditMode]
    public class InputManager : Singleton<InputManager>
    {
        public bool _canInput = true;
        public bool _360Controller = true;

        [Header("Stick")]
        //Direction du stick
        public Vector2 _stickDirection;

        public float _stickMagnitude;

        [Header("Interaction Button")]
        public bool _interaction;

        [Header("Attack Button")]
        public bool _attack;

        [Header("Interaction Button")]
        public bool _mark;

        [Header("Left Switch Button")]
        public bool _leftSwitch;

        [Header("Right Switch Button")]
        public bool _rightSwitch;

        [Header("Pause Button")]
        public bool _pause;

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

            _pause = Input.GetButtonDown("Pause");

            if (_canInput)
            {
                if (_360Controller)
                {
                    //Je prends les valeurs du stick
                    _stickDirection = new Vector2(Input.GetAxis("LStickAxisX"), Input.GetAxis("LStickAxisY"));
                    _stickMagnitude = _stickDirection.magnitude;

                    //Je prends les buttons
                    _mark = Input.GetButton("Y/Triangle");
                    _attack = Input.GetButton("X/Square");
                    _interaction = Input.GetButton("A/Cross");
                    _leftSwitch = Input.GetButton("LB/L1");
                    _rightSwitch = Input.GetButton("RB/R1");

                    //update de la direction du joueur
                    if (_stickDirection.magnitude >= 0.8f)
                    {
                        _CharacterDirection = _stickDirection;
                        _CharacterDirection.Normalize();
                    }
                }
                else
                {
                    //Je prends les valeurs du stick
                    _stickDirection = new Vector2(Input.GetAxis("Keyboard-AxisX"), Input.GetAxis("Keyboard-AxisY"));
                    _stickMagnitude = _stickDirection.magnitude;

                    //Je prends les buttons
                    _mark = Input.GetButton("Keyboard-MarkButton");
                    _attack = Input.GetButton("Keyboard-AttackButton");
                    _interaction = Input.GetButton("Keyboard-InteractionButton");
                    _leftSwitch = Input.GetButton("Keyboard-LeftSwitch");
                    _rightSwitch = Input.GetButton("Keyboard-RightSwitch");

                    //update de la direction du joueur
                    if (_stickDirection != Vector2.zero)
                    {
                        _CharacterDirection = _stickDirection.normalized;
                    }
                }
            }
        }

        public void DesactivateControl()
        {
            _canInput = false;

            _stickDirection = Vector2.zero;
            _stickMagnitude = 0;

            _interaction = false;
            _attack = false;
            _leftSwitch = false;
            _rightSwitch = false;
        }

        public void ReActivateControl()
        {
            _canInput = true;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(Vector2.zero, _CharacterDirection * 5 , Color.red);

        }
    }
}