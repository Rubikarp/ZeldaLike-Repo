using UnityEngine;

namespace Dialogue
{
    public class Cin_0_Intro : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;

        [Header("Component")]
        public Conversation Intro = null;

        void Start()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            dialogManag.BeginCoversation(Intro);
        }

        private void Update()
        {
            if (dialogManag.haveEnd)
            {

            }

            Destroy(gameObject);
        }

    }
}