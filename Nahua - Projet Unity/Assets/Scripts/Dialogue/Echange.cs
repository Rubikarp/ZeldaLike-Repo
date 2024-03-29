﻿using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New_Echange", menuName = "Dialogue/Echange")]
    public class Echange : ScriptableObject
    {
        [Header("Interlocuteur A qui parle à B")]
        public string interlocuteur_A;
        public string interlocuteur_B;

        public Sprite image_A;
        public Sprite image_B;

        [Header("Phrase de A /Réponse de B")]
        [TextArea(2, 10)] public string[] _echange;


    }
}