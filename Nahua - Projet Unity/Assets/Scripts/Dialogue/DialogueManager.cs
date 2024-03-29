﻿using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Management;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue")]
        public Conversation _actualConversation;
        private Echange _actualEchange;
        private int _EchangeCounter = 0;
        private int _SentanceCounter = 0;

        [Header("UI Elements")]
        [SerializeField] private RectTransform _textBox = null;
        [SerializeField] private Text _nom = null;
        [SerializeField] private Text _dialogues = null;
        [SerializeField] private Image _portrait = null;

        [Header("Variable")]
        [SerializeField] [Range(0, 0.05f)]
        private float DelayBetweenLetters = 1f;
        private float tempsLectParLettre = 0.05f;
        public bool haveEnd = false;
        public bool isAuto = false;

        public void BeginCoversation(Conversation conversation)
        {
            _actualConversation = conversation;

            _EchangeCounter = 0;

            _actualEchange = _actualConversation.echangeList[_EchangeCounter];

            _SentanceCounter = 0;

            haveEnd = false;

            StartCoroutine(StartDialogue());
        }

        private IEnumerator StartDialogue()
        {
            float enterTime = 0.3f;

            LeanTween.moveY(_textBox, 100, enterTime);

            yield return new WaitForSeconds(enterTime);

            NextEchange();
        }

        private IEnumerator TypeSentence(string name, string phrase, Sprite portrait)
        {
            _dialogues.text = "";
            _nom.text = "";
            _portrait.sprite = null;

            _nom.text = name;
            _portrait.sprite = portrait;

            foreach (char letter in phrase.ToCharArray())
            {
                _dialogues.text += letter;
                
                yield return new WaitForSeconds(DelayBetweenLetters);
                
            }

            float wait = phrase.Length * tempsLectParLettre;
            
            yield return new WaitForSeconds(wait);


            yield return new WaitForSeconds(0.1f);

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_SentanceCounter >= _actualEchange._echange.Length)
            {
                if (isAuto)
                {
                    NextEchange();
                }
                else
                {
                    EndDialogue();
                }
                return;
            }
            if (haveEnd)
            {
                StartDialogue();
                haveEnd = false;
            }

            StopAllCoroutines();
            StartCoroutine(TypeSentence(
                _SentanceCounter % 2 == 0? _actualEchange.interlocuteur_A : _actualEchange.interlocuteur_B
                , _actualEchange._echange[_SentanceCounter]
                , _SentanceCounter % 2 == 0 ? _actualEchange.image_A : _actualEchange.image_B
                ));
            
            _SentanceCounter++;
        }

        private void NextEchange()
        {
            if (_EchangeCounter >= _actualConversation.echangeList.Length)
            {
                EndDialogue();
                return;
            }

            StopAllCoroutines();

            _actualEchange = _actualConversation.echangeList[_EchangeCounter];
            _SentanceCounter = 0;

            DisplayNextSentence();

            _EchangeCounter++;

        }

        public void EndDialogue()
        {
            _nom.text = "";
            _dialogues.text = "";
            LeanTween.moveY(_textBox, -100, 0.3f);
            haveEnd = true;
        }
    }
}