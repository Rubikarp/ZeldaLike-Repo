using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_CombatTheme : MonoBehaviour
    {
        private MusicManager _music;
        [SerializeField] private string oldSong;
        [SerializeField] private string newSong;
        [SerializeField] private string nextSong;

        private void Awake()
        {
            _music = MusicManager.Instance;
        }

        private void OnEnable()
        {
            _music.StopMusic(oldSong);
            _music.PlayMusic(newSong);
        }

        private void OnDisable()
        {
            _music.StopMusic(newSong);
            _music.PlayMusic(nextSong);
        }
    }
}