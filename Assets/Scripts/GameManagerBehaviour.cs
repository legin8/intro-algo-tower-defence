/* Program name: Game Mechanics - Tower Defense
Project file name: GameManagerBehaviour.cs
Author: Nigel Maynard
Date: 22/3/23
Language: C#
Platform: Unity/ VS Code
Purpose: Assessment
Description: Controls the Game Manager
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour
{
    public Text goldLabel, waveLabel, healthLabel;
    public bool gameOver = false;
    public GameObject[] nextWaveLabels, healthIndicator;

    private int gold;
    public int Gold {

        get => gold;
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
        }
    }

    private int wave;
    public int Wave {
        get => wave;
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);
        }
    }

    private int health;
    public int Health
    {
        get => health;
        set
        {
            if (value < health) {
                Camera.main.GetComponent<CameraShake>().Shake();
            }
    
            health = value;
            healthLabel.text = "HEALTH: " + health;

            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                GameObject.FindGameObjectWithTag("GameOver").GetComponent<Animator>().SetBool("gameOver", true);
            }

            for (int i = 0; i < healthIndicator.Length; i++)
            {
                healthIndicator[i].SetActive(i < Health);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = 1000;
        Wave = 0;
        Health = 5;
    }
}
