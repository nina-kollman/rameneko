using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<String, SoundSO> _sounds = new Dictionary<string, SoundSO>();

    protected AudioManager() {}

    private void Awake()
    {
        // Loads all SoundSo 
        foreach (SoundSO s in Resources.LoadAll<SoundSO>("Sounds")){
            _sounds[s.soundName] = s;
            s.AddSource(gameObject.AddComponent<AudioSource>());
        }
    }
    
    /**
     * The function plays the audio file according to its given name
     */
    public void Play(string name){
        if(!_sounds.ContainsKey(name)){
            Debug.LogWarning("Missing Sound:" + name);
            return;
        }
        if (!_sounds[name].IsPlaying())
        {
            _sounds[name].Play();
        }
    }

    /**
     * The function stops the audio file according to its given name
     */
    public void Stop(string name)
    {
        if(!_sounds.ContainsKey(name)){
            Debug.LogWarning("Missing Sound:" + name);
            return;
        }
        if (_sounds[name].IsPlaying())
        {
            _sounds[name].Stop();
        } 
    }
}
