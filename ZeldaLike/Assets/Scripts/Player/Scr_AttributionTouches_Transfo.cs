using UnityEngine;
using Management;

namespace Game
{
    public class Scr_AttributionTouches_Transfo : MonoBehaviour
    {
        enum Forme { Humain, Agile, Lourd }
        Forme actualForm;

        [Header("Components")]
        public Movement_2D_TopDown PlayerMove;
        private InputManager _input;

        [Space(10f)]
        
        [Header("Variable")]
        public bool _canSwitch = true;
        public float _switchTimer = 0f;


        [Header("Variable à Tweek")]
        public float _switchCooldown = 0.3f;

        [Header("Formes Stockés")]
        public Data_PlayerForme _human = null;
        public Data_PlayerForme _agile = null;
        public Data_PlayerForme _heavy = null;

        private Data_PlayerForme _leftForm = null;
        private Data_PlayerForme _actualForm = null;
        private Data_PlayerForme _rightForm = null;

        [Space(10)]

        [Header("SpriteParForme")]
        public GameObject _humanSprite = null;
        public GameObject _agileSprite = null;
        public GameObject _heavySprite = null;

        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            InitialisationHumanForm();
        }

        void Update()
        {
            if (_canSwitch)
            {
                if (_input._leftSwitch)
                {
                    _actualForm = _leftForm;
                    TransformCommandSwitch();
                    RefreshActual();

                    _canSwitch = false;
                    _switchTimer = _switchCooldown;

                    //Debug.Log("Forme de gauche : " + _leftForm + "   Forme activée : " + _actualForm + "   Forme de droite : " + _rightForm);
                }
                else
                if (_input._rightSwitch)
                {
                    _actualForm = _rightForm;
                    TransformCommandSwitch();
                    RefreshActual();

                    _canSwitch = false;
                    _switchTimer = _switchCooldown;

                    //Debug.Log("Forme de gauche : " + _leftForm + "   Forme activée : " + _actualForm + "   Forme de droite : " + _rightForm);
                }
            }
            else
            {
                if(_switchTimer <= 0)
                {
                    _canSwitch = true;
                }
                else
                {
                    _switchTimer -= Time.deltaTime;
                }
            }
        }

        private void TransformCommandSwitch()
        {

            if (_actualForm == _human)
            {
                actualForm = Forme.Humain;

                _leftForm = _heavy;
                _rightForm = _agile;
            }
            else if (_actualForm == _agile)
            {
                actualForm = Forme.Agile;

                _leftForm = _human;
                _rightForm = _heavy;
            }
            else if (_actualForm == _heavy)
            {
                actualForm = Forme.Lourd;

                _leftForm = _agile;
                _rightForm = _human;
            }
        }
    
        void RefreshActual()
        {
            PlayerMove._actualForme = _actualForm;

            switch (actualForm)
            {
                case Forme.Humain:

                    _humanSprite.SetActive(true);
                    _agileSprite.SetActive(false);
                    _heavySprite.SetActive(false);
                    break;

                case Forme.Agile:

                    _humanSprite.SetActive(false);
                    _agileSprite.SetActive(true);
                    _heavySprite.SetActive(false);
                    break;

                case Forme.Lourd:

                    _humanSprite.SetActive(false);
                    _agileSprite.SetActive(false);
                    _heavySprite.SetActive(true);
                    break;

                default:
                    Debug.Log("problem in the switch");
                    break;
            }
        } 
    
        void InitialisationHumanForm()
        {
            _actualForm = _human;
            _leftForm = _heavy;
            _rightForm = _agile;

            _humanSprite.SetActive(true);
            _agileSprite.SetActive(false);
            _heavySprite.SetActive(false);

            TransformCommandSwitch();
        }
    }
}
