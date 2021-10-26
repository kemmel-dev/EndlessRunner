using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject wallSpawnerPrefab;

    private WallSpawner wallSpawner;

    public float spawnDistance;
    public float spawnTimeBetweenWalls;
    private float nextWallCheckpoint;

    private void Start()
    {
        wallSpawner = Instantiate(wallSpawnerPrefab, transform).GetComponent<WallSpawner>();
        wallSpawner.SpawnDistance = spawnDistance;
    }

    private void Update()
    {
        SpawnObstacles();
    }

    private void SpawnObstacles()
    {
        if (Time.time >= nextWallCheckpoint)
        {
            SpawnRandomWall();
        }
    }

    private void SpawnRandomWall()
    {
        nextWallCheckpoint = Time.time + spawnTimeBetweenWalls;
        wallSpawner.SpawnRandomWall();
    }
}
