using UnityEngine;

namespace Graphique
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MovingSpriteDepth : MonoBehaviour
    {
        public Transform _pos = null;

        private SpriteRenderer _sprRender = null;

        void Start()
        {
            if(_pos == null)
            {
                Debug.Log(this.gameObject.name + " n'a pas eut sa position référencée"); 
            }
            _sprRender = this.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            refreshSortingOrder(_pos.position.y);
        }

        public void refreshSortingOrder(float PosY)
        {
            _sprRender.sortingOrder = -Mathf.RoundToInt(PosY);
        }
    }
}