using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Management;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private InputManager _input = null;

        [Header("Dialogue")]
        public Conversation _actualConversation;
        private Echange _actualEchange;
        private int _EchangeCounter = 0;
        private int _SentanceCounter = 0;
        private bool _isTalking = false;

        [Header("UI Elements")]
        [SerializeField] private GameObject _textBox;
        [SerializeField] private Animator _textBoxAnim;
        [SerializeField] private Text _nom;
        [SerializeField] private Text _dialogues;

        [Header("Variable")]
        [SerializeField] [Range(0, 0.05f)]
        private float DelayBetweenLetters = 0.03f;


        void Start()
        {
            _textBoxAnim = _textBox.GetComponent<Animator>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

        }


        public void StartDialogue(Conversation conversation)
        {
            _actualConversation = conversation;

            _EchangeCounter = 0;

            _actualEchange = _actualConversation.echangeList[_EchangeCounter];

            _SentanceCounter = 0;

            _textBoxAnim.SetBool("speaking", true);
        }

        private IEnumerator TypeSentence(string name, string phrase)
        {
            _dialogues.text = "";
            _nom.text = "";

            _nom.text = name;

            foreach (char letter in phrase.ToCharArray())
            {
                _dialogues.text += letter;

                yield return new WaitForSeconds(DelayBetweenLetters);
            }

        }

        private void DisplayNextSentence()
        {
            if (_SentanceCounter > _actualEchange._echange.Length)
            {
                NextEchange();
                return;
            }

            _SentanceCounter++;

            float tempsParLettre = 0.2f;
            float wait = _actualEchange._echange[_SentanceCounter].Length * tempsParLettre;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(_SentanceCounter % 2 == 0? _actualEchange.interlocuteur_B : _actualEchange.interlocuteur_A, _actualEchange._echange[_SentanceCounter]));
        }

        private void NextEchange()
        {
            if (_EchangeCounter > _actualConversation.echangeList.Length)
            {
                EndDialogue();
                return;
            }

            StopAllCoroutines();

        }

        public void EndDialogue()
        {
            _textBoxAnim.SetBool("speaking", false);
        }
    }
}