using System;
using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private Player player;
    
    [SerializeField] private float hp = 100;
    [SerializeField] private float MaxSoulEnergy = 100f;
    [SerializeField] private float EnergyPerSecond = 2f;
    [SerializeField] private float soulEnergy;
    
    public bool IsAlive = true;
    public bool IsInvincible = false;

    [SerializeField] private ParticleSystem rageEffect = null;
    [SerializeField] private ParticleSystem deathEffect = null;

    public Action OnDeath;
    public Action OnRageEnable;
    
    public void Init(Player p)
    {
        player = p;
    }
    
    private void Start()
    {
        OnDeath += DeathEvent;
        OnDeath += UIManager.Instance.ShowEndScreen;
        soulEnergy = MaxSoulEnergy;

        GameManager.Instance.OnGameStart +=
            delegate { InvokeRepeating(nameof(UpdateSoulsEnergy), 0f, Time.deltaTime); };
    }
    
    private void UpdateSoulsEnergy()
    {
        soulEnergy -= EnergyPerSecond * Time.deltaTime;
        soulEnergy = Mathf.Clamp(soulEnergy, 0f, 100f);
        UIManager.Instance.UpdateSoulBar(soulEnergy/MaxSoulEnergy);
        if (soulEnergy <= 0)
        {
            OnDeath?.Invoke();
            CancelInvoke(nameof(UpdateSoulsEnergy));
        }
    }

    public void ChangeHealth(float value)
    {
        hp += value;
        if(hp <= 0)
            OnDeath?.Invoke();
    }

    public void HarvestRewards(int souls = 0, float energy = 0)
    {
        GameProfile.Souls += souls;
        soulEnergy += energy;
    }

    public void HarvestKillRewards(int score, int souls, float energy)
    {
        GameProfile.Score += score;
        GameProfile.TotalKills++;
        GameProfile.Souls += souls;
        soulEnergy += energy;
    }

    private float rageLeft = 0f;
    
    public void ApplyRage(float duration, float multiplier)
    {
        print("Apply rage. Duration: " + duration + " Multi: " + multiplier);
        if (rageLeft <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(RageMode(duration, multiplier));
        }
        else
            rageLeft = duration;
    }
    
    private IEnumerator RageMode(float duration, float multiplier)
    {
        UIManager.Instance.ShowRageStatus();
        rageLeft = duration;
        IsInvincible = true;
        player.Movement.ActivateBoost(multiplier);
        player.Collider.EnableRageCollider();
        rageEffect.Play();
        while (rageLeft > 0)
        {
            Debug.Log("Rage Left: " + rageLeft);
            rageLeft -= Time.deltaTime;
            rageLeft = (rageLeft < 0) ? 0 : rageLeft;
            UIManager.Instance.UpdateRageBar(rageLeft/duration, rageLeft);
            yield return null;
        }
        player.Movement.DeactivateBoost();
        rageEffect.Stop();
        UIManager.Instance.HideRageStatus();
        yield return new WaitForSeconds(0.5f);
        player.Collider.DisableRageCollider();
        IsInvincible = false;
    }
    
    private void DeathEvent()
    {
        CancelInvoke(nameof(UpdateSoulsEnergy));
        player.Animator.SetTrigger("Die");
        IsAlive = false;
        deathEffect.Play();
    }
    
    private void OnDisable()
    {
        OnDeath -= DeathEvent;
        OnDeath -= UIManager.Instance.ShowEndScreen;
        GameManager.Instance.OnGameStart -=
            delegate { InvokeRepeating(nameof(UpdateSoulsEnergy), 0f, 1); };
    }
}
