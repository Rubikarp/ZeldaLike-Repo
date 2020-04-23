using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_DoorToMaintain : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Scr_LD_ActiveState>()._isActive == true)
            {
                gameObject.SetActive(false);
            }

            if (GetComponent<Scr_LD_ActiveState>()._isActive == false)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
