using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BossEnemy extends the Enemy class
public class BossEnemy : Enemy
{
    [Header("Set in Inspector: BossEnemy")]
    public float moveSpeed = 1f; // Slower movement for the boss
    public float bossfireRate = 0.5f; // Time between projectile bursts
    public GameObject projectilePrefab; // Prefab for shooting at the player
    public float projectileSpeed = 8f; // Speed of the projectiles
  

    private float screenHalfWidth; // Adjust to move within the screen's half width
    private float birthTime;

    private void Start()
    {
        StartCoroutine(FireProjectiles());
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

    // Coroutine for firing projectiles at regular intervals
    private IEnumerator FireProjectiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossfireRate);
            Fire();
        }
    }

    // Custom Fire method to shoot multiple projectiles in a spread pattern
    private void Fire()
    {
        if (projectilePrefab == null) return;

        // Fire multiple projectiles in a spread
        int numProjectiles = 3; // Number of projectiles in each burst
        float angleStep = 25f; // Angle between each projectile
        float startAngle = -(angleStep * (numProjectiles - 1) / 2);

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = startAngle + (i * angleStep);
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Rigidbody projRigidbody = proj.GetComponent<Rigidbody>();
            projRigidbody.velocity = proj.transform.rotation * Vector3.down * projectileSpeed;
            proj.tag = "ProjectileEnemy";
        }
    }
}
