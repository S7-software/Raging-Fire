using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    public static SoundBox instance;
    AudioSource audioSource;
    const string SAVE_VOLUME = "Save_Volume";
    private void Awake()
    {
        if (FindObjectsOfType<SoundBox>().Length > 1 && instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GetVolume();
    }

    public void PlayOneShot(NamesOfSound name)
    {
        audioSource.PlayOneShot(GetAudioClip(name));
    }
    public void PlayIfDontPlay(NamesOfSound name)
    {
        if (!audioSource.isPlaying) PlayOneShot(name);
    }

    public void SetVolume(float volume)
    {
        volume = volume > 1f ? 1f : volume;
        volume = volume < 0 ? 0 : volume;

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(SAVE_VOLUME, volume);
    }
    public float GetVolume() {return PlayerPrefs.GetFloat(SAVE_VOLUME, 1); }
    AudioClip GetAudioClip(NamesOfSound name)
    {
        return Resources.Load<AudioClip>("Sounds/" + name.ToString());
    }
}
