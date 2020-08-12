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

    [SerializeField] private AudioSource Main = null;
    [SerializeField] private AudioSource SFX = null;

    public void PlayOneShot(AudioClip clip, float volume = 0.5f)
    {
        SFX.PlayOneShot(clip, volume);
    }
    
}
