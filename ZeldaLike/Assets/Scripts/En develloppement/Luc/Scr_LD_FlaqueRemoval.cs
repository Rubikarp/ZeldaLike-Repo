using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Scr_LD_FlaqueRemoval : MonoBehaviour
{
    public GameObject _dangerousBloc;
    public Tilemap _tilemap;
    public Vector3Int _blocPos;
    private bool _flaqueGettingDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_flaqueGettingDestroyed == true)
        {
            _blocPos = _tilemap.WorldToCell(_dangerousBloc.transform.position);
            _tilemap.SetTile(_blocPos, null);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            _flaqueGettingDestroyed = true;
            _dangerousBloc = collision.gameObject;
        }
    } 
}
