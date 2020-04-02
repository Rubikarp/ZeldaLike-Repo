using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossPhase1 : MonoBehaviour
{
    private bool _actionActive;
    public float _delayBetweenActions;
    private float _delay;
    private bool _canGoDelay;
    private int _randomAction;

    public List<GameObject> _renforts;
    public List<Transform> _renfortsSpawns;
    // Start is called before the first frame update
    void Start()
    {
        _actionActive = false;
        _delay = _delayBetweenActions;
        _canGoDelay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canGoDelay == true)
        {
            if (_delay > 0)
            {
                _delay -= Time.deltaTime;
            }
            else if (_delay <= 0)
            {
                _delay = _delayBetweenActions;
                _canGoDelay = false;
                _actionActive = false;
            }
        }

        if (_actionActive == false)
        {
            _randomAction = Random.Range(0, 4);

            switch (_randomAction)
            {
                case 0:
                    Renforts();
                    _actionActive = true;
                    break;
            }
        }
    }

    private void Renforts()
    {
        for (int i = 0; i <= _renforts.Count; i++)
        {
            Instantiate(_renforts[i], _renfortsSpawns[i]);
        }

        _canGoDelay = true;
    }
}
