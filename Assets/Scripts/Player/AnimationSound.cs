﻿using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    private AudioSource src;

    [Header("Footsteps")] 
    [SerializeField] AudioType footsteps = null;

    [Header("Attack")] 
    [SerializeField] AudioType attack = null;

    [Header("Attack")] 
    [SerializeField] AudioType jump = null;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void Footstep()
    {
        footsteps.PlayClip(src);
    }

    public void Attack()
    {
        attack.PlayClip(src);        
    }

    public void Jump()
    {
        Debug.Log("jump");
        jump.PlayClip(src);
    }
}

[System.Serializable]
public class AudioType
{
    public AudioClip[] clips;
    private int counter = 0;

    public void PlayClip(AudioSource src)
    {
        if (clips.Length == 0)
            return;
        src.PlayOneShot(clips[counter]);
        counter = counter >= clips.Length - 1 ? 0 : counter + 1;
    }
}
