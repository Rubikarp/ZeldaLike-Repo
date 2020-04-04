using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BombBehaviour_Ingénieur : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Scr_BombBehaviour_Ingénieur _behavIng = null;
    [SerializeField] private SpriteRenderer bombSprite = null;
    public Transform _bomb = null;
    public GameObject _explosionRange = null;

    [Header("Statistiques")]
    public float _timeBeforeExplosion = 0.7f;
    public float _timer = 2f;

    private void Start()
    {
        _bomb = this.transform;
        _behavIng = this.gameObject.GetComponent<Scr_BombBehaviour_Ingénieur>();
        bombSprite = this.gameObject.GetComponent<SpriteRenderer>();
        bombSprite.enabled = true;
    }

    private void Update()
    {
        _timeBeforeExplosion -= Time.deltaTime;

        if(_timeBeforeExplosion <= 0)
        {
            StartCoroutine(Explosion(_timer));
        }
    }

    public IEnumerator Explosion(float timer)
    {
        bombSprite.enabled = false;
        Instantiate(_explosionRange);
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
