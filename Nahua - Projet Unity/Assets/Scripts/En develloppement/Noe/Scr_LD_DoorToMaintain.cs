using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_DoorToMaintain : MonoBehaviour
    {

        Transform[] allChildren;
        private bool _open;

        // Start is called before the first frame update
        void Start()
        {
            allChildren = GetComponentsInChildren<Transform>();
            _open = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Scr_LD_ActiveState>()._isActive == true && _open == false)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.GetComponent<Collider2D>().enabled = false;
                    child.gameObject.GetComponent<Animator>().SetTrigger("Ouverture");

                }

                _open = true;
            }

            if (GetComponent<Scr_LD_ActiveState>()._isActive == false && _open == true)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.GetComponent<Collider2D>().enabled = true;
                    child.gameObject.GetComponent<Animator>().SetTrigger("Fermeture");
                }

                _open = false;
            }
        }
    }
}
