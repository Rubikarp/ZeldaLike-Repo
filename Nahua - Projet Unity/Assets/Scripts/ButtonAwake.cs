using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    [RequireComponent(typeof(Collider2D))]
    public class ButtonAwake : MonoBehaviour
    {
        private InputManager _input;
        public UnityEvent onInteraction;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        private void OnEnable()
        {
            if (onInteraction != null)
            {
                onInteraction.Invoke();
            }
        }
    }
}