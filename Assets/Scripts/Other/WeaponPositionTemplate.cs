using UnityEngine;

[CreateAssetMenu(menuName = "SnakeMadness/NewWeaponPositionTemplate")]
public class WeaponPositionTemplate : ScriptableObject
{
    [Header("Positioning")]
    public Vector3 HandPos;
    public Vector3 HandEuler;
    [HideInInspector]
    public Quaternion HandRot
    {
        get { return Quaternion.Euler(HandEuler.x, HandEuler.y, HandEuler.z);}
    }

    public Vector3 handScale;
}
