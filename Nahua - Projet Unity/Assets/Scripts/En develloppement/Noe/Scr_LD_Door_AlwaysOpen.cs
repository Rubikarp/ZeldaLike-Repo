using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_Door_AlwaysOpen : MonoBehaviour
    {
        Transform[] allChildren;

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
                foreach (Transform child in transform)
                {
                    child.gameObject.GetComponent<Collider2D>().enabled = false;
                    child.gameObject.GetComponent<Animator>().SetTrigger("Ouverture");
                }
            }
        }
    }
}
