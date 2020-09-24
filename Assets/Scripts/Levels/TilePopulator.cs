using UnityEngine;

public class TilePopulator : MonoBehaviour
{
    public void PopulateTile(GameObject tileObj)
    {
        MeshRenderer ground = tileObj.GetComponent<Tile>().ground;
        foreach (Spawnable s in ResourcesManager.Instance.Spawnables)
        {
            Spawn(s, ground);    
        }
    }

    public void Spawn(Spawnable s, MeshRenderer surface)
    {
        if(Random.Range(0f,1f) > s.spawnChance)
            return;
        var bounds = surface.bounds;
        for (int i = 0; i < Random.Range(s.minSpawnPerTile, s.maxSpawnPerTile + 1); i++)
        {
            float randomX = Random.Range(bounds.min.x + s.widthOffset, bounds.max.x - s.heightOffset);
            float randomZ = Random.Range(surface.bounds.min.z, surface.bounds.max.z);
            Vector3 spawnPos = new Vector3(randomX, s.heightOffset, randomZ);
            PoolManager.instance.GetObject(s.poolName, spawnPos, Quaternion.identity);
        }
    }
}
