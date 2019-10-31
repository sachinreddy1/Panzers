using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    //
    public AudioMixerGroup mixerGroup;
    public Sound[] sounds;
    //
    static bool fadedIn = false;
    static bool played = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    void Start()
    {
        Play("MainMenu", 23.25f, 42f, 5f, 10f);
    }

    public void Play(string sound, float startTime, float endTime, float fadeInTime, float fadeOutTime)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        //
        fadedIn = false; played = false;
        s.source.time = startTime;
        StartCoroutine(FadeIn(s.source, fadeInTime));
        StartCoroutine(Wait(endTime - startTime - fadeInTime - fadeOutTime));
        StartCoroutine(FadeOut(s.source, fadeOutTime));
    }

    // ----------------------------------- //

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
        fadedIn = true;
    }

    public static IEnumerator Wait(float duration)
    {
        while (!fadedIn)
            yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(duration);
        played = true;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        while (!played)
            yield return new WaitForSeconds(0.1f);

        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
}
