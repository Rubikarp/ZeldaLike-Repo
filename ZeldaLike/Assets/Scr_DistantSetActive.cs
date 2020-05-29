using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Scr_DistantSetActive : MonoBehaviour
    {
        public UnityEvent onInteraction;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                onInteraction?.Invoke();
                onInteraction.RemoveAllListeners();
            }
        }
    }
}