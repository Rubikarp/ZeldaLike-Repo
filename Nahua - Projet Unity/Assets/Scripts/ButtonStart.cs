using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    public class ButtonStart : MonoBehaviour
    {
        public UnityEvent onStart;

        private void Start()
        {
            onStart?.Invoke();
        }
    }
}