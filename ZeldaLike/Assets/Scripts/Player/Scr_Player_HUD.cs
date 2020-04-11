using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Management;

namespace Game
{
    public class Scr_Player_HUD : MonoBehaviour
    {
        public enum Forme { Humain, Agile, Lourd }

        [SerializeField] InputManager _input = default;
        [SerializeField] Scr_FormeManager _forme = default;
        [SerializeField] Scr_PlayerLifeSystem _lifeSystem = default;

        [SerializeField] public Image[] coeur = default;
        [SerializeField] public RectTransform _formePalette = default;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        void Update()
        {
            lifeBarUpdate(_lifeSystem._life);
        }

        private void LateUpdate()
        {
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
            if (_forme._humanSprite.activeInHierarchy)
            {
                _formePalette.LeanRotateZ( 0, 0.3f);
            }
            if (_forme._agileSprite.activeInHierarchy)
            {
                _formePalette.LeanRotateZ(120, 0.3f);
            }
            if (_forme._heavySprite.activeInHierarchy)
            {
                _formePalette.LeanRotateZ(-120, 0.3f);
            }
        }
    }
}