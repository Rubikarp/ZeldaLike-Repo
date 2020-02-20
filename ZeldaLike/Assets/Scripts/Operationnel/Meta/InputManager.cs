using UnityEngine;

namespace Management
{
    /// <summary>
    /// Fait par Arthur Deleye
    /// 
    /// private InputManager input;        
    /// input = GameObject.FindGameObjectWithTag("GameController");
    /// 
    /// </summary>
    public class InputManager : Singleton<InputManager>
    {
        #region Input

        [Header("Inputs")]

        #region LeftStick

        [HeaderAttribute("Stick")]

        //Direction du stick
        public Vector2 _stickDirection;
        public Vector2 _stickDirectionNorm;
        public float _stickMagnitude;


        #endregion LeftStick

        #region Buttons

        [HeaderAttribute("Interaction")]
        public bool _interactionEnter;
        public bool _interaction;
        public bool _interactionExit;

        [HeaderAttribute("Attack")]
        public bool _attackEnter;
        public bool _attack;
        public bool _attackExit;

        #endregion Buttons

        #endregion Input

        private void Update()
        {
            #region PrendsLesInputs

            //Je prends les valeurs du stick
            _stickDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _stickDirectionNorm = _stickDirection.normalized;
            _stickMagnitude = _stickDirection.magnitude;

            //Je prends les buttons
            _interaction = Input.GetButton("Interaction");
            _interactionEnter = Input.GetButtonDown("Interaction");
            _interactionExit = Input.GetButtonUp("Interaction");

            _attack = Input.GetButton("Attack");
            _attackEnter = Input.GetButtonDown("Attack");
            _attackExit = Input.GetButtonUp("Attack");

            #endregion PrendsLesInputs
        }
    }
}