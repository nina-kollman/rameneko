using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "SoundSO")]
public class SoundSO : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    [Range(0f,1f)] public float volume;
    [Range(.1f,3f)] public float pitch;
    public bool loop;
    public bool overlap;
    
    [HideInInspector]
    public AudioSource source;

    public void AddSource(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
    }

    public bool IsPlaying()
    {
        return !overlap && source.isPlaying;
    }

    public void Play()
    {
        source.Play();
    }
}
