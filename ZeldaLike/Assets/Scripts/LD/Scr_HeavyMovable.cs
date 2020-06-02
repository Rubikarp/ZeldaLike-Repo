using UnityEngine;
using Management;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Scr_HeavyMovable : MonoBehaviour
    {
        [SerializeField] private Scr_FormeHandler _forme = null;
        private Rigidbody2D _myBody = null;
        private SoundManager sound; //Le son
        private Transform player = null;

        [SerializeField] private bool movable = false;
        public bool _isBig = true;

        void Start()
        {
            _forme = GameObject.Find("Avatar").GetComponent<Scr_FormeHandler>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Awake()
        {
            sound = SoundManager.Instance;
            _myBody = this.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            //Pas de forme lourd pas de bloc qui bouge
            if(_forme._switchForm == Scr_FormeHandler.Forme.Heavy && PlayerNearBlock(player, 4f))
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                movable = true;
            }
            else
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeAll;
                movable = false;
            }

            if(Mathf.Abs(_myBody.velocity.x) > Mathf.Abs(_myBody.velocity.y))
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            }
            else if (Mathf.Abs(_myBody.velocity.y) > Mathf.Abs(_myBody.velocity.x))
            {
                _myBody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            }
        }

        protected bool PlayerNearBlock(Transform player, float testingDist)
        {
            var distance = Vector2.Distance(this.transform.position, player.position);
            if (distance <= testingDist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}