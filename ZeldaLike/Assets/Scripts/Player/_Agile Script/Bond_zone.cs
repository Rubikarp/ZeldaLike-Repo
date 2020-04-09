using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Bond_zone : MonoBehaviour
    {
        public List<GameObject> _detectedEnnemisList = new List<GameObject>();
        public Transform _player = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Ennemis/HurtBox"))
            {
                _detectedEnnemisList.Add(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Ennemis/HurtBox"))
            {
                _detectedEnnemisList.Remove(collision.gameObject);
            }
        }



        public GameObject NearestEnnemis()
        {
            GameObject nearestEnnemis = null;
            float nearestDist = 1000;

            foreach (GameObject ennemis in _detectedEnnemisList)
            {
                float testingDist = Vector2.Distance(_player.position, ennemis.transform.position);

                if (testingDist < nearestDist)
                {
                    nearestEnnemis = ennemis;
                    nearestDist = testingDist;
                }
            }

            return nearestEnnemis;
        }
    }

}