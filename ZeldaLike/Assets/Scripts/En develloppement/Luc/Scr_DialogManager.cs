using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Management
{
    public class Scr_DialogManager : MonoBehaviour
    {
        Text _nameText;
        Text _dialogueText;

        GameObject _name;
        GameObject _dialogues;
        GameObject _textBox;

        Animator _animator;

        private Queue<string> _sentences;

        // Start is called before the first frame update
        void Start()
        {
            _sentences = new Queue<string>();
            _name = GameObject.Find("Nom_Du_PNJ");
            _nameText = _name.GetComponent<Text>();
            _dialogues = GameObject.Find("Paroles");
            _dialogueText = _dialogues.GetComponent<Text>();
            _textBox = GameObject.Find("Boîte_de_Dialogues");
            _animator = _textBox.GetComponent<Animator>();
        }

        public void StartDialogue(Dialog dialogue)
        {

            _animator.SetBool("speaking", true);

            Debug.Log("Début de la conversation avec" + dialogue.name);

            _nameText.text = dialogue.name;

            _sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        void EndDialogue()
        {
            _animator.SetBool("speaking", false);
        }

        IEnumerator TypeSentence(string sentence)
        {
            _dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                _dialogueText.text += letter;
                yield return null;
            }
        }
    }
}

