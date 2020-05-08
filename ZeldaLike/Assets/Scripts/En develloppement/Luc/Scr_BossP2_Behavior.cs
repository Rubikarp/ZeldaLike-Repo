using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Game;

namespace Ennemies
{
    public class Scr_BossP2_Behavior : MonoBehaviour
    {
        enum Pattern { Rien, Renforts, MonArmee, Laser, Aspiration, CoupMassif};
        private Pattern _actualPattern = 0;
        private int _patternCount = 0;
        private bool _inPattern;
        public float _delayBetweenPatterns;
        private GameObject _player;
        public Transform _attackPos;
        public Vector3 _bossDirection;
        public GameObject[] _interrupteursTargets;

        [Header("Renforts")]
        public List<GameObject> _ennemiesRenforts;
        public List<Transform> _renfortsSpawns;
        public float _castDelayRenforts;

        [Header("MonArmée")]
        public List<GameObject> _ennemiesArmy;
        public List<Transform> _armySpawns;
        public float _castDelayArmy;

        [Header("Laser")]
        public float _laserRotateSpeed;
        public GameObject _laserHit;
        public LayerMask _laserMask;
        private bool _playerHit;
        public LineRenderer _laserGraph;
        private float _rotation;
        public Transform _laserPos;
        public float _laserCastTime;

        [Header("Aspiration")]
        public float _aspirationRange;
        public float _aspirationSpeed;
        public float _aspirationTime;
        public float _repulseRange;
        public float _repulseSpeed;
        public float _repulseTime;
        public LayerMask _playerMask;
        public LayerMask _enemyMask;

        [Header("CoupMassif")]
        public Transform _coupMassifPos;
        public GameObject _coupMassifHitbox;

        [Header("Jet de débris")]
        public GameObject _projectileThrown;

    // Start is called before the first frame update
    void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _inPattern = false;
            _playerHit = false;
        }

        // Update is called once per frame
        void Update()
        {
            _bossDirection = (_player.transform.position - transform.position);

            if (_inPattern == false)
            {
                if (_patternCount < 3)
                {
                    switch (_actualPattern)
                    {
                        case Pattern.Rien:
                            NextDirection();
                            Debug.Log("Rien");
                            break;

                        case Pattern.Renforts:
                            _inPattern = true;
                            StartCoroutine(Renforts());
                            Debug.Log("Renforts");
                            NextDirection();
                            break;

                        case Pattern.MonArmee:
                            _inPattern = true;
                            StartCoroutine(MonArmee());
                            Debug.Log("MonArmée");
                            NextDirection();
                            break;

                        case Pattern.Laser:
                            _inPattern = true;
                            StartCoroutine(LaserPattern());
                            Debug.Log("Laser");
                            NextDirection();
                            break;

                        case Pattern.Aspiration:
                            _inPattern = true;
                            StartCoroutine(Aspiration(_aspirationTime, _repulseTime));
                            Debug.Log("Aspiration");
                            NextDirection();
                            break;

                        case Pattern.CoupMassif:
                            _inPattern = true;
                            StartCoroutine(CoupMassif());
                            Debug.Log("CoupMassif");
                            NextDirection();
                            break;
                    }

                    _patternCount += 1;
                }
                else if (_patternCount == 3)
                {
                    _inPattern = true;
                    StartCoroutine(JetDeDebris());
                    Debug.Log("Jet de Débris");
                    NextDirection();
                    _patternCount = 0;
                }
            }

            if (_interrupteursTargets[0].GetComponent<Scr_LD_ActiveState>()._isActive == true)
            {
                if (_interrupteursTargets[1].GetComponent<Scr_LD_ActiveState>()._isActive == true)
                {
                    if (_interrupteursTargets[2].GetComponent<Scr_LD_ActiveState>()._isActive == true)
                    {
                        if (_interrupteursTargets[3].GetComponent<Scr_LD_ActiveState>()._isActive == true)
                        {
                            if (_interrupteursTargets[4].GetComponent<Scr_LD_ActiveState>()._isActive == true)
                            {
                                Destroy(gameObject);
                            }
                        }
                    }
                }
            }
        }

        private void NextDirection()
        {
            int randomPattern = 0;

            randomPattern = Random.Range(0, 12);

            if (randomPattern == 0 || randomPattern == 1)
            {
                _actualPattern = Pattern.Renforts;
            }
            else if (randomPattern == 2)
            {
                _actualPattern = Pattern.MonArmee;
            }
            else if (randomPattern == 3 || randomPattern == 4 || randomPattern == 5)
            {
                _actualPattern = Pattern.Laser;
            }
            else if (randomPattern == 6 || randomPattern == 7 || randomPattern == 8)
            {
                _actualPattern = Pattern.Aspiration;
            }
            else if (randomPattern == 9 || randomPattern == 10 ||randomPattern == 11)
            {
                _actualPattern = Pattern.CoupMassif;
            }
        }

        private IEnumerator Renforts()
        {
            yield return new WaitForSeconds(_castDelayRenforts);

            for (int i = 0; i < _ennemiesRenforts.Count; i++)
            {
                Instantiate(_ennemiesRenforts[i], _renfortsSpawns[i]);
            }

            yield return new WaitForSeconds(_delayBetweenPatterns);
            _inPattern = false;
        }

        private IEnumerator MonArmee()
        {
            yield return new WaitForSeconds(_castDelayArmy);

            for (int j = 0; j < _ennemiesArmy.Count; j++)
            {
                Instantiate(_ennemiesArmy[j], _armySpawns[j]);
            }

            yield return new WaitForSeconds(_delayBetweenPatterns);
            _inPattern = false;
        }

        private IEnumerator LaserPattern()
        {
            yield return new WaitForSeconds(_laserCastTime);
            while (_rotation > -360)
            {
                LaserBehavior(_laserPos.position, -_laserPos.up);
                _rotation -= Time.deltaTime * _laserRotateSpeed;
                _laserPos.rotation = Quaternion.Euler(0f, 0f, _rotation);
                yield return new WaitForEndOfFrame();
                Debug.Log("Un fois");
            }

            _rotation = 0;
            _laserPos.rotation = Quaternion.Euler(0f, 0f, 0f);
            _laserGraph.enabled = false;

            yield return new WaitForSeconds(_delayBetweenPatterns);
            _inPattern = false;
        }

        private void LaserBehavior(Vector2 origin, Vector2 destination)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(origin, destination, Mathf.Infinity, _laserMask);

            if (hitInfo)
            {
                if (hitInfo.transform.gameObject.CompareTag("Environment"))
                {
                    Debug.Log("Laser bloqué");
                }
                else if (!hitInfo.transform.gameObject.CompareTag("Environment"))
                {
                    Debug.Log("Laser traverse");

                    if (hitInfo.transform.gameObject.CompareTag("Player") && _playerHit == false)
                    {
                        Instantiate(_laserHit, hitInfo.transform.position, hitInfo.transform.rotation);
                        _playerHit = true;
                        Debug.Log("Blessure");
                        _laserMask = LayerMask.GetMask("Nothing");
                        _laserMask = LayerMask.GetMask("Default");
                    }
                }

                _laserGraph.enabled = true;
                _laserGraph.SetPosition(0, origin);
                _laserGraph.SetPosition(1, hitInfo.transform.position);

                Debug.Log(hitInfo.transform.name);
                Debug.Log(hitInfo.transform.position);
            }
            else
            {
                Debug.Log("Nada");
            }
        }

        private IEnumerator Aspiration(float aspiTime, float repulseTime)
        {
            while (aspiTime > 0)
            {
                Collider2D[] playerToAspi = Physics2D.OverlapCircleAll(transform.position, _aspirationRange, _playerMask);
                Collider2D[] enemyToAspi = Physics2D.OverlapCircleAll(transform.position, _aspirationRange, _enemyMask);

                for (int k = 0; k < playerToAspi.Length; k++)
                {
                    playerToAspi[k].gameObject.transform.parent.parent.transform.position = Vector2.MoveTowards(playerToAspi[k].gameObject.transform.parent.parent.transform.position, transform.position, _aspirationSpeed * Time.deltaTime);
                }

                for (int h = 0; h < enemyToAspi.Length; h++)
                {
                    enemyToAspi[h].gameObject.transform.position = Vector2.MoveTowards(enemyToAspi[h].gameObject.transform.position, transform.position, _aspirationSpeed * Time.deltaTime);
                }

                aspiTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(0.25f);

            while (repulseTime > 0)
            {
                Collider2D[] playerToRepulse = Physics2D.OverlapCircleAll(transform.position, _aspirationRange, _playerMask);
                Collider2D[] enemyToRepulse = Physics2D.OverlapCircleAll(transform.position, _aspirationRange, _enemyMask);

                for (int m = 0; m < playerToRepulse.Length; m++)
                {
                    playerToRepulse[m].gameObject.transform.parent.parent.transform.position = Vector2.MoveTowards(playerToRepulse[m].gameObject.transform.parent.parent.transform.position, transform.position, -_repulseSpeed * Time.deltaTime);
                }

                for (int n = 0; n < enemyToRepulse.Length; n++)
                {
                    enemyToRepulse[n].gameObject.transform.position = Vector2.MoveTowards(enemyToRepulse[n].gameObject.transform.position, transform.position, -_repulseSpeed * Time.deltaTime);
                }

                repulseTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }


            yield return new WaitForSeconds(_delayBetweenPatterns);
            _inPattern = false;
        }

        private IEnumerator CoupMassif()
        {
            Instantiate(_coupMassifHitbox, _coupMassifPos.position + _bossDirection.normalized * 25, _coupMassifPos.rotation, _coupMassifPos);

            yield return new WaitForSeconds(_delayBetweenPatterns);
            _inPattern = false;
        }

        private IEnumerator JetDeDebris()
        {
           Instantiate(_projectileThrown, _attackPos.position, transform.rotation, _attackPos);

            yield return new WaitForSeconds(_delayBetweenPatterns);
            _inPattern = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aspirationRange);
        }
    }
}
