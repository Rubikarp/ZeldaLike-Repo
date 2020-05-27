using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BackGroundTravelling : MonoBehaviour
    {
        [Header("Travelling")]
        public AnimationCurve curve;
        [SerializeField] private GameObject BG = null;

        [SerializeField] private float finalHeight = 0f;
        [SerializeField] private float travelingTime = 5f;

        [Header("Event")]
        public UnityEvent uEvent = null ;

        private void Start()
        {
            Time.timeScale = 1;
            LeanTween.moveY(BG, finalHeight, travelingTime).setLoopOnce().setEase(curve);

            Invoke("Evenement", travelingTime * 0.66f);
        }

        public void Evenement() 
        {
            uEvent?.Invoke();
        } 
    }
}