using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class Scr_BossP2_CoupMassifTimer : MonoBehaviour
    {
        public float _hitDelay;
        public float _dmgLifeTime;
        public GameObject _hitbox;
        public GameObject _rectangleJaune;
        public GameObject _rectangleBlanc1;
        public GameObject _rectangleBlanc2;
        public GameObject _joliSprite;

        // Start is called before the first frame update
        void Start()
        {
            GetComponentInParent<Scr_BossP2_CoupMAssifPos>()._attackSet = true;
            StartCoroutine(BoomBoom());
        }

        private IEnumerator BoomBoom()
        {
            yield return new WaitForSeconds(_hitDelay);
            _hitbox.SetActive(true);

            _joliSprite.SetActive(false);
            _rectangleJaune.SetActive(true);
            yield return new WaitForSeconds(0.10f);
            _rectangleJaune.SetActive(false);
            _rectangleBlanc1.SetActive(true);
            yield return new WaitForSeconds(0.10f);
            _rectangleBlanc1.SetActive(false);
            _rectangleJaune.SetActive(true);
            yield return new WaitForSeconds(0.10f);
            _hitbox.SetActive(false);
            _rectangleJaune.SetActive(false);
            _rectangleBlanc2.SetActive(true);
            yield return new WaitForSeconds(0.10f);
            Destroy(gameObject);
        }
    }
}
