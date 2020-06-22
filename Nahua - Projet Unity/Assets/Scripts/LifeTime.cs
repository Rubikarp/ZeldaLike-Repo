using UnityEngine;

namespace Game
{
    public class LifeTime : MonoBehaviour
    {
        [SerializeField] private float lifetime = 1f;
        
        void Start()
        {
            Destroy(gameObject, lifetime);
        }   
    }
}