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
        public float _soundDelay;
        public bool _soundPlayed;

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
                StartCoroutine(GetMarked());
            }
        }

        public IEnumerator GetMarked()
        {
            _isMarked = true;
            _logoMarked.SetActive(true);

            if (_soundPlayed == false)
            {
                yield return new WaitForSeconds(_soundDelay);

                sound.PlaySound("Activation Dummy");
                _soundPlayed = true;
            }
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
