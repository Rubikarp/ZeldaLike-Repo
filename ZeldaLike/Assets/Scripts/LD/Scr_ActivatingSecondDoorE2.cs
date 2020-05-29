using UnityEngine;

namespace Game
{
    public class Scr_ActivatingSecondDoorE2 : MonoBehaviour
    {
        public GameObject _lockedDoor = null;
        [SerializeField] private Animator _lockedDoorAnim = null;
        [SerializeField] private Collider2D _lockedDoorCollid = null;

        public Scr_LD_ActiveState[] _interruptors = null;
        private bool _open = false;

        private void Update()
        {
            if(_interruptors[0]._isActive && _interruptors[1]._isActive && _interruptors[2]._isActive) 
            {
                _lockedDoorCollid.enabled = false;
                
                if (!_open) 
                {
                    _lockedDoorAnim.SetTrigger("Ouverture");
                    _open = false;
                }
            }
            else 
            {
                _lockedDoorCollid.enabled = true;
                
                if (_open) 
                {
                    _lockedDoorAnim.SetTrigger("Fermeture");
                    _open = true;
                }
            }
        }
    }
}
