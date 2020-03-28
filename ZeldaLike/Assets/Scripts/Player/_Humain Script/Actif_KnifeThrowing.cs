using UnityEngine;
using Management;

public class Actif_KnifeThrowing : MonoBehaviour
{

    [SerializeField] private InputManager _input = null;
    [SerializeField] private AnimatorManager _animator = null;

    public GameObject _knife;
    public Transform _Container;
    public Transform _attackPos;

    [SerializeField] private bool _goodToShoot = true;
    public float _throwRecup = 0.2f;
    public float _recupTimer = 0f;

    void Start()
    {
        _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    }

    void Update()
    {
        if (_input._attack && _goodToShoot == true)
        {
            Instantiate(_knife, _attackPos.position, _attackPos.rotation, _Container);
            _animator.TriggerAttack();

            _goodToShoot = false;
            _recupTimer = _throwRecup;
        }
        else
        {
            _recupTimer -= Time.deltaTime;

            if(_recupTimer <= 0)
            {
                _goodToShoot = true;
            }
        }
    }
}
