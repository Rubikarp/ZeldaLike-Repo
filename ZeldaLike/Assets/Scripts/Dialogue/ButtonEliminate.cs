using UnityEngine;
using UnityEngine.Events;

namespace Management
{
    public class ButtonEliminate : MonoBehaviour
    {
        public Transform container;
        [Space(10)]
        public UnityEvent onInteraction;

        public void Update()
        {
            if (container.childCount == 0)
            {
                onInteraction.Invoke();
                Destroy(this);
            }
        }


    }
}