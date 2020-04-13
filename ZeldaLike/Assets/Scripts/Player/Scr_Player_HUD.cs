using Management;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Scr_Player_HUD : MonoBehaviour
    {
        public enum Forme { Humain, Agile, Lourd }

        [SerializeField] private InputManager _input = default;
        [SerializeField] private Scr_FormeHandler _forme = default;
        [SerializeField] private Scr_PlayerLifeSystem _lifeSystem = default;

        [SerializeField] public Image[] coeur = default;
        [SerializeField] public RectTransform _formePalette = default;

        private float switchDuration = 0.3f;

        private float littleSize = 0.3f;
        private float largeSize = 0.4f;

        [SerializeField] public RectTransform _formeAgile = default;
        [SerializeField] public RectTransform _formeHeavy = default;
        [SerializeField] public RectTransform _formeHumain = default;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        private void Update()
        {
            lifeBarUpdate(_lifeSystem._life);
            FormePalette();
        }

        private void lifeBarUpdate(int health)
        {
            switch (health)
            {
                case 0:
                    {
                        foreach (Image allImage in coeur)
                        {
                            allImage.gameObject.SetActive(false);
                        }
                    }
                    break;

                case 1:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 1)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 2:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 2)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 3:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 3)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 4:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 4)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 5:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 5)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 6: //Max de départ
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 6)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 7:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 7)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 8:
                    {
                        for (int i = 1; i < coeur.Length; i++)
                        {
                            if (i <= 8)
                            {
                                coeur[i].gameObject.SetActive(true);
                            }
                            else
                            {
                                coeur[i].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case 9:
                    {
                        foreach (Image allImage in coeur)
                        {
                            allImage.gameObject.SetActive(true);
                        }
                    }
                    break;

                default:
                    {
                        Debug.LogError("Problème avec l'affichage de la vie");
                    }
                    break;
            }
        }

        private void FormePalette()
        {
            if (_input._leftSwitch || _input._rightSwitch)
            {
                if (_forme._switchForm == Scr_FormeHandler.Forme.Humain)
                {
                    _formePalette.LeanRotateZ(0, switchDuration);

                    _formeHumain.LeanScaleX(largeSize, switchDuration);
                    _formeHumain.LeanScaleY(largeSize, switchDuration);

                    _formeAgile.LeanScaleX(littleSize, switchDuration);
                    _formeAgile.LeanScaleY(littleSize, switchDuration);

                    _formeHeavy.LeanScaleX(littleSize, switchDuration);
                    _formeHeavy.LeanScaleY(littleSize, switchDuration);

                }
                else
                if (_forme._switchForm == Scr_FormeHandler.Forme.Agile)
                {
                    _formePalette.LeanRotateZ(120, switchDuration);

                    _formeHumain.LeanScaleX(littleSize, switchDuration);
                    _formeHumain.LeanScaleY(littleSize, switchDuration);

                    _formeAgile.LeanScaleX(largeSize, switchDuration);
                    _formeAgile.LeanScaleY(largeSize, switchDuration);

                    _formeHeavy.LeanScaleX(littleSize, switchDuration);
                    _formeHeavy.LeanScaleY(littleSize, switchDuration);

                }
                else
                if (_forme._switchForm == Scr_FormeHandler.Forme.Heavy)
                {
                    _formePalette.LeanRotateZ(-120, switchDuration);

                    _formeHumain.LeanScaleX(littleSize, switchDuration);
                    _formeHumain.LeanScaleY(littleSize, switchDuration);

                    _formeAgile.LeanScaleX(littleSize, switchDuration);
                    _formeAgile.LeanScaleY(littleSize, switchDuration);

                    _formeHeavy.LeanScaleX(largeSize, switchDuration);
                    _formeHeavy.LeanScaleY(largeSize, switchDuration);

                }
            }
        }
    }
}