using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class OnSwitch_StunZoneApparrition : MonoBehaviour
    {
        [SerializeField] GameObject _StunZonePrefab = null;
        [SerializeField] Transform _attackContainer = null;

        private void OnEnable()
        {
            Instantiate(_StunZonePrefab, this.transform.position, Quaternion.identity, _attackContainer);
        }

    }
}