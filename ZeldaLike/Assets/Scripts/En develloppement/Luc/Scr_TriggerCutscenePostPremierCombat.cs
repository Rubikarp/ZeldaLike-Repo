using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Management;
using Narration;

namespace Narration
{
    public class Scr_TriggerCutscenePostPremierCombat : MonoBehaviour
    {
        public PlayableDirector _playableDirector;
        public float _firstCutsceneDuration;
        public bool _firstCutsceneActive;

        public List<GameObject> firstCutsceneCharacters;

        public GameObject _afterFonduEnNoir;

        // Start is called before the first frame update
        void Start()
        {
            _firstCutsceneActive = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.J) && _firstCutsceneActive == false)
            {
                FindObjectOfType<InputManager>().DesactivateControl();

                for (int i = 0; i < firstCutsceneCharacters.Count; i++)
                {
                    firstCutsceneCharacters[i].SetActive(true);
                }

                _playableDirector.Play();
                _firstCutsceneActive = true;
            }

            if (_firstCutsceneDuration > 0 && _firstCutsceneActive == true)
            {
                _firstCutsceneDuration -= Time.deltaTime;
            }
            else if (_firstCutsceneDuration <= 0 && _firstCutsceneActive == true)
            {
                for (int i = 0; i < firstCutsceneCharacters.Count; i++)
                {
                    Destroy(firstCutsceneCharacters[i]);
                }
                _afterFonduEnNoir.SetActive(true);

                //Là faut mettre un fondu en noir;
            }
        }
    }
}
