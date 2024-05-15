using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float speed = 1f;
    public int size = 1;

    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private float originalSpeed; // Store the original speed
    private bool isSlowed = false;

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        originalSpeed = speed; // Store the original speed
    }

    private void Update()
    {
        // Check if the object is past the right edge of the screen
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            transform.position = new Vector3(leftEdge.x - size, transform.position.y, transform.position.z);
        }
        // Check if the object is past the left edge of the screen
        else if (direction.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            transform.position = new Vector3(rightEdge.x + size, transform.position.y, transform.position.z);
        }
        // Move the object
        else
        {
            // Check if the object is slowed down
            if (isSlowed)
            {
                transform.Translate(speed * 0.5f * Time.deltaTime * direction); // Move at half the speed
            }
            else
            {
                transform.Translate(speed * Time.deltaTime * direction);
            }
        }
    }

    // Method to slow down the object
    public void SlowDown(float slowFactor)
    {
        isSlowed = true;
        speed *= slowFactor;
    }

    // Method to restore original speed
    public void RestoreSpeed()
    {
        isSlowed = false;
        speed = originalSpeed;
    }

    // Method to stop the object
    public void StopMoving()
    {
        speed = 0f;
    }

    // Method to resume movement
    public void ResumeMoving()
    {
        speed = originalSpeed;
    }
}