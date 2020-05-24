using UnityEngine;
using Management;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Scr_HeavyMovable : MonoBehaviour
    {
        [SerializeField] private Scr_FormeHandler _forme = null;
        [SerializeField] private Rigidbody2D _myBody = null;
        private float _delay;
        private bool _movable;
        [HideInInspector] public Vector2 _rockDirection;
        public bool _isBig;
        private SoundManager sound; //Le son

        void Start()
        {
            _myBody = this.gameObject.GetComponent<Rigidbody2D>();
            _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
            _movable = false;
            _delay = 0.25f;

            _forme = GameObject.Find("Avatar").GetComponent<Scr_FormeHandler>();

        }

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        void Update()
        {

            //_myBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (_movable == true)
            {

                _rockDirection = new Vector2(Input.GetAxis("LStickAxisX"), Input.GetAxis("LStickAxisY")).normalized;
                sound.PlaySound("Deplacement Bloc");

                if (Mathf.Abs(_rockDirection.x) > Mathf.Abs(_rockDirection.y))
                {
                    _myBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    _myBody.velocity = new Vector2(0 , 5).normalized;

                }
                else if (Mathf.Abs(_rockDirection.x) <= Mathf.Abs(_rockDirection.y))
                {
                    _myBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    _myBody.velocity = new Vector2(5, 0).normalized;


                }
            }
            else
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            //Pour éviter que le Bloc ne s'arrête pas lorsqu'on ne change pas de forme.
            if(_delay > 0 && _forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                _delay -= Time.deltaTime;
            }
            else if (_delay <= 0 && _forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
                _delay = 0.25f;
            }
 
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.gameObject.transform.parent.parent.CompareTag("Player") && _forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                _movable = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.transform.parent.parent.CompareTag("Player") && _forme._switchForm == Scr_FormeHandler.Forme.Heavy)
            {
                _movable = false;
            }
        }
    }
}