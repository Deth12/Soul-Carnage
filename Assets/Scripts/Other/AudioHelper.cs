using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static IEnumerator FadeAudio(AudioSource audioSource, float duration, float initialVolume, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, targetVolume, currentTime / duration);
            Debug.Log(audioSource.volume);
            yield return null;
        }
        
        yield break;
    }

    public static IEnumerator FadeClipTransition(AudioSource audioSource, AudioClip targetClip, float duration)
    {
        float currentTime = 0;
        float initialVolume = audioSource.volume;
        
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(initialVolume, 0, currentTime / duration);
            yield return null;
        }
        audioSource.clip = targetClip;
        audioSource.volume = initialVolume;
        audioSource.Play();
        yield break;
    }
}
