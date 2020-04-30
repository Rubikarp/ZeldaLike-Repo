using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_ActivatingSecondDoorE2 : MonoBehaviour
    {
        public GameObject _lockedDoor = null;
        public GameObject _openedDoor = null;
        public Scr_LD_Interrupteur[] _interruptors = null;

        private void Start()
        {

        }

        private void Update()
        {
            if(_interruptors[0]._isActive && _interruptors[1]._isActive && _interruptors[2]._isActive)
            {
                _openedDoor.SetActive(true);
                _lockedDoor.SetActive(false);
            }
            else
            {
                _openedDoor.SetActive(false);
                _lockedDoor.SetActive(true);
            }
        }
    }
}
