using UnityEngine;

namespace Game
{
    public class OnSwitch_MarkApparrition : MonoBehaviour
    {
        [SerializeField] GameObject _markPrefab = null;
        [SerializeField] Transform  _markContainer = null;

        private void OnEnable()
        {
            //Pour ne laisser qu'une marque active
            if(_markContainer.childCount >=1)
            {
                Destroy(_markContainer.GetChild(0).gameObject);
            }
            
            Instantiate(_markPrefab, this.transform.position, Quaternion.identity, _markContainer);
        }

        public void MarkApparrition()
        {
            if (_markContainer.childCount >= 1)
            {
                Destroy(_markContainer.GetChild(0).gameObject);
            }

            Instantiate(_markPrefab, this.transform.position, Quaternion.identity, _markContainer);

        }
    }
}