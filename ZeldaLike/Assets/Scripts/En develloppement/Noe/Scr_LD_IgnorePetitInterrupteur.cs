using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LD_IgnorePetitInterrupteur : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PetitInterrupteur")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
