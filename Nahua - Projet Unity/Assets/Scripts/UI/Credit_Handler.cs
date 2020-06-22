using UnityEngine;

namespace Game
{
    public class Credit_Handler : MonoBehaviour
    {
        [Header("Travelling")]
        [SerializeField] private GameObject CreditList = null;
        [SerializeField] private float startHeight = 0f;
        [SerializeField] private float finalHeight = 4000f;
        [SerializeField] private float travelingTime = 10f;

        private void OnEnable()
        {
            Time.timeScale = 1;
            CreditList.transform.position = new Vector3(960, startHeight, 0);
            LeanTween.moveY(CreditList, finalHeight, travelingTime);

        }
    }
}