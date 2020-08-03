using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
    }
    
    public List<WeaponTemplate> Weapons = new List<WeaponTemplate>();
    public List<Spawnable> Spawnables = new List<Spawnable>();
}
