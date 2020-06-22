using UnityEngine;

namespace Game
{
    public class Scr_LD_D_E3_P1_Door : MonoBehaviour
    {

        Transform[] allChildren;

        public Scr_LD_Interrupteur Button1;
        public Scr_LD_Interrupteur Button2;

        // Start is called before the first frame update
        void Start()
        {
            allChildren = GetComponentsInChildren<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Button1._isActive == true && Button2._isActive == true)
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
