using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
     
    public Sound[] sounds;
    public static AudioManager instance;
    // Before game starts (before the start method)
    void Awake()
    {
        //Music blijft ook na het switchen naar een ander scene
        if (instance == null)
            instance = this;
        else
        {
            //zorgen voor geen 2 AudioManagers
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        //making audio source for each sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

   
    public void Play (string name)
    {
        //finding sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //error mesage
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " Not found!");
            return;
        }
            //playing sound
        s.source.Play();
    }
}
