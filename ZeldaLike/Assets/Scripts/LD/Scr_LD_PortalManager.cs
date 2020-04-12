using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_PortalManager : MonoBehaviour
    {
        public GameObject _portal1;
        public GameObject _portal2;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            if (GetComponent<Scr_LD_ActiveState>()._isActive == true)
            {
                _portal1.SetActive(true);
                _portal2.SetActive(true);
            }
        }
    }
}

