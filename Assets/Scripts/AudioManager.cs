using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    [SerializeField] public Sound[] backgroundMusic, sfxSounds;
    [SerializeField] public AudioSource backgroundMusicSource, sfxSoundsSource;
    [SerializeField] public AudioClip[] shootSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(backgroundMusic, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            backgroundMusicSource.clip = s.clip;
            backgroundMusicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSoundsSource.PlayOneShot(s.clip);


        }
    }

    public void PlayRandomShoot()
    {
        int index = UnityEngine.Random.Range(0, shootSounds.Length);
        AudioSource.PlayClipAtPoint(shootSounds[index], transform.position);
        //Debug.Log("Shoot sound effect #" + index + " played.");
    }

    public void ToggleMaster()
    {
        backgroundMusicSource.mute = !backgroundMusicSource.mute;
        sfxSoundsSource.mute = !sfxSoundsSource.mute;
    }
    public void ToggleMusic()
    {
        backgroundMusicSource.mute = !backgroundMusicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSoundsSource.mute = !sfxSoundsSource.mute;
    }
    public void MasterVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
        sfxSoundsSource.volume = volume;
    }
    public void MusicVolume(float volume)
    {
        backgroundMusicSource.volume = volume;
    }
    
    public void SfxVolume(float volume)
    {
        sfxSoundsSource.volume = volume;
    }
}
