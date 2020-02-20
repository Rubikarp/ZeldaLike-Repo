using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AttributionTouches_Transfo : MonoBehaviour
{
    string _human;
    string _agile;
    string _heavy;

    string _actualForm;
    string _leftForm;
    string _rightForm;

    // Start is called before the first frame update
    void Start()
    {
        _human = "Humaine";
        _agile = "Agile";
        _heavy = "Lourde";

        _actualForm = _human;
        TransformCommandSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _actualForm = _leftForm;
            TransformCommandSwitch();

            Debug.Log("Forme de gauche : " + _leftForm);
            Debug.Log("Forme activée : " + _actualForm);
            Debug.Log("Forme de droite : " + _rightForm);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _actualForm = _rightForm;
            TransformCommandSwitch();

            Debug.Log("Forme de gauche : " + _leftForm);
            Debug.Log("Forme activée : " + _actualForm);
            Debug.Log("Forme de droite : " + _rightForm);
        }
    }

    private void TransformCommandSwitch()
    {
        if (_actualForm == _human)
        {
            _leftForm = _heavy;
            _rightForm = _agile;
        }
        else if (_actualForm == _agile)
        {
            _leftForm = _human;
            _rightForm = _heavy;
        }
        else if (_actualForm == _heavy)
        {
            _leftForm = _agile;
            _rightForm = _human;
        }
    }
}
