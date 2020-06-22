using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BombBehaviour_Ingénieur : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private SpriteRenderer bombSprite = null;
    public Transform _bomb = null;
    public GameObject _explosionRange = null;

    [Header("Statistiques")]
    public float _timeBeforeExplosion = 0.7f;
    public float _timer = 2f;
    private bool _explosion;

    private void Start()
    {
        _explosion = false;
        _bomb = this.transform;
        bombSprite = this.gameObject.GetComponent<SpriteRenderer>();
        bombSprite.enabled = true;
    }

    private void Update()
    {
        _timeBeforeExplosion -= Time.deltaTime;

        if(_timeBeforeExplosion <= 0 && _explosion == false)
        {
            StartCoroutine(Explosion(_timer));
            _explosion = true;
        }
    }

    public IEnumerator Explosion(float timer)
    {
        bombSprite.enabled = false;
        Instantiate(_explosionRange, this.gameObject.transform);
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
