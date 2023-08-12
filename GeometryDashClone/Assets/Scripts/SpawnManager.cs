using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public  Level currentLevel;
    public  int levelPartIndex;
    public PlayerController currentPlayer;
    private GameObject previousLevel;

    private void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        levelPartIndex = -1;
        SpawnLevelPart();
    }

    public void SpawnLevelPart()
    {
        levelPartIndex++;
        if (previousLevel != null)
        {
            Destroy(previousLevel);
        }
        LevelPart levelPart = currentLevel.levelParts[levelPartIndex];   
        transform.position = levelPart.transform.position;
        GameObject level =Instantiate(levelPart.gameObject, transform.position, transform.rotation);
        previousLevel = level;
        
    }

}
