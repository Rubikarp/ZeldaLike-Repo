using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_ActivatingSecondDoorE2 : MonoBehaviour
    {
        public GameObject _lockedDoor = null;
        //public GameObject _openedDoor = null;
        public Scr_LD_ActiveState[] _interruptors = null;
        private bool _open;

        private void Start()
        {
            _open = false;
        }

        private void Update()
        {
            if(_interruptors[0]._isActive && _interruptors[1]._isActive && _interruptors[2]._isActive && _open == false)
            {
                _lockedDoor.GetComponent<Collider2D>().enabled = false;
                _lockedDoor.GetComponent<Animator>().SetTrigger("Ouverture");
                _open = true;
            }
            else
            {
                _lockedDoor.GetComponent<Collider2D>().enabled = true;
                if (_open == true)
                {
                    _lockedDoor.GetComponent<Animator>().SetTrigger("Fermeture");
                }
                _open = false;
            }
        }
    }
}
