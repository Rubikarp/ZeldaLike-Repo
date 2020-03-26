using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class Scr_LD_FlaqueRemoval : MonoBehaviour
    {
        public GameObject _dangerousBloc;
        public Tilemap _tilemap;
        public Vector3Int _blocPos;
        public Vector3Int _blocPos1;
        public Vector3Int _blocPos2;
        public Vector3Int _blocPos3;
        public Vector3Int _blocPos4;
        public Vector3Int _blocPos5;
        public Vector3Int _blocPos6;
        public Vector3Int _blocPos7;
        public Vector3Int _blocPos8;

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

                _blocPos1 = new Vector3Int(_blocPos.x + 1, _blocPos.y, _blocPos.z);
                _blocPos2 = new Vector3Int(_blocPos.x, _blocPos.y + 1, _blocPos.z);
                _blocPos3 = new Vector3Int(_blocPos.x + 1, _blocPos.y + 1, _blocPos.z);
                _blocPos4 = new Vector3Int(_blocPos.x + 1, _blocPos.y - 1, _blocPos.z);
                _blocPos5 = new Vector3Int(_blocPos.x, _blocPos.y - 1, _blocPos.z);
                _blocPos6 = new Vector3Int(_blocPos.x - 1, _blocPos.y - 1, _blocPos.z);
                _blocPos7 = new Vector3Int(_blocPos.x - 1, _blocPos.y, _blocPos.z);
                _blocPos8 = new Vector3Int(_blocPos.x - 1, _blocPos.y + 1, _blocPos.z);

                _tilemap.SetTile(_blocPos, null);
                _tilemap.SetTile(_blocPos1, null);
                _tilemap.SetTile(_blocPos2, null);
                _tilemap.SetTile(_blocPos3, null);
                _tilemap.SetTile(_blocPos4, null);
                _tilemap.SetTile(_blocPos5, null);
                _tilemap.SetTile(_blocPos6, null);
                _tilemap.SetTile(_blocPos7, null);
                _tilemap.SetTile(_blocPos8, null);
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
}

