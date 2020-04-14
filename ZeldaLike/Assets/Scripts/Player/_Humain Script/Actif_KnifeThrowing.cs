using UnityEngine;
using Management;

public class Actif_KnifeThrowing : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private InputManager _input = null;
    [SerializeField] private AnimatorManager_Player _animator = null;

    [Space(10)]

    [SerializeField] private GameObject _knife;
    [SerializeField] private Transform _KnifeContainer;
    [SerializeField] private Transform _attackPos;

    [Header("Variable")]
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private float _Cooldown = 0.2f;
    private float _reloadTime = 0f;

    void Start()
    {
        _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    }

    void Update()
    {
        if (_input._attack && _canShoot == true)
        {
            Instantiate(_knife, _attackPos.position, _attackPos.rotation, _KnifeContainer);
            _animator.TriggerAttack();

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

    private void OnDisable()
    {
        _canShoot = true;
    }
}
