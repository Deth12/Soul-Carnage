using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);	
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private AudioSource Main = null;
    [SerializeField] private AudioSource SFX = null;

    public AudioClip mainMenuTheme;
    public AudioClip gameTheme;

    private void Start()
    {
        SwitchMainClip(mainMenuTheme);
        Main.Play();
    }

    public void PlayOneShot(AudioClip clip, float volume = 0.5f)
    {
        SFX.PlayOneShot(clip, volume);
    }

    public void SwitchMainClip(AudioClip clip, float fadeTime = 0.8f)
    {
        if (Main.clip != null)
            StartCoroutine(AudioHelper.FadeClipTransition(Main, clip, fadeTime));
        else
            Main.clip = clip;
    }

    public void PauseMainClip()
    {
        StartCoroutine(AudioHelper.FadeAudio(Main, 0.4f, Main.volume, 0f));
    }

    public void ResumeMainClip()
    {
        StartCoroutine(AudioHelper.FadeAudio(Main, 0.4f, Main.volume, .4f));
    }
    
}
