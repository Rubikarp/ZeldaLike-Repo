using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class DummyDonjon_Anim : MonoBehaviour
    {
        Animator dummyAnim;

        private bool _isMarked;

        // Start is called before the first frame update
        void Start()
        {
            dummyAnim = GetComponent<Animator>();

            _isMarked = GetComponentInChildren<Scr_DummyLifeSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(_isMarked);
            if (_isMarked == true)
            {
                dummyAnim.SetBool("Dummy", _isMarked);
            }
        }
    }
}
