using UnityEngine;

namespace Game
{
    public class MarkApparrition : MonoBehaviour
    {
        [SerializeField] GameObject _mark = null;

        private void OnEnable()
        {
            Instantiate(_mark, this.transform.position, Quaternion.identity);
        }
    }
}