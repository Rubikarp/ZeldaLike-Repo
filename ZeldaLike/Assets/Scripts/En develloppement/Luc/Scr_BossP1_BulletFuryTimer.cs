using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_BossP1_BulletFuryTimer : MonoBehaviour
{
    public float _lifeTime;
    public List<GameObject> _bullets;
    private bool _allLaunched;

    // Start is called before the first frame update
    void Start()
    {
        _allLaunched = false;
        for (int j = 0; j < _bullets.Count; j++)
        {
            _bullets[j].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_allLaunched == false)
        {
            StartCoroutine(BulletLaunch());
        }

        if (_lifeTime > 0 && _allLaunched == true)
        {
            _lifeTime -= Time.deltaTime;
        }
        else if (_lifeTime <= 0 && _allLaunched == true)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator BulletLaunch()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            _bullets[i].SetActive(true);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
