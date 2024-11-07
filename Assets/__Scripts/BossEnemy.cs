using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BossEnemy extends the Enemy class
public class BossEnemy : Enemy
{
    [Header("Set in Inspector: BossEnemy")]
    public float moveSpeed = 1f; // Slower movement for the boss
  

    private float screenHalfWidth; // Adjust to move within the screen's half width
    private float birthTime;

    private void Start()
    {
        // Set the initial horizontal limits for movement (occupying half the screen)
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        screenHalfWidth = camWidth / 2;

        birthTime = Time.time;
        
    }

    // Override the Move function to implement a slower, large movement
    public override void Move()
    {
        Vector3 tempPos = pos;

        // Boss moves in a wide, slow horizontal pattern
        float age = Time.time - birthTime;
        float sin = Mathf.Sin(age * moveSpeed);
        tempPos.x = sin * screenHalfWidth; // Horizontal movement
        pos = tempPos;


        base.Move(); // Ensure screen boundary checks and destruction logic
    }
}
