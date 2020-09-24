using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance;
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance);	
        Instance = this;
        InitialLoad();
    }
    
    public List<WeaponTemplate> Weapons = new List<WeaponTemplate>();
    public List<Spawnable> Spawnables = new List<Spawnable>();

    private void InitialLoad()
    {
        WeaponTemplate[] w =
            Resources.LoadAll("ScriptableObjects/Weapon", typeof(WeaponTemplate)).Cast<WeaponTemplate>().ToArray();
        Debug.Log("Resources > [WeaponTemplate] Loaded: " + w.Length.ToString());
        if(w != null)
            Weapons.AddRange(w);
        Spawnable[] s = 
            Resources.LoadAll("ScriptableObjects/Spawnables", typeof(Spawnable)).Cast<Spawnable>().ToArray();
        Debug.Log("Resources > [Spawnable] Loaded: " + s.Length.ToString());
        if(s != null)
            Spawnables.AddRange(s);
    }
}
