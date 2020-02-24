using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "New Forme", menuName = "PlayerForme")]
    public class Data_PlayerForme : ScriptableObject
    {
        [Space(20)]
        public float _maxSpeed = 1;
        [Space(10)]
        public AnimationCurve _accelerationCurve    = AnimationCurve.EaseInOut( 0, 0, 0.6f, 1);
        public AnimationCurve _topSpeedCurve        = AnimationCurve.Constant( 0, 1, 1);
        public AnimationCurve _deccelerationCurve   = AnimationCurve.EaseInOut( 0, 1, 0.4f, 0);

        public void Attack()
        {

        }

        public void Passif()
        {

        }

    }
}