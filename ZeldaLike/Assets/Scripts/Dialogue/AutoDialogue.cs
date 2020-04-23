using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Dialogue
{
    public class AutoDialogue : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;

        [Header("Component")]
        public Conversation Intro = null;
        public PlayableDirector playableDirector;
        public TimelineAsset timeline;

        void OnEnable()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            dialogManag.BeginCoversation(Intro);
            dialogManag.isAuto = true;
        }

        private void Update()
        {
            if (dialogManag.haveEnd)
            {
                timeline.GetOutputTracks();
                playableDirector.Stop();
            }
        }
    }
}