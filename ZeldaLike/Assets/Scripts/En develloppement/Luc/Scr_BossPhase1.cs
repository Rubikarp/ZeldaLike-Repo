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
    public Transform _mySelf;
    private GameObject _player;

    [Header("Renforts")]
    public List<GameObject> _renforts;
    public List<Transform> _renfortsSpawns;

    [Header("Tirs")]
    public GameObject _bullet;
    public float _couvertureSpeed;
    private float _couvertureDuration;
    public float _couvertureDurationOrigin;
    public Transform _bulletContainer;
    public float _shootingAllonge;

    [Header("Attaque au CaC")]
    public float _attackRange;

    [Header("Grenade")]
    public GameObject _grenade;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); 
        _actionActive = false;
        _delay = _delayBetweenActions;
        _canGoDelay = false;
        _couvertureDuration = _couvertureDurationOrigin;
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

                case 1:
                    TirDeCouverture();
                    _actionActive = true;
                    break;

                case 2:
                    Grenade();
                    _actionActive = true;
                    break;

                case 3:
                    AttaqueCaC();
                    _actionActive = true;
                    break;

                case 4:
                    FouDeLaGachette();
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

    private void TirDeCouverture()
    {
        Instantiate(_bullet, _mySelf.position + _player.transform.position.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);

        while (_couvertureDuration > 0)
        {
            _couvertureDuration -= Time.deltaTime;

            _mySelf.position = Vector2.MoveTowards(_mySelf.position, -_player.transform.position, _couvertureSpeed * Time.deltaTime);

            new WaitForEndOfFrame();
        }
    }

    private void Grenade()
    {

    }

    private void AttaqueCaC()
    {

    }

    private void FouDeLaGachette()
    {
        
    }
}
