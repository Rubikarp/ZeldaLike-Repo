using UnityEngine;

public class Scr_Donjon_Rest : MonoBehaviour
{
    public GameObject _puzzle;
    public Transform _puzzlePosition;
    private Vector3 _spawnPosition;

    void Start()
    {
        _spawnPosition = _puzzlePosition.position;
    }

    public void Reset_Puzzle ()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Instantiate(_puzzle, _spawnPosition, Quaternion.identity, transform);
    }
}
