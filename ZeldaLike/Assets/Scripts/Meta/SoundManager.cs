using UnityEngine.Audio;
using UnityEngine;
using System;
using Management;

/// Fait par Arthur Deleye
/// private SoundManager sound;        
/// sound = SoundManager.instance;
/// 

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] public GameSound[] musics;
    [SerializeField] public GameSound[] sounds;

    void Awake()
    {
        foreach (GameSound son in sounds)
        {
            son.source = gameObject.AddComponent<AudioSource>();
            son.source.clip = son.clip;

            son.source.loop = son.loop;
        }
    }

    //Bruitage
    public void PlaySound(string name)
    {
        GameSound son = Array.Find(sounds, sound => sound.name == name);
        son.source.Play();
    }
    public void StopSound(string name)
    {
        GameSound son = Array.Find(sounds, sound => sound.name == name);
        son.source.Stop();
    }

    //Musique
    public void PlayMusic(string name)
    {
        GameSound son = Array.Find(musics, sound => sound.name == name);
        son.source.Play();
    }
    public void StopMusic(string name)
    {
        GameSound son = Array.Find(musics, sound => sound.name == name);
        son.source.Stop();
    }

}
