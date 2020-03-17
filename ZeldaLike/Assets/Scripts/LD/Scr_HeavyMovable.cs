using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Scr_HeavyMovable : MonoBehaviour
    {
        [SerializeField] private GameObject _HeavyForm = null;
        [SerializeField] private Rigidbody2D _myBody = null;

        void Start()
        {
            _myBody = this.gameObject.GetComponent<Rigidbody2D>();
            _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        void Update()
        {
            if (_HeavyForm.activeSelf)
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            
        }
    }
}