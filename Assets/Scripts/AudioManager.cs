using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<String, SoundSO> _sounds = new Dictionary<string, SoundSO>();

    protected AudioManager() {}

    private void Awake()
    {
        foreach (SoundSO s in Resources.LoadAll<SoundSO>("Sounds")){
            _sounds[s.soundName] = s;
            s.AddSource(gameObject.AddComponent<AudioSource>());
        }
    }
    
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
}
