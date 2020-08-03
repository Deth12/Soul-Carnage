using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
    }

    public AudioSource Main;
    public AudioSource SFX;

    public void PlayOneShot(AudioClip clip, float volume = 0.5f)
    {
        SFX.PlayOneShot(clip, volume);
    }
    
}
