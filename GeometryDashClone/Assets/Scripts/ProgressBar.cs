using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float targetProgress = 0;
    public float fillSpeed = 0.5f;
    public float totalLevelDistance;
    public string currentLevel;
    public Level level;
    public float playerProgress;
    public float moveSpeed = 10;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        currentLevel = SpawnManager.Instance.currentLevel.name;
        level = Resources.Load<Level>(currentLevel);
        CalculateLevelDistance();
        slider.maxValue = totalLevelDistance;
    }

    private void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }

        playerProgress += moveSpeed * Time.deltaTime;
        slider.value = playerProgress;
    }

    public void CalculateLevelDistance()
    {
        List<LevelPart> levelParts = level.levelParts;

        foreach (LevelPart part in levelParts)
        {
            totalLevelDistance += part.CalculateLevelPartDistance();
        }
    }
}
