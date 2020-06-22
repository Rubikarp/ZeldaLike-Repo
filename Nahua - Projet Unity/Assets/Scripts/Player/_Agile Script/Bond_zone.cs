using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Bond_zone : MonoBehaviour
    {
        public List<GameObject> _detectedEnnemisList = new List<GameObject>();
        public Transform _player = null;
        public float _listClear;
        [SerializeField] private float _listClearEffective;


        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Ennemis/HurtBox"))
            {
                if (!_detectedEnnemisList.Contains(collision.gameObject))
                {
                    _detectedEnnemisList.Add(collision.gameObject);
                }
            }
        }

        private void LateUpdate()
        {
            if (_listClearEffective > 0)
            {
                _listClearEffective -= Time.deltaTime;
            }
            else if (_listClearEffective <= 0)
            {
                _detectedEnnemisList.Clear();
                _listClearEffective = _listClear;
            }
        }
    }

}