using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_Timer_ActiveState : MonoBehaviour
    {

        Transform[] allChildren;
        public float timer;

        // Start is called before the first frame update
        void Start()
        {
            allChildren = GetComponentsInChildren<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Scr_LD_ActiveState>()._isActive == true)
            {
                StartCoroutine(Countdown());
            }
        }

        private IEnumerator Countdown()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(timer);

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
