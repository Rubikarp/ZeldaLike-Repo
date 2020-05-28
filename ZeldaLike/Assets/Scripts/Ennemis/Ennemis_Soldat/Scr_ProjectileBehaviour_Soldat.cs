using UnityEngine;

namespace Ennemis
{
    public class Scr_ProjectileBehaviour_Soldat : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private Scr_EnnemisBehaviour_Soldat _behavSoldat = null;
        public Transform _projectile = null;
        public Rigidbody2D _rb2d = null;

        [Header("Statistiques")]
        public Vector2 _BulletDir = Vector2.zero;
        public float _mySpeed = 10f;
        public float _timer = 1f;
        private float _rotZ;

        private void Start()
        {
            _projectile = this.transform;
            _rb2d = this.GetComponent<Rigidbody2D>();
            _behavSoldat = this.gameObject.GetComponentInParent<Scr_EnnemisBehaviour_Soldat>();
            _BulletDir = _behavSoldat._targetDirection.normalized;
            _rotZ = Mathf.Atan2(_BulletDir.y, _BulletDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + 90);

            Destroy(this.gameObject, _timer);
        }

        private void Update()
        {
            _rb2d.position += _BulletDir * _mySpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player/HurtBox"))
            {
                Destroy(gameObject);
            }
            else
            {
                //Debug.Log(collision.gameObject.name);
            }
        }

    }
}
