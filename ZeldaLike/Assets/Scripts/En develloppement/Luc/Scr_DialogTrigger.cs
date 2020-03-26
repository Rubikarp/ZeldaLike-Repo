using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Management;

namespace Game
{
    public class Scr_DialogTrigger : MonoBehaviour
    {
        public Dialog dialogue;

        public Transform talkPos;
        public float talkRange;
        public LayerMask whatIsThePlayer;

        private bool isTalking;

        Animator textBox;

        public GameObject toucheA;

        void Start()
        {
            isTalking = false;
            toucheA.SetActive(false);
            textBox = GameObject.Find("Boîte_de_Dialogues").GetComponent<Animator>();
        }

        void Update()
        {
            if (isTalking == false)
            {
                Collider2D[] playerToTalk = Physics2D.OverlapCircleAll(talkPos.position, talkRange, whatIsThePlayer);

                if (playerToTalk.Length > 0)
                {
                    toucheA.SetActive(true);
                }
                else
                {
                    toucheA.SetActive(false);
                }

                if (playerToTalk.Length > 0 && Input.GetButtonDown("Interract"))
                {
                    TriggerDialogue();
                    isTalking = true;
                    toucheA.SetActive(false);
                }
            }
            else if (isTalking == true && Input.GetButtonDown("Interract"))
            {
                ContinueDialogue();
            }

            if (!textBox.GetBool("IsOpen"))
            {
                isTalking = false;
            }

        }

        public void TriggerDialogue()
        {
            FindObjectOfType<Scr_DialogManager>().StartDialogue(dialogue);
        }

        public void ContinueDialogue()
        {
            FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, talkRange);
        }
    }
}

