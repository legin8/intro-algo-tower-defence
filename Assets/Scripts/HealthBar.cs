/* Program name: Game Mechanics - Tower Defense
Project file name: HealthBar.cs
Author: Nigel Maynard
Date: 22/3/23
Language: C#
Platform: Unity/ VS Code
Purpose: Assessment
Description: Sets and updates the health bar
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100, currentHealth = 100;
    private float originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale;
    }
}
