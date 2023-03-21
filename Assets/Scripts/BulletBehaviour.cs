/* Program name: Game Mechanics - Tower Defense
Project file name: BulletBehaviour.cs
Author: Nigel Maynard
Date: 22/3/23
Language: C#
Platform: Unity/ VS Code
Purpose: Assessment
Description: Controls how the bullet behaves
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition, targetPosition;
    private Vector3 normalizeDirection;
    private GameManagerBehaviour gameManager;
    // Start is called before the first frame update
    void Start()
    {
        normalizeDirection = (targetPosition - startPosition).normalized;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime; 
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        target = other.gameObject;
        if(target.tag.Equals("Enemy"))
        {
            HealthBar healthBar = target.transform.Find("HealthBar").gameObject.GetComponent<HealthBar>();
            healthBar.currentHealth -= damage;

            if (healthBar.currentHealth <= 0)
            {
                Destroy(target);
                AudioSource.PlayClipAtPoint(target.GetComponent<AudioSource>().clip, transform.position);
                gameManager.Gold += 50;
            }  
            Destroy(gameObject);
        }
    }
}
