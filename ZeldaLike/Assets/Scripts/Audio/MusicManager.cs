using UnityEngine.Audio;
using UnityEngine;
using System;

/// Fait par Arthur Deleye
/// private MusicManager music;        
/// music = MusicManager.instance;
/// 

namespace Game
{
    public class MusicManager : Singleton<MusicManager>
    {
        [SerializeField] public Music[] musics;

        void Awake()
        {
            foreach (Music son in musics)
            {
                son.source = gameObject.AddComponent<AudioSource>();
                son.source.clip = son.clip;

                son.source.loop = son.loop;
            }
        }

        //Musique
        public void PlayMusic(string name)
        {
            Music son = Array.Find(musics, sound => sound.name == name);
            son.source.Play();
        }
        public void StopMusic(string name)
        {
            Music son = Array.Find(musics, sound => sound.name == name);
            son.source.Stop();
        }

    }
}