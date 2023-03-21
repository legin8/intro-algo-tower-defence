/* Program name: Game Mechanics - Tower Defense
Project file name: MoveEnemy.cs
Author: Nigel Maynard
Date: 22/3/23
Language: C#
Platform: Unity/ VS Code
Purpose: Assessment
Description: This is responsible for making each enemy move
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        GameManagerBehaviour gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPosition = waypoints [currentWaypoint].transform.position;
        Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance (startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPosition)) 
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
                RotateIntoMoveDirection();
            }
            else
            {
                Destroy(gameObject);
                GameManagerBehaviour gameManager = GameObject.Find("Game Manager").GetComponent<GameManagerBehaviour>();
                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                gameManager.Health -= 1;
            }
        }
    }


    private void RotateIntoMoveDirection()
    {
        Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);

        float x = newDirection.x, y = newDirection.y;
        float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;

        gameObject.transform.Find("Sprite").gameObject.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }


    public float DistanceToGoal()
    {
        float distance = Vector2.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }
}