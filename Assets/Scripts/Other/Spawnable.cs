using UnityEngine;

[CreateAssetMenu(menuName = "SnakeMadness/NewSpawnable")]
public class Spawnable : ScriptableObject
{
    public string poolName;

    public int minSpawnPerTile;
    public int maxSpawnPerTile;
    [Range(0,1)]
    public float spawnChance;
    public float widthOffset;
    public float heightOffset;
}
