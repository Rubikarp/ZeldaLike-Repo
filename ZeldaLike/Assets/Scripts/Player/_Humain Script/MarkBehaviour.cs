using UnityEngine;
using Management;

public class MarkBehaviour : MonoBehaviour
{
    private GameObject _player;

    [SerializeField]
    private InputManager _input = null;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
    }

    void Update()
    {
        if (_input._interaction)
        {
            _player.transform.position = gameObject.transform.position;
        }
    }

}
