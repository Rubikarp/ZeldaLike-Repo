using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class Scr_BossP2_DebrisDestruction : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Environment"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}

