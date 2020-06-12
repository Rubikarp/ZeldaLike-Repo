using UnityEngine;

namespace Ennemis
{
    public class DummyDonjon_Anim : MonoBehaviour
    {
        Animator dummyAnim;

        private bool _isMarked;
        private bool _headFallen;

        // Start is called before the first frame update
        void Start()
        {
            dummyAnim = GetComponent<Animator>();

            _isMarked = GetComponentInChildren<Scr_DummyLifeSystem>()._isMarked;

            _headFallen = false;
        }

        // Update is called once per frame
        void Update()
        {
            _isMarked = GetComponentInChildren<Scr_DummyLifeSystem>()._isMarked;

            if (_isMarked == true && _headFallen == false)
            {
                dummyAnim.SetTrigger("HeadOff");
                _headFallen = true;
            }
        }
    }
}
