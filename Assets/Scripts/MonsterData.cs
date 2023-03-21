/* Program name: Game Mechanics - Tower Defense
Project file name: MonsterData.cs
Author: Nigel Maynard
Date: 22/3/23
Language: C#
Platform: Unity/ VS Code
Purpose: Assessment
Description: Holds and controls the monster data, including leveling it up.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterLevel
{
    public int cost;
    public GameObject visualization, bullet;
    public float fireRate;
}

public class MonsterData : MonoBehaviour
{
    public List<MonsterLevel> levels;
    private MonsterLevel currentLevel;

    public MonsterLevel CurrentLevel
    {

        get => currentLevel;

        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
    
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null) levels[i].visualization.SetActive(i == currentLevelIndex);
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }
    
    public MonsterLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        return currentLevelIndex < maxLevelIndex ? levels[currentLevelIndex+1] : null;
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1) CurrentLevel = levels[currentLevelIndex + 1];
    }
}
