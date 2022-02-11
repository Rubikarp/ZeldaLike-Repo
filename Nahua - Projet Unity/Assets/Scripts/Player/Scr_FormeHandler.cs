﻿using Game;
using UnityEngine;
using UnityEngine.Rendering;

namespace Management
{
    public class Scr_FormeHandler : MonoBehaviour
    {
        public enum Forme { Humain, Agile, Heavy }

        [SerializeField] public Forme _switchForm;

        [Header("Components")]
        private InputManager _input;

        [Header("Formes Stockés")]
        public Data_PlayerForme _actualForm = null;

        [SerializeField] private Data_PlayerForme _humanForme = null;
        [SerializeField] private Data_PlayerForme _agileForme = null;
        [SerializeField] private Data_PlayerForme _heavyForme = null;

        [Header("GameObject Forme")]
        public GameObject _humanGameObject = null;

        public GameObject _agileGameObject = null;
        public GameObject _heavyGameObject = null;

        [Space(10f)]
        [Header("Variable")]
        [SerializeField] private Scr_Player_HUD HUD = null;
        [SerializeField] private Scr_PlayerLifeSystem lifesyst = null;

        public bool _canSwitch = true;

        public bool _heavyFormeUnlock = false;

        [Header("Variable à Tweek")]
        public float _switchCooldown = 0.5f;

        private float _switchTimer = 0.5f;

        private SoundManager sound;

        private void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            Initialisation();
            _switchTimer = _switchCooldown;
        }

        private void Update()
        {
            if (_canSwitch)
            {
                if (_input._leftSwitch)
                {
                    _canSwitch = false;
                    _switchTimer = _switchCooldown;

                    RoulementGauche();

                    _switchTimer = _switchCooldown;
                    lifesyst._isTakingDamage = false;
                }
                else
                if (_input._rightSwitch)
                {
                    _canSwitch = false;
                    _switchTimer = _switchCooldown;

                    RoulementDroite();

                    _switchTimer = _switchCooldown;
                    lifesyst._isTakingDamage = false;
                }
            }
            else
            {
                if (_switchTimer <= 0)
                {
                    _canSwitch = true;
                } 
                else 
                {
                    _switchTimer -= Time.deltaTime;
                }
            }
        }

        //Switch
        private void RoulementDroite()
        {
            switch (_switchForm)
            {
                case Forme.Humain:
                    AgileActivation();
                    break;

                case Forme.Agile:
                    if (_heavyFormeUnlock)
                    { HeavyActivation(); }
                    else
                    { HumainActivation(); }
                    break;

                case Forme.Heavy:
                    HumainActivation();
                    break;

                default:
                    Debug.LogError("problem in the switch");
                    break;
            }
            sound.PlaySound("FormeChanged");
        }
        private void RoulementGauche()
        {
            switch (_switchForm)
            {
                case Forme.Humain:
                    if (_heavyFormeUnlock)
                    { HeavyActivation(); }
                    else
                    { AgileActivation(); }
                    break;

                case Forme.Agile:
                    HumainActivation();
                    break;

                case Forme.Heavy:
                    AgileActivation();
                    break;

                default:
                    Debug.Log("problem in the switch");
                    break;
            }
            sound.PlaySound("FormeChanged");
        }

        //Activer une forme
        public void HumainActivation()
        {
            _switchForm = Forme.Humain;
            _actualForm = _humanForme;

            _agileGameObject.SetActive(false);
            _heavyGameObject.SetActive(false);

            _humanGameObject.SetActive(true);

            HUD.SwitchUIhuman();
        }
        public void AgileActivation()
        {
            _switchForm = Forme.Agile;
            _actualForm = _agileForme;

            _humanGameObject.SetActive(false);
            _heavyGameObject.SetActive(false);

            _agileGameObject.SetActive(true);

            HUD.SwitchUIfeline();
        }
        public void HeavyActivation()
        {
            _switchForm = Forme.Heavy;
            _actualForm = _heavyForme;

            _humanGameObject.SetActive(false);
            _agileGameObject.SetActive(false);

            _heavyGameObject.SetActive(true);

            HUD.SwitchUIdino();
        }

        //Activer une forme
        public void UnlockHeavy()
        {
            _heavyFormeUnlock = true;
        }

    private void Initialisation()
        {
            switch (_switchForm)
            {
                case Forme.Humain:

                    HumainActivation();
                    break;

                case Forme.Agile:

                    AgileActivation();
                    break;

                case Forme.Heavy:

                    HeavyActivation();
                    break;

                default:
                    Debug.Log("problem in the switch");
                    break;
            }
        }
    }
}