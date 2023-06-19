/* Program name: Game Mechanics - Tower Defense
Project file name: SpawnEnemy.cs
Author: Nigel Maynard
Date: 22/3/23
Language: C#
Platform: Unity/ VS Code
Purpose: Assessment
Description: This controls when enemies spawn, and know what wave it is currently on.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab, enemyPrefab2;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}


public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject testEnemyPrefab, enemyPrefab2;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehaviour gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;
    private bool spawnSecondEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || (enemiesSpawned != 0 && timeInterval > spawnInterval)) && 
            (enemiesSpawned < waves[currentWave].maxEnemies))
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = (!spawnSecondEnemy) ? (GameObject)Instantiate(waves[currentWave].enemyPrefab)
                : (GameObject)Instantiate(waves[currentWave].enemyPrefab2);
                spawnSecondEnemy = !spawnSecondEnemy;
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
            return;
        }
        
        gameManager.gameOver = true;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
    }
}
