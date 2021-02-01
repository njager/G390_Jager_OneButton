using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //private variables
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 50f;
    private Vector3 lastEndPosition;
    private int levelPartsSpawned;

    //level variables
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartEasyList;
    [SerializeField] private List<Transform> levelPartHardList;
    [SerializeField] private Rigidbody2D player;

    //runs at the start of the application
        private void Awake()
    {
        //finds the end position of the first part of the level and sets it to last end position
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        //begins the level with a set number of parts
        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    //runs every frame
    private void Update()
    {
        //if the player gets close to the end, spawn more parts
       if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            //Spawn another level part
            SpawnLevelPart();
        }
    }
    //spawns parts at the end position of the last part
    private void SpawnLevelPart()
    {
        Transform chosenLevelPart;
        // select easy parts first
        chosenLevelPart = levelPartEasyList[Random.Range(0, levelPartEasyList.Count)];
        // select hard parts after a certain number of parts
        if (levelPartsSpawned > 5)
        {
            chosenLevelPart = levelPartHardList[Random.Range(0, levelPartHardList.Count)];
        }
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelPartsSpawned++;
    }
    //sets spawned part's transform
    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
