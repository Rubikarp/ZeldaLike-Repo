using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemies
{
    public class Scr_BossP2_Behavior : MonoBehaviour
    {
        enum Pattern { Rien, Renforts, MonArmee, Laser, Aspiration, CoupMassif};
        private Pattern _actualPattern = 0;
        private int _patternCount = 0;
        private bool _inPattern;

        [Header("Renforts")]
        public List<GameObject> _ennemiesRenforts;
        public List<Transform> _renfortsSpawns;

        [Header("MonArmée")]
        public List<GameObject> _ennemiesArmy;
        public List<Transform> _armySpawns;

        [Header("Laser")]
        public float _laserTimer;

        [Header("Aspirtaion")]
        public float _aspirationRange;

        [Header("CoupMassif")]
        public float _hitDelay;

        [Header("Jet de débris")]
        public GameObject _projectileThrown;

    // Start is called before the first frame update
    void Start()
        {
            _inPattern = false;
        }

        // Update is called once per frame
        void Update()
        {
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
                            Renforts();
                            Debug.Log("Renforts");
                            break;

                        case Pattern.MonArmee:
                            _inPattern = true;
                            MonArmee();
                            Debug.Log("MonArmée");
                            break;

                        case Pattern.Laser:
                            _inPattern = true;
                            Laser();
                            Debug.Log("Laser");
                            break;

                        case Pattern.Aspiration:
                            _inPattern = true;
                            Aspiration();
                            Debug.Log("Aspiration");
                            break;

                        case Pattern.CoupMassif:
                            _inPattern = true;
                            CoupMassif();
                            Debug.Log("CoupMassif");
                            break;
                    }
                }
                else if (_patternCount == 3)
                {
                    _inPattern = true;
                    JetDeDebris();
                    Debug.Log("Jet de Débris");
                    _patternCount = 0;
                }
            }
        }

        private void NextDirection()
        {
            int randomPattern = 0;

            randomPattern = Random.Range(0, 5);

            if (randomPattern == 0)
            {
                _actualPattern = Pattern.Renforts;
            }
            else if (randomPattern == 1)
            {
                _actualPattern = Pattern.MonArmee;
            }
            else if (randomPattern == 2)
            {
                _actualPattern = Pattern.Laser;
            }
            else if (randomPattern == 3)
            {
                _actualPattern = Pattern.Aspiration;
            }
            else if (randomPattern == 4)
            {
                _actualPattern = Pattern.CoupMassif;
            }
        }

        private void Renforts()
        {

        }

        private void MonArmee()
        {

        }

        private void Laser()
        {

        }

        private void Aspiration()
        {

        }

        private void CoupMassif()
        {

        }

        private void JetDeDebris()
        {

        }
    }
}
