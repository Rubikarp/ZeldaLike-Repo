using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_Ennemis_D_E2_P1 : MonoBehaviour
    {
        public GameObject _ingenieur;
        public GameObject _harceleur;
        public GameObject _expRat1;
        public GameObject _expRat2;


        public bool appearing;

        public Vector2 ingenieurPosition;
        public Vector2 harceleurPosition;
        public Vector2 expRat1Position;
        public Vector2 expRat2Position;

        private float timerEnnemy;

        // Start is called before the first frame update
        void Start()
        {
            appearing = false;

            timerEnnemy = GetComponent<Scr_LD_Timer_ActiveState>().timer;
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Scr_LD_ActiveState>()._isActive == true && appearing == false)
            {
                StartCoroutine(EnnemisActivation());
            }
        }

        private IEnumerator EnnemisActivation()
        {
            appearing = true;
            
            GameObject ingenieur = (GameObject)Instantiate(_ingenieur, ingenieurPosition, Quaternion.identity);
            GameObject harceleur = (GameObject)Instantiate(_harceleur, harceleurPosition, Quaternion.identity);
            Debug.Log("truc");
            GameObject expRat1 = (GameObject)Instantiate(_expRat1, expRat1Position, Quaternion.identity);
            GameObject expRat2 = (GameObject)Instantiate(_expRat2, expRat2Position, Quaternion.identity);

            yield return new WaitForSecondsRealtime(timerEnnemy);

            Destroy(ingenieur);
            Destroy(harceleur);
            Destroy(expRat1);
            Destroy(expRat2);

            appearing = false;

        }
    }
}

