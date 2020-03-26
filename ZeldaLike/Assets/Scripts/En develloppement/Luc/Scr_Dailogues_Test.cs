using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{

    public class Scr_Dailogues_Test : MonoBehaviour
    {
        private InputManager _input;
        public Dialog _dialogue;
        private bool _isTalking;
        private int _talkCount;

        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            _isTalking = false;
            _talkCount = 0;
        }

        void Update()
        {
            if (_input._attack && _isTalking == false)
            {
                TriggerDialogue();
                _talkCount += 1;
            }
            else if (_input._attack && _isTalking == true)
            {
                ContinueDialogue();
                _talkCount += 1;

                if (_talkCount == _dialogue.sentences.Length)
                {
                    _isTalking = false;
                    _talkCount = 0;
                }
            }
        }

        public void TriggerDialogue()
        {
            FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogue);
            _isTalking = true;
        }

        public void ContinueDialogue()
        {
            FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
        }
    }
}
