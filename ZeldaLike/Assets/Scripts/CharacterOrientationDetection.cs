using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientationDetection : MonoBehaviour
{
    Vector2 _orientation;
    public Vector2 _lastOrientation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //On récupère l'orientation du joystick (marche avec les flèches aussi).
        _orientation.x = Input.GetAxisRaw("Horizontal");
        _orientation.y = Input.GetAxisRaw("Vertical");

        //On mémorise cette orientation pour les cas où le joystick n'est pas touché.
        if (_orientation.x != 0 || _orientation.y != 0)
        {
            _lastOrientation = _orientation;
        }
    }
}
