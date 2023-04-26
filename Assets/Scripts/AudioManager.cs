using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    //private AudioSource audioSource;
    public AudioClip desiredClip;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            //s.source.loop = s.loop; // set the loop property of the AudioSource
        }

    }

   public void Play(string name)
   {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Play();
   }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    private void Start()
    {
        // Get all the AudioSources on the current GameObject
        AudioSource[] audioSources = GetComponents<AudioSource>();

        // Loop through all the AudioSources
        foreach (AudioSource audioSource in audioSources)
        {
            // Check if the current AudioSource's clip is the desired clip
            if (audioSource.clip == desiredClip)
            {
                // Set the loop property to true
                audioSource.loop = true;
                break; // Exit the loop, since we found the desired AudioSource
            }
        }
    }


}
