using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class ButtonTriggerEnter : MonoBehaviour
    {
        public UnityEvent onInteraction;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                onInteraction?.Invoke();
                onInteraction.RemoveAllListeners();
            }
        }        
    }
}