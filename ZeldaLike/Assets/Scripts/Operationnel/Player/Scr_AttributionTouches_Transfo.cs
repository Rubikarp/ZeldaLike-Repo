using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_AttributionTouches_Transfo : MonoBehaviour
    {
        enum Forme { Humain, Agile, Lourd}
        Forme actualForm;

        [Header("Formes Stockés")]
        public Data_PlayerForme _human = null;
        public Data_PlayerForme _agile = null;
        public Data_PlayerForme _heavy = null;

        [Space(10)]

        [Header("SpriteParForme")]
        public GameObject _humanSprite = null;
        public GameObject _agileSprite = null;
        public GameObject _heavySprite = null;

        [Space(10)]

        [Header("Switch")]
        public Data_PlayerForme _leftForm;
        public Data_PlayerForme _actualForm;
        public Data_PlayerForme _rightForm;

        [Space(10)]

        public Movement_2D_TopDown PlayerMove;

        void Start()
        {
            _actualForm = _human;
            _humanSprite.SetActive(true);
            _agileSprite.SetActive(false);
            _heavySprite.SetActive(false);
            TransformCommandSwitch();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("LeftTransform"))
            {
                _actualForm = _leftForm;
                TransformCommandSwitch();
                RefreshActual();

                Debug.Log("Forme de gauche : " + _leftForm + "   Forme activée : " + _actualForm + "   Forme de droite : " + _rightForm);
            }
            else if (Input.GetButtonDown("RightTransform"))
            {
                _actualForm = _rightForm;
                TransformCommandSwitch();
                RefreshActual();

                Debug.Log("Forme de gauche : " + _leftForm + "   Forme activée : " + _actualForm + "   Forme de droite : " + _rightForm);
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
    }
}
