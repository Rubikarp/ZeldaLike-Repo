using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Scr_Damage : MonoBehaviour, Int_Damage
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _stunDuration;
        [SerializeField] private float _knockbackPower;

        public int Damage
        {
            get
            {
                return _damage;
            }
            set
            {
                _damage = value;
            }
        }
        public float StunDuration
        {
            get
            {
                return _stunDuration;
            }
            set
            {
                _stunDuration = value;
            }
        }
        public float KnockbackPower
        {
            get
            {
                return _knockbackPower;
            }
            set
            {
                _knockbackPower = value;
            }
        }
    
    }
}