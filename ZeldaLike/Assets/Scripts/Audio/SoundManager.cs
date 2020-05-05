using UnityEngine.Audio;
using UnityEngine;
using System;

/// Fait par Arthur Deleye
/// private SoundManager sound;        
/// sound = SoundManager.instance;
/// 

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] public SoundEffect[] sounds;

    void Awake()
    {
        foreach (SoundEffect son in sounds)
        {
            son.source = gameObject.AddComponent<AudioSource>();
            son.source.clip = son.clip;

            son.source.loop = son.loop;
        }
    }

    //Bruitage
    public void PlaySound(string name)
    {
        SoundEffect son = Array.Find(sounds, sound => sound.name == name);
        son.source.Play();
    }
    public void StopSound(string name)
    {
        SoundEffect son = Array.Find(sounds, sound => sound.name == name);
        son.source.Stop();
    }

}
