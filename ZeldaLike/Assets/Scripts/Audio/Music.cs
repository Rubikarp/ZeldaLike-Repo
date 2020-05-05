using UnityEngine;

[CreateAssetMenu(fileName = "New_Music", menuName = "Audio/Musique")]
public class Music : ScriptableObject
{
    [Header("CLIP")]
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;

    [Space(5)]
    [Header("SETTINGS")]
    public float volume = 1f;
    public float pitch = 1f;
    public bool loop = true;
}
