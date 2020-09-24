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
    
    // Side Tiles
    [SerializeField] private string[] leftSideTilePools;
    [SerializeField] private string[] rightSideTilePools;
    [SerializeField] private float sideTileOffset = 27f;
    
    public float TotalLength { get => maxTileAmount * tileLength; }
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
        SpawnSideTiles(tile.transform.position);
        if (populate)
            populator.PopulateTile(tile);
        nextSpawnZ += tileLength;
    }
    
    public void SpawnSideTiles(Vector3 tilePos)
    {
        PoolManager.instance
            .GetObject
            (leftSideTilePools[Random.Range(0, leftSideTilePools.Length)],
                new Vector3(-sideTileOffset, tilePos.y, tilePos.z),
                Quaternion.identity);
        PoolManager.instance
            .GetObject(rightSideTilePools[Random.Range(0, rightSideTilePools.Length)],
                new Vector3(sideTileOffset, tilePos.y, tilePos.z),
                Quaternion.identity);
    }
}
