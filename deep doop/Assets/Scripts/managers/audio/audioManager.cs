using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static audioManager audioManagerInstance;

    // Start is called before the first frame update
    void Awake()
    {
        if(audioManagerInstance == null)
            audioManagerInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //to use => FindObjectOfType<audioManager>().Play("name");
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("sound "+name+" not found");
            return;
        }
            
        s.source.Play();

    }
}
