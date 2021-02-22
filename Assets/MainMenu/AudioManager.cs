using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sounds[] sounds;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.Output;
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s!=null)
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Stop();
    }

    public void Pause(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Pause();
    }
    public void UnPause(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.UnPause();
    }
}
