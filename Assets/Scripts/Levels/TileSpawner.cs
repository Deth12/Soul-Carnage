using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    private TilePopulator populator;
    private Transform player;
    private float nextSpawnZ;

    private List<GameObject> tiles = new List<GameObject>();

    [SerializeField] private int maxTileAmount = 8;
    [SerializeField] private float tileLength = 30f;
    
    public bool IsSpawningAllowed = false;

    private void Start()
    {
        populator = gameObject.AddComponent<TilePopulator>();
        player = GameManager.Instance.Player;
        FIllTilesList();
        for(int i = 0; i < maxTileAmount; i++)
            SpawnTile(i > 1 ? true : false);
        IsSpawningAllowed = true;
        // GameManager.instance.OnCutsceneStart += InitialTilePopulation;
    }

    private void FIllTilesList()
    {
        foreach (Transform tile in PoolManager.instance.GetPool("Tiles").parent)
            tiles.Add(tile.gameObject);
    }

    private void InitialTilePopulation()
    {
        foreach (GameObject tile in tiles)
        {
            if(tile.activeSelf)
                populator.PopulateTile(tile);
        }
    }
    
    private void Update()
    {
        if(IsSpawningAllowed && player.position.z > (nextSpawnZ - maxTileAmount * tileLength))
        {
            SpawnTile(true);
        }
    }
    private void SpawnTile(bool populate)
    {
        Vector3 spawnPos = new Vector3(0f, 0f, nextSpawnZ);
        GameObject tile = PoolManager.instance.GetObject("Tiles", spawnPos, Quaternion.identity);
        if (populate)
            populator.PopulateTile(tile);
        nextSpawnZ += tileLength;
    }
}
