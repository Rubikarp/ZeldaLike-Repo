using UnityEngine;

namespace Graphique
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticSpriteDepth : MonoBehaviour
    {
        public Transform _pos = null;

        private SpriteRenderer _sprRender = null;

        void Start()
        {
            _pos = this.transform;
            _sprRender = this.GetComponent<SpriteRenderer>();

            if(_pos = null)
            {
            refreshSortingOrder(_pos.position.y);
            }
        }

        public void refreshSortingOrder(float PosY)
        {
            _sprRender.sortingOrder = -Mathf.RoundToInt(PosY);
        }
    }
}