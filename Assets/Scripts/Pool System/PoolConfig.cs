using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PoolSystem/PoolConfig")]
public class PoolConfig : ScriptableObject
{
    public List<Pool> pools = new List<Pool>();
}
