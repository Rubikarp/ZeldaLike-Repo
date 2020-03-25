using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Dailogues_Test : MonoBehaviour
{
    public Dialog _dialogue;
    private bool _isTalking;
    private int _talkCount;

    // Start is called before the first frame update
    void Start()
    {
        _isTalking = false;
        _talkCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && _isTalking == false)
        {
            TriggerDialogue();
            _talkCount += 1;
        }
        else if (Input.GetButtonDown("Attack") && _isTalking == true)
        {
            ContinueDialogue();
            _talkCount += 1; 

            if(_talkCount == _dialogue.sentences.Length)
            {
                _isTalking = false;
                _talkCount = 0;
            }
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogue);
        _isTalking = true;
    }

    public void ContinueDialogue()
    {
        FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
    }
}
