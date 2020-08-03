using UnityEngine;

public class TilePopulator : MonoBehaviour
{
    public float wallDetailsOffset = 0.5f;

    public void PopulateTile(GameObject tileObj)
    {
        MeshRenderer ground = tileObj.GetComponent<Tile>().ground;
        foreach (Spawnable s in ResourcesManager.Instance.Spawnables)
        {
            Spawn(s, ground);    
        }
        
       // MeshRenderer leftWall = tileObj.GetComponent<Tile>().leftWall;
       // SpawnWallDetail(leftWall, "Wall Rocks");
       // MeshRenderer rightWall = tileObj.GetComponent<Tile>().rightWall;
       // SpawnWallDetail(rightWall, "Wall Rocks");
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
            //Vector3 spawnPos = transform.localPosition;
            PoolManager.instance.GetObject(s.poolName, spawnPos, Quaternion.identity);
        }
    }
    
    // public void SpawnScore(MeshRenderer srf)
    // {
    //     int amount = Random.Range(GameManager.instance.minScoreSpawnAmount, GameManager.instance.maxScoreSpawnAmount + 1);
    //     for (int i = 0; i < amount; i++)
    //     {
    //         float randomX = Random.Range(srf.bounds.min.x + groundOffset, srf.bounds.max.x - groundOffset);
    //         float randomZ = Random.Range(srf.bounds.min.z, srf.bounds.max.z);
    //         Vector3 spawnPos = new Vector3(randomX, .4f, randomZ);
    //         PoolManager.instance.GetObject("Score", spawnPos, Quaternion.identity);
    //     }
    // }

    public void SpawnWallDetail(MeshRenderer srf, string name)
    {
        float randomZ = Random.Range(srf.bounds.min.z, srf.bounds.max.z);
        Vector3 spawnPos = new Vector3(srf.bounds.center.x, srf.bounds.max.y + wallDetailsOffset, randomZ);
        PoolManager.instance.GetObject("Wall Rocks", spawnPos, Quaternion.identity);
    }
}
