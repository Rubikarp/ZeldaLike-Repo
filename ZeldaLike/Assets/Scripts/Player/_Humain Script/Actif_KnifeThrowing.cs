using System.Collections;
using UnityEngine;
using Management;
using UnityEngine.Events;

public class Actif_KnifeThrowing : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private InputManager _input = null;
    [SerializeField] private AnimatorManager_Player _animator = null;
    [SerializeField] private SoundManager sound;

    [Space(10)]

    [SerializeField] private GameObject _knife = null;
    [SerializeField] private Transform _KnifeContainer = null;
    [SerializeField] private Transform _attackPos = null;
    public UnityEvent Actif;

    [Header("Variable")]
    public bool _canShoot = true;
    [SerializeField] private float _Cooldown = 0.2f;
    private float _reloadTime = 0f;

    void Awake()
    {
        sound = SoundManager.Instance;
    }

    void Start()
    {
        _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    }

    void Update()
    {
        if (_input._attack && _canShoot == true)
        {
            sound.PlaySound("AttackHuman");

            Throwing();

            _canShoot = false;
            _reloadTime = _Cooldown;
        }
        else
        {
            _reloadTime -= Time.deltaTime;

            if(_reloadTime <= 0)
            {
                _canShoot = true;
            }
        }
    }

    void Throwing()
    {
        _animator.TriggerAttack();
        Instantiate(_knife, _attackPos.position, _attackPos.rotation, _KnifeContainer);

    }

    private void OnDisable()
    {
        _canShoot = true;
    }
}
