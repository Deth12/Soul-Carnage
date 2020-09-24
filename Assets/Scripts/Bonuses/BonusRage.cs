using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRage : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound = null;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float rageDuration = 3f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerStatus>().ApplyRage(rageDuration, speedMultiplier);
            AudioManager.Instance.PlayOneShot(breakSound, .4f);
            this.gameObject.SetActive(false);
        }
    }
}
