using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton instance

    [Header("Audio Sources")]
    //things that actually play the sounds
    public AudioSource sfxSource;  // For sound effects
    public AudioSource musicSource; // For background music
   
    [Header("Sound Clips")]
    public AudioClip urMom;
    //you can also use a list of sounds for things u want slight variations in

    [Header("Music")]
    public AudioClip urDad;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it across scenes
        }
        else Destroy(gameObject);
    }


    // Play a sound effect
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // plays a random sound out of a list
    public void PlaySound(List<AudioClip> clips)
    {
        if (clips != null && clips.Count > 0)
        {
            int randomIndex = Random.Range(0, clips.Count);
            sfxSource.PlayOneShot(clips[randomIndex]);
        }
    }

    //this one is specifically for things that need to change pitch,
    //public void PlaySound(AudioClip clip, float pitch)
    //{
    //    if (clip != null)
    //    {
    //        //create a new audiosource so multiple blobs (or anything else) can make sounds at different pitches at the same time :3
    //        GameObject tempGO = new GameObject("TempAudio");
    //        AudioSource tempSource = tempGO.AddComponent<AudioSource>();
    //
    //        tempSource.clip = clip;
    //        tempSource.pitch = pitch;
    //        tempSource.volume = sfxSource.volume;
    //        tempSource.Play();
    //
    //        Destroy(tempGO, clip.length / pitch);
    //    }
    //}

    public void StopSound()
    {
        if (sfxSource != null)
        {
            sfxSource.Stop();
        }
    }

    // Set background music
    public void PlayMusic(AudioClip music)
    {
        //Debug.Log("Playing: " + music?.name);
        musicSource.clip = music;
        musicSource.Play();
        //musicSource.volume = 0;
    }

    public void FadeOutMusic(float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    public void FadeInMusic(float fadeDuration)
    {
        StartCoroutine(FadeInCoroutine(fadeDuration));
    }

    private IEnumerator delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxSource.pitch = 1;
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        //Debug.Log("---------- FADING OUT ----------");

        float startVolume = musicSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        musicSource.volume = 0;
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        //Debug.Log("---------- FADING IN ----------");

        float startVolume = 0; // THIS WAS THE KEY.. THAAAAAAAAAAAAAAANK GOD

        //Debug.Log("startVolume: " + startVolume);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 1, t / duration);
            yield return null;
        }

        musicSource.volume = 1;
        //musicSource.Stop();
    }

    
}
