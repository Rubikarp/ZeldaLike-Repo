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
            _pos = this.transform;
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