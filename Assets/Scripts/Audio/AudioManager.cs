using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

   

    private Dictionary<string, Sound> soundsDict = new Dictionary<string, Sound>();

    

    public AudioMixerGroup masterGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Music Sources")]
    [SerializeField] private AudioSource forestMusic;
    [SerializeField] private AudioSource endingMusic;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        foreach(Sound sound in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            float volume = sound.volume;
            audioSource.volume = volume;
            audioSource.outputAudioMixerGroup = sfxGroup;
            sound.source = audioSource;
            soundsDict[sound.name] = sound;
        }

        //forestMusic.Play();
        //forestMusicReverbStart.PlayDelayed(forestMusic.clip.length);
    }

    public void PlaySound(string name)
    {
        Sound sound = soundsDict[name];
        sound.source.PlayOneShot(sound.GetClip());
    }

    public void PlayFinalMusic()
    {
        forestMusic.Stop();
        endingMusic.Play();
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        [HideInInspector] public AudioSource source;
        public AudioClip[] clips;
        private int currentClipIndex = -1;
        [Range(0.0001f, 1f)] public float volume;

        public AudioClip GetClip()
        {
            if(currentClipIndex + 1 < clips.Length)
            {
                currentClipIndex++;
            }
            else
            {
                currentClipIndex = 0;
            }

            return clips[currentClipIndex];
        }
    }
}

