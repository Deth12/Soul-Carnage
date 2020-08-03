using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private Animator anim;

    [SerializeField] float hp = 100;
    [SerializeField] float MaxSoulEnergy = 100f;
    [SerializeField] float EnergyPerSecond = 2f;
    [SerializeField] float soulEnergy;
    
    public bool IsAlive = true;

    public Action OnDeath;
    
    public void Init(Player p)
    {
        anim = p.Animator;
    }
    
    void Start()
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

    private void DeathEvent()
    {
        CancelInvoke(nameof(UpdateSoulsEnergy));
        anim.SetTrigger("Die");
        IsAlive = false;
    }

    public void HarvestKillRewards(int score, int souls, float energy)
    {
        GameProfile.Score += score;
        GameProfile.Souls += souls;
        GameProfile.TotalKills++;
        soulEnergy += energy;
    }

    private void OnDisable()
    {
        OnDeath -= DeathEvent;
        OnDeath -= UIManager.Instance.ShowEndScreen;
        GameManager.Instance.OnGameStart -=
            delegate { InvokeRepeating(nameof(UpdateSoulsEnergy), 0f, 1); };
    }
}
