using System.Collections.Generic;
using UnityEngine;
using Management;

public class Scr_Dial_first_ciné : MonoBehaviour
{
    public Dialog _dialog;
    public List<float> _delai;
    public List<bool> _actifDialog;

    public Transform _enemy1;
    public Transform _enemy2;
    public Transform _enemy3;
    public Transform _enemy4;

    public GameObject _shooter;
    public GameObject _experience;

    private Scr_DialogManager DialogueManager;

    void Start()
    {
        DialogueManager = FindObjectOfType<Scr_DialogManager>();
        DialogueManager.StartDialogue(_dialog);
        _actifDialog[0] = true;
    }

    void Update()
    {
        // Soldat
        if (_delai[0] > 0 && _actifDialog[0] == true)
        {
            _delai[0] -= Time.deltaTime;
        }
        else if (_delai[0] <= 0 && _actifDialog[0] == true)
        {
            Debug.Log("Merde");
            _dialog.name = "Soldat";
            DialogueManager.StartDialogue(_dialog);
            DialogueManager.DisplayNextSentence();
            _actifDialog[0] = false;
            _actifDialog[1] = true;
        }

        // Nahua
        if (_delai[1] > 0 && _actifDialog[1] == true)
        {
            _delai[1] -= Time.deltaTime;
        }
        else if (_delai[1] <= 0 && _actifDialog[1] == true)
        {
            _dialog.name = "Nahua";
            DialogueManager.StartDialogue(_dialog);
            DialogueManager.DisplayNextSentence();
            DialogueManager.DisplayNextSentence();
            _actifDialog[1] = false;
            _actifDialog[2] = true;

        }

        // Soldat
        if (_delai[2] > 0 && _actifDialog[2] == true)
        {
            _delai[2] -= Time.deltaTime;
        }
        else if (_delai[2] <= 0 && _actifDialog[2] == true)
        {
            _dialog.name = "Soldat";
            DialogueManager.StartDialogue(_dialog);
            DialogueManager.DisplayNextSentence();
            DialogueManager.DisplayNextSentence();
            DialogueManager.DisplayNextSentence();
            _actifDialog[2] = false;
            _actifDialog[3] = true;
        }

        // Nahua
        if (_delai[3] > 0 && _actifDialog[3] == true)
        {
            _delai[3] -= Time.deltaTime;
        }
        else if (_delai[3] <= 0 && _actifDialog[3] == true)
        {
            _dialog.name = "Nahua";
            DialogueManager.StartDialogue(_dialog);
            DialogueManager.DisplayNextSentence();
            DialogueManager.DisplayNextSentence();
            DialogueManager.DisplayNextSentence();
            DialogueManager.DisplayNextSentence();
            _actifDialog[3] = false;
            _actifDialog[4] = true;
        }

        // Dernier Délai
        if (_delai[4] > 0 && _actifDialog[4] == true)
        {
            _delai[4] -= Time.deltaTime;
        }
        else if (_delai[4] <= 0 && _actifDialog[4] == true)
        {
            DialogueManager.DisplayNextSentence();
            _actifDialog[4] = false;
            FindObjectOfType<InputManager>().ReActivateControl();
            Instantiate(_experience, _enemy1);
            Instantiate(_experience, _enemy2);
            Instantiate(_experience, _enemy3);
            Instantiate(_shooter, _enemy4);
        }
    }
}
