using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BackGroundTravelling : MonoBehaviour
    {
        [Header("Travelling")]
        public LeanTweenType easeType;
        public AnimationCurve curve;
        [SerializeField] private float finalHeight = 0f;
        [SerializeField] private float travelingTime = 5f;

        [Header("Event")]
        public UnityEvent uEvent = null ;

        private void Start()
        {
            if(easeType == LeanTweenType.animationCurve)
            {
                LeanTween.moveY(gameObject, finalHeight, travelingTime).setLoopOnce().setEase(curve);
            }
            else
            {
                LeanTween.moveY(gameObject, finalHeight, travelingTime).setLoopOnce().setEase(easeType);
            }

            Invoke("Evenement", travelingTime * 0.66f);
        }

        public void Evenement() 
        {
            uEvent?.Invoke();
        } 
    }
}