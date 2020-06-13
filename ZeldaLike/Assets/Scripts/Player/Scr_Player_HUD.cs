using Management;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Scr_Player_HUD : MonoBehaviour
    {
        public enum Forme { Humain, Agile, Lourd }

        [Header("Component")]
         private InputManager _input = default;
        [SerializeField] private Scr_FormeHandler _forme = default;
        [SerializeField] private PlayerLife lifeSystem = default;
        [Space(5)]
        [SerializeField] private Actif_HeavyAttack heavyActif = default;
        [SerializeField] private Actif_KnifeThrowing humanActif = default;
        [SerializeField] private Actif_AgileAttack agileActif = default;

        [Header("Element de l'UI")]
        public Image[] coeur = default;
        public GameObject[] extraCoeur = default;
        [Space(5)]
        public RectTransform _formePalette = default;
        public RectTransform _formeAgile = default;
        public RectTransform _formeHeavy = default;
        public RectTransform _formeHumain = default;
        [Space(5)]
        public Image _attackButton = default;
        public Image _markButton = default;
        public Transform _markContainer = default;

        [Header("Variable")]
        [SerializeField] private Color inactiveColor = Color.grey;
        private bool CanAttack = true;
        [SerializeField] private float switchDuration = 0.2f;
        private float littleSize = 0.3f;
        private float largeSize = 0.4f;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            switchDuration = _forme._switchCooldown - 0.1f;
        }

        public void lifeBarUpdate(int health)
        {
            switch (health)
            {
                case 0:
                    {
                        foreach (Image allImage in coeur)
                        {
                            allImage.rectTransform.LeanScale(Vector3.zero, 0.3f);
                        }
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 1)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 2)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 3)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 4)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                    }
                    break;
                case 5:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 5)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                    }
                    break;
                case 6: //Max de départ
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 6)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                    }
                    break;
                case 7:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 7)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                        extraCoeur[0].SetActive(true);

                    }
                    break;
                case 8:
                    {
                        for (int i = 0; i < coeur.Length; i++)
                        {
                            if (i < 8)
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.one, 0.3f);
                            }
                            else
                            {
                                coeur[i].rectTransform.LeanScale(Vector3.zero, 0.3f);
                            }
                        }
                        extraCoeur[1].SetActive(true);

                    }
                    break;
                case 9:
                    {
                        foreach (Image allImage in coeur)
                        {
                            allImage.rectTransform.LeanScale(Vector3.one, 0.3f);
                        }
                        extraCoeur[2].SetActive(true);

                    }
                    break;
                default:
                    {
                        Debug.Log("Vie inférieur à zéro");
                    }
                    break;
            }
        }

        public void SwitchUIdino()
        {
            _formePalette.LeanRotateZ(-120, switchDuration);

            _formeHumain.LeanScaleX(littleSize, switchDuration);
            _formeHumain.LeanScaleY(littleSize, switchDuration);
            _formeHumain.LeanRotateZ(0, switchDuration);

            _formeAgile.LeanScaleX(littleSize, switchDuration);
            _formeAgile.LeanScaleY(littleSize, switchDuration);
            _formeAgile.LeanRotateZ(0, switchDuration);

            _formeHeavy.LeanScaleX(largeSize, switchDuration);
            _formeHeavy.LeanScaleY(largeSize, switchDuration);
            _formeHeavy.LeanRotateZ(0, switchDuration);
        }
        public void SwitchUIhuman()
        {
            _formePalette.LeanRotateZ(0, switchDuration);

            _formeHumain.LeanScaleX(largeSize, switchDuration);
            _formeHumain.LeanScaleY(largeSize, switchDuration);
            _formeHumain.LeanRotateZ(0, switchDuration);

            _formeAgile.LeanScaleX(littleSize, switchDuration);
            _formeAgile.LeanScaleY(littleSize, switchDuration);
            _formeAgile.LeanRotateZ(0, switchDuration);

            _formeHeavy.LeanScaleX(littleSize, switchDuration);
            _formeHeavy.LeanScaleY(littleSize, switchDuration);
            _formeHeavy.LeanRotateZ(0, switchDuration);
        }
        public void SwitchUIfeline()
        {
            _formePalette.LeanRotateZ(120, switchDuration);

            _formeHumain.LeanScaleX(littleSize, switchDuration);
            _formeHumain.LeanScaleY(littleSize, switchDuration);
            _formeHumain.LeanRotateZ(0, switchDuration);

            _formeAgile.LeanScaleX(largeSize, switchDuration);
            _formeAgile.LeanScaleY(largeSize, switchDuration);
            _formeAgile.LeanRotateZ(0, switchDuration);

            _formeHeavy.LeanScaleX(littleSize, switchDuration);
            _formeHeavy.LeanScaleY(littleSize, switchDuration);
            _formeHeavy.LeanRotateZ(0, switchDuration);
        }

        /*
        private void RefreshInput()
        {
            if (_forme._switchForm == Scr_FormeHandler.Forme.Humain)
            {
                CanAttack = humanActif._canShoot;

            }
            else
            if (_forme._switchForm == Scr_FormeHandler.Forme.Agile)
            {
                CanAttack = agileActif._canAttack;

            }
            else
            if (_forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                CanAttack = heavyActif._canAttack;

            }

            if (CanAttack)
            {
                _attackButton.color = Color.white;
            }
            else
            {
                _attackButton.color = inactiveColor;
            }

            if (_markContainer.childCount != 0)
            {
                _markButton.color = Color.white;
            }
            else
            {
                _markButton.color = inactiveColor;
            }
        }
        */
    }
}