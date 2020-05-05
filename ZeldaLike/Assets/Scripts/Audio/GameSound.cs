using UnityEngine;

namespace Management
{
    [System.Serializable]
    public class GameSound
    {
        [Header("NAME")]
        public string name;

        [Space(10)]

        [Header("CLIP")]
        public AudioClip clip;
        [HideInInspector] 
        public AudioSource source;

        [Space(10)]

        [Header("SETTINGS")]
        public bool loop;

    }
}