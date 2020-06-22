using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Dialogue
{
    public class BeginDialogue : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;

        [Header("Component")]
        public Conversation Intro = null;
        public PlayableDirector playableDirector;
        public TimelineAsset timeline;
        public bool endwithdialog = true;

        void OnEnable()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            dialogManag.BeginCoversation(Intro);
            dialogManag.isAuto = false;
        }

        private void Update()
        {
            if (dialogManag.haveEnd && endwithdialog)
            {
                timeline.GetOutputTracks();
                playableDirector.Stop();
            }
        }
    }
}