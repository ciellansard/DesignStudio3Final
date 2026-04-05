using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using static Unity.VisualScripting.Member;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton instance

    [Header("Audio Sources")]
    //things that actually play the sounds
    public AudioSource sfxSource;  // For sound effects
    public AudioSource musicSource; // For background music

    [Header("Sound Clips")]
    //you can also use a list of sounds for things u want slight variations in
    public List<AudioClip> cheers = new List<AudioClip>();
    public AudioClip healing;
    public List<AudioClip> hurts = new List<AudioClip>();
    public List<AudioClip> impacts = new List<AudioClip>();
    public AudioClip loss;
    public List<AudioClip> splashes = new List<AudioClip>();
    public List<AudioClip> squishes = new List<AudioClip>();
    public AudioClip steam;
    public List<AudioClip> steps = new List<AudioClip>();
    public List<AudioClip> swings = new List<AudioClip>();
    public AudioClip thunder;
    public AudioClip tumble;
    public AudioClip win;
    public List<AudioClip> wind =  new List<AudioClip>();

    [Header("Music")]
    public AudioClip beautifulJazz;

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
         //   sfxSource.volume = 5;
            sfxSource.PlayOneShot(clip);

        }
    }

    // plays a random sound out of a list
    public void PlaySound(List<AudioClip> clips)
    {
        if (clips != null && clips.Count > 0)
        {
            //sfxSource.volume = 5;
            int randomIndex = Random.Range(0, clips.Count);
            sfxSource.PlayOneShot(clips[randomIndex]);
        }
    }

    public void PlaySound(AudioClip clip, float fade)
    {
        GameObject tempGO = new GameObject("TempAudio");
        AudioSource tempSource = tempGO.AddComponent<AudioSource>();

        //tempSource.volume = 5;
        tempSource.PlayOneShot(clip);
        StartCoroutine(FadeOutCoroutineA(fade, tempSource));
        Destroy(tempGO, fade);
    }

    public void PlaySound(List<AudioClip> clips, float fade)
    {
        GameObject tempGO = new GameObject("TempAudio");
        AudioSource tempSource = tempGO.AddComponent<AudioSource>();

        if (clips != null && clips.Count > 0)
        {
            //sfxSource.volume = 5;
            int randomIndex = Random.Range(0, clips.Count);
            tempSource.PlayOneShot(clips[randomIndex]);

            StartCoroutine(FadeOutCoroutineA(fade, tempSource));
            Destroy(tempGO, fade);
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

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
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
        StartCoroutine(FadeOutCoroutineM(fadeDuration));
    }

    public void FadeInMusic(float fadeDuration)
    {
        StartCoroutine(FadeInCoroutineM(fadeDuration));
    }

    public IEnumerator delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxSource.pitch = 1;
    }

    private IEnumerator FadeOutCoroutineM(float duration)
    {
        //Debug.Log("---------- FADING OUT ----------");

        float startVolume = musicSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        musicSource.volume = 0;
        StopSound();
        musicSource.volume = 1;
    }

    private IEnumerator FadeInCoroutineM(float duration)
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

    public void FadeOutSfx(float fadeDuration)
    {
        StartCoroutine(FadeOutCoroutineS(fadeDuration));
    }

    public void FadeInSfx(float fadeDuration)
    {
        StartCoroutine(FadeInCoroutineS(fadeDuration));
    }

    private IEnumerator FadeOutCoroutineS(float duration)
    {
        //Debug.Log("---------- FADING OUT ----------");
        
        float startVolume = sfxSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            sfxSource.volume = Mathf.Lerp(startVolume, 0, t / duration);

            yield return null;

        }
        
        sfxSource.volume = 0;
        StopSound();
        sfxSource.volume = 1;

    }

    private IEnumerator FadeInCoroutineS(float duration)
    {
        //Debug.Log("---------- FADING IN ----------");

        float startVolume = 0; // THIS WAS THE KEY.. THAAAAAAAAAAAAAAANK GOD

        //Debug.Log("startVolume: " + startVolume);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            sfxSource.volume = Mathf.Lerp(startVolume, 1, t / duration);
            yield return null;
        }

        sfxSource.volume = 1;
        //musicSource.Stop();
    }

    private IEnumerator FadeOutCoroutineA(float duration, AudioSource source)
    {
        //Debug.Log("---------- FADING OUT ----------");

        float startVolume = source.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        source.volume = 0;
        StopSound();
        //Destroy(source.gameObject);
    }



}

