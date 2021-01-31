using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private Transform levelPart_1;

        private void Awake()
    {
        
        SpawnLevelPart(levelPart_Start.Find("EndPosition").position);
        SpawnLevelPart(new Vector3(19, 3) + new Vector3(10, 0));
        SpawnLevelPart(new Vector3(19, 3) + new Vector3(10 + 10, 0));
    }

    private void SpawnLevelPart(Vector3 spawnPosition)
    {
        Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
    }
}
