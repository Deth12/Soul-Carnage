using UnityEngine;

[CreateAssetMenu(menuName = "SnakeMadness/NewWeaponTemplate")]
public class WeaponTemplate : ScriptableObject
{
    public int weaponId;
    public WeaponType weaponType;
    public string weaponName;
    public Sprite shopIcon;
    public GameObject weaponPrefab;
    public int price;
    public WeaponPositionTemplate posTemplate;
}

public enum WeaponType
{
    Scythe = 1, Sword = 2, Axe = 3
}
