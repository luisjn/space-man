using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private const int InitialBlocks = 2;

    public static LevelManager Instance { get; private set; }

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateInitialBlocks();
    }
    
    private void Update()
    {
        
    }

    public void AddLevelBlock()
    {
        var randomIdx = Random.Range(0, allTheLevelBlocks.Count);

        LevelBlock block;
        
        Vector3 spawnPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }
        
        block.transform.SetParent(transform, false);

        Vector3 correction = new Vector3(
                spawnPosition.x - block.startPoint.position.x,
                spawnPosition.y - block.startPoint.position.y,
                0
            );
        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }
    
    public void RemoveLevelBlock()
    {
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }
    
    public void RemoveAllLevelBlock()
    {
        while (currentLevelBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }
    
    public void GenerateInitialBlocks()
    {
        for (var i = 0; i < InitialBlocks; i++)
        {
            AddLevelBlock();
        }
    }
}
