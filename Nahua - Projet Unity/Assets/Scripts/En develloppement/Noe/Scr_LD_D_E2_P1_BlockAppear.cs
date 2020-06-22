using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_D_E2_P1_BlockAppear : MonoBehaviour
    {
        Transform[] allChildren;

        public Scr_LD_InterrupteurActive Button1;
        public Scr_LD_InterrupteurActive Button2;
        public Scr_LD_InterrupteurActive Button3;
        public Scr_LD_InterrupteurActive Button4;

        // Start is called before the first frame update
        void Start()
        {
            allChildren = GetComponentsInChildren<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Button1.pressed == true && Button2.pressed == true && Button3.pressed == true && Button4.pressed == true)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
