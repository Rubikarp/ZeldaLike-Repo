using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Scr_Damage : MonoBehaviour, Int_Damage
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _stunDuration = 0.25f;
        [SerializeField] private float _knockbackPower = 10f;

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }
        public float StunDuration
        {
            get { return _stunDuration; }
            set { _stunDuration = value; }
        }
        public float KnockbackPower
        {
            get { return _knockbackPower; }
            set { _knockbackPower = value; }
        }
    
    }
}