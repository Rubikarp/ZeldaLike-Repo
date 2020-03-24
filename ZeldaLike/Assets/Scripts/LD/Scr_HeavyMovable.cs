using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Scr_HeavyMovable : MonoBehaviour
    {
        [SerializeField] private GameObject _HeavyForm = null;
        [SerializeField] private Rigidbody2D _myBody = null;
        private float _delay;

        void Start()
        {
            _myBody = this.gameObject.GetComponent<Rigidbody2D>();
            _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
            _delay = 0.25f;
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

            //Pour éviter que le Bloc ne s'arrête pas lorsqu'on ne change pas de forme.
            if(_delay > 0 && _HeavyForm.activeSelf)
            {
                _delay -= Time.deltaTime;
            }
            else if (_delay <= 0 && _HeavyForm.activeSelf)
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
                _delay = 0.25f;
            }
 
        }
    }
}