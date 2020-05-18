using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_Geyser : MonoBehaviour
    {
        private float _repulseDelay;
        public float _repulseDelayOrigin;
        public float _repulseSpeed;
        public float _repulseDuration;
        public float _repulseRange;
        public LayerMask _whatIsPlayer;
        private bool _startRepulse;

        public Rigidbody2D _targetBody;
        public GameObject _water;
        public float _waterDur;
        private List<GameObject> _targetEnnemyBodies;

        private SoundManager sound;

        private void Awake()
        {
            sound = SoundManager.Instance;
        }

        // Start is called before the first frame update
        void Start()
        {
            _startRepulse = false;
            _repulseDelay = _repulseDelayOrigin;
            _water.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            // Sert de délai commun à l'explosion : lorsque le geyser est activé, son explosion affecte tous ceux qui sont dessus, même s'ils sont arrivés après l'activation.
            if (_startRepulse == true)
            {
                if (_repulseDelay > 0)
                {
                    _repulseDelay -= Time.deltaTime;
                }
                else if (_repulseDelay <= 0)
                {
                    _startRepulse = false;
                    _repulseDelay = _repulseDelayOrigin;
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _targetBody = collision.gameObject.transform.parent.parent.GetComponent<Rigidbody2D>();

                StartCoroutine(Repulse(collision.gameObject.transform.parent.parent.position, _targetBody, _repulseDelay, _repulseDuration));

                //On lance le délai si celui-ci n'est pas déjà lancé;
                if (_startRepulse == false)
                {
                    _startRepulse = true;
                }

                Debug.Log("Target");
            }
            else if (collision.gameObject.CompareTag("Ennemis"))
            {

                StartCoroutine(RepulseEnnemies(collision.gameObject.transform.parent.position, collision.gameObject.GetComponentInParent<Rigidbody2D>(), _repulseDelay, _repulseDuration));

                //On lance le délai si celui-ci n'est pas déjà lancé;
                if (_startRepulse == false)
                {
                    _startRepulse = true;
                }

                Debug.Log("Target Ennemy");
            }
        }

        IEnumerator Repulse(Vector3 position, Rigidbody2D body, float delay, float duration)
        {
            yield return new WaitForSeconds(delay);
            _water.SetActive(true);
            sound.PlaySound("GeyserGushing");

            Collider2D[] playerToRepulse = Physics2D.OverlapCircleAll(transform.position, _repulseRange);

            for (int i = 0; i < playerToRepulse.Length; i++)
            {
                if (playerToRepulse[i].gameObject.transform.parent.parent.CompareTag("Player"))
                {
                    Vector3 repulseDirection = position - playerToRepulse[i].gameObject.transform.parent.parent.position;

                    while (duration > 0)
                    {
                        body.velocity = repulseDirection * _repulseSpeed;
                        duration -= Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }

                    body.velocity = Vector2.zero;
                }
            }

            yield return new WaitForSeconds(_waterDur);
            _water.SetActive(false);

        }

        IEnumerator RepulseEnnemies(Vector3 position, Rigidbody2D targetObject, float delay, float duration)
        {
            yield return new WaitForSeconds(delay);
            _water.SetActive(true);
            sound.PlaySound("GeyserGushing");

            Collider2D[] ennemyToRepulse = Physics2D.OverlapCircleAll(transform.position, _repulseRange);

            for (int i = 0; i < ennemyToRepulse.Length; i++)
            {
                if (ennemyToRepulse[i].gameObject == targetObject.gameObject)
                {
                    Vector3 repulseDirection = position - ennemyToRepulse[i].gameObject.transform.position;

                    while (duration > 0)
                    {
                        targetObject.velocity = repulseDirection * _repulseSpeed;
                        duration -= Time.deltaTime;
                        yield return new WaitForEndOfFrame();
                    }

                    targetObject.velocity = Vector2.zero;
                }
            }

            yield return new WaitForSeconds(_waterDur);
            _water.SetActive(false);

        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _repulseRange);
        }
    }
}

