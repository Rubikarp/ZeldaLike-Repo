using UnityEngine.Audio;
using UnityEngine;
using System;

/// Fait par Arthur Deleye
/// private SoundManager _sound;        
/// _sound = FindObjectOfType<ARD_SoundManager>().Play("name");
/// 
namespace Management
{
    public class SoundManager : Singleton<SoundManager>
    {
        public GameSound[] sounds;

        void Awake()
        {
            foreach (GameSound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.loop = s.loop;
            }
        }

        public void Play(string name)
        {
            GameSound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
    }
}