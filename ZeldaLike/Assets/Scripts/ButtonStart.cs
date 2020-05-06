using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    public class ButtonStart : MonoBehaviour
    {
        private InputManager _input;
        public UnityEvent onInteraction;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

            if (onInteraction != null)
            {
                onInteraction.Invoke();
            }
        }

    }
}