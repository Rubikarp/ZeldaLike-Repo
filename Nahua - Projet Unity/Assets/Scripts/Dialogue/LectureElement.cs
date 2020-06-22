using UnityEngine;

namespace Dialogue
{
    public class LectureElement : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;
        public GameObject ActivationButton;

        private void Start()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ActivationButton.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ActivationButton.SetActive(false);
            }
        }

        public void Lecture(Conversation Text)
        {
            {
                dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
                dialogManag.BeginCoversation(Text);
                dialogManag.isAuto = true;
            }
        }

    }
}