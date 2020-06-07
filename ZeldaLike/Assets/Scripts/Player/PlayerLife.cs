using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Life", menuName = "Player")]
    public class PlayerLife : ScriptableObject
    {
        public int life = 6;
        public int maxlife = 6;
    }
}