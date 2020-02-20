using UnityEngine;

namespace Game
{
    public class Data_PlayerState : ScriptableObject
    {
        [Header("Can")]
        public bool canMove = true;

        [Header("Is")]
        public bool isAttacking;
        public bool isTalking;
        public bool isRuning;

    }
}