using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_InterrupteurActive : MonoBehaviour
    {

        public bool pressed;

        // Start is called before the first frame update
        void Start()
        {
            pressed = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Scr_LD_Interrupteur>()._isActive == true)
            {
                pressed = true;
            }   
        }

        private void OnDisable()
        {
            pressed = false;
        }
    }
}
