using UnityEngine;

namespace Game
{
    public class OnSwitch_StunZoneApparrition : Singleton<OnSwitch_StunZoneApparrition>
    {
        [SerializeField] GameObject _StunZonePrefab = null;
        [SerializeField] Transform _attackContainer = null;

        private void OnEnable()
        {
            Instantiate(_StunZonePrefab, this.transform.position, Quaternion.identity, _attackContainer);
        }

        public void StunZoneApparrition()
        {
            Instantiate(_StunZonePrefab, this.transform.position, Quaternion.identity, _attackContainer);

        }
    }
}