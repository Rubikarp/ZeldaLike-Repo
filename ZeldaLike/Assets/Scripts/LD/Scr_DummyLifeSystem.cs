using System.Collections;
using UnityEngine;
using Game;

namespace Ennemis
{
    public class Scr_DummyLifeSystem : MonoBehaviour, Int_EnnemisLifeSystem
    {
        [Header("Marked")]
        public GameObject _logoMarked = null;
        public bool IsBleeding
        {
            get { return _isMarked; }
            set { _isMarked = value; }
        }
        public bool _isMarked = false;

        [Header("Data")]
        public GameObject Dummy = null;
        public Rigidbody2D body = null;

        private SoundManager sound; //Le son

        //private Animator dummyAnimator;

        /*void Start ()
        {
            dummyAnimator = GetComponent<Animator>();
        }*/
        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Knife"))
            {
                GetMarked();
                sound.PlaySound("Activation Dummy");
            }
        }

        public void GetMarked()
        {
            _isMarked = true;
            _logoMarked.SetActive(true);
        }

        /*void Update ()
        {
            if (_isMarked == true)
            {
                dummyAnimator.SetBool("Dummy", _isMarked);dummyAnimator.SetBool("Dummy", _isMarked);
            }
        }*/

        public IEnumerator TakingDamage(int damage, Rigidbody2D body, Vector2 knockBackDirection, float knockbackSpeed, float stunDuration)
        {
            return null;
        }

    }
}
