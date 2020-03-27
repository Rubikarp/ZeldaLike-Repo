using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Narration;

namespace Narration
{
    public class Scr_DialoguesCutsceneApresFondu : MonoBehaviour
    {
        public Dialog _dialogues;

        public List<float> _dialogDuration;
        public List<bool> _dialogActive;

        // Start is called before the first frame update
        void Start()
        {
            FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogues);
            for (int i = 0; i < _dialogActive.Count; i++)
            {
                _dialogActive[i] = false;
            }
            _dialogActive[0] = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_dialogActive[0] == true && _dialogDuration[0] > 0)
            {
                _dialogDuration[0] -= Time.deltaTime;
            }
            else if (_dialogActive[0] == true && _dialogDuration[0] <= 0)
            {
                _dialogues.name = "Nahua";
                FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogues);
                FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
                _dialogActive[0] = false;
                _dialogActive[1] = true;
            }

            if (_dialogActive[1] == true && _dialogDuration[1] > 0)
            {
                _dialogDuration[1] -= Time.deltaTime;
            }
            else if (_dialogActive[1] == true && _dialogDuration[1] <= 0)
            {
                _dialogues.name = "L'Ancien";
                FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogues);
                FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
                FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
                _dialogActive[1] = false;
                _dialogActive[2] = true;
            }

            if (_dialogActive[2] == true && _dialogDuration[2] > 0)
            {
                _dialogDuration[2] -= Time.deltaTime;
            }
            else if (_dialogActive[2] == true && _dialogDuration[2] <= 0)
            {
                FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
                _dialogActive[2] = false;
            }
        }
    }
}

