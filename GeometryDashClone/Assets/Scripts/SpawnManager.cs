using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Level currentLevel;
    public int levelPartIndex;

    private void Start()
    {
        levelPartIndex = -1;
        SpawnLevelPart();
    }

    public void SpawnLevelPart()
    {
        levelPartIndex++;
        LevelPart levelPart = currentLevel.levelParts[levelPartIndex];
        Instantiate(levelPart.gameObject, transform.position,transform.rotation);
    }

}
