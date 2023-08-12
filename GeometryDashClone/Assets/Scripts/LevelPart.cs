using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart: MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public float levelDistance;

    public float CalculateLevelPartDistance()
    {
        levelDistance = endPoint.transform.position.x - startPoint.transform.position.x;
        return levelDistance;
    }

}
