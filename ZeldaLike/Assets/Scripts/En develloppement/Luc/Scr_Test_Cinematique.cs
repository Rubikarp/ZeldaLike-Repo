using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Management;

public class Scr_Test_Cinematique : MonoBehaviour
{

    public Dialog _dialogue;
    private int _talkCount;
    private float _talkDelay;
    private bool _isTalking;


    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Scr_DialogManager>().StartDialogue(_dialogue);
        _isTalking = true;
        _talkDelay = 5;
        _talkCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (_isTalking == true && _talkDelay > 0)
        {
            _talkDelay -= Time.deltaTime;
        }
        else if (_isTalking == true && _talkDelay <= 0)
        {
            FindObjectOfType<Scr_DialogManager>().DisplayNextSentence();
            _talkCount += 1;
            _talkDelay = 5;
        }

        if (_isTalking == true && _talkCount > _dialogue.sentences.Length)
        {
            _isTalking = false;
            _talkCount = 0;
        }
    }
}
