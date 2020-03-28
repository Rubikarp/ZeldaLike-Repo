using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Narration;

namespace Narration
{
    public class Scr_Dialogues2eCutscene : MonoBehaviour
    {
        public Dialog _dialogues;
        public float _talkTime;
        private bool _firstLineSet;
        private bool _secondLineSet;
        public float _secondTalkTime;

        // Start is called before the first frame update
        void Start()
        {
            FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogues);
            _firstLineSet = true;
            _secondLineSet = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_firstLineSet == true && _talkTime > 0)
            {
                _talkTime -= Time.deltaTime;
            }
            else if (_firstLineSet == true && _talkTime <= 0)
            {
                FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
                _secondLineSet = true;
                _firstLineSet = false;
            }

            if (_secondLineSet == true && _secondTalkTime > 0)
            {
                _secondTalkTime -= Time.deltaTime;
            }
            else if (_secondLineSet == true && _secondTalkTime <= 0)
            {
                FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
                _secondLineSet = false;
            }
        }
    }
}

