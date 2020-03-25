using UnityEngine;

namespace Ennemis
{
    public class Scr_DummyLifeSystem : MonoBehaviour
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


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Knife"))
            {
                GetMarked();
            }
        }

        public void GetMarked()
        {
            _isMarked = true;
            _logoMarked.SetActive(true);
        }
    }
}
