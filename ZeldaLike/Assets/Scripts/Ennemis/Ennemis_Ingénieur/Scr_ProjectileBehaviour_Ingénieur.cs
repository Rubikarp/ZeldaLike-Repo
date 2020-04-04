using UnityEngine;

namespace Ennemis
{
    public class Scr_ProjectileBehaviour_Ingénieur : MonoBehaviour
    {
        [Header("Data")]
        public Transform _projectile = null;
        public Rigidbody2D _rb2d = null;

        [Header("Statistiques")]
        public Vector2 _BulletDir = Vector2.zero;
        public float _mySpeed = 10f;
        public float _timer = 1f;

        private void Start()
        {
            _projectile = this.transform;
            _rb2d = this.GetComponent<Rigidbody2D>();
            Destroy(this.gameObject, _timer);
        }

        private void Update()
        {
            _rb2d.position += _BulletDir * _mySpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log(collision.gameObject.name);
                Destroy(gameObject);
            }
        }

        public void BulletSetDir(Vector2 direction)
        {
            _BulletDir = direction;
        }

    }
}
