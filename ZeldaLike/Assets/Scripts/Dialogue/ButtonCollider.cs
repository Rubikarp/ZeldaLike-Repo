using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    [RequireComponent(typeof(Collider2D))]
    public class ButtonCollider : MonoBehaviour
    {
        private InputManager _input;
        private bool haveInput = false;

        public UnityEvent onInteraction;


        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        public void OnTriggerStay()
        {
            if (_input._interaction & !haveInput)
            {
                if (onInteraction != null)
                {
                    onInteraction.Invoke();
                }

                haveInput = true;
            }
            else
            {
                haveInput = false;
            }
        }
    }
}