using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody rb;
    public Animator animator; // Reference to the Animator component

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movementDirection != Vector3.zero) // If there's any movement input
        {
            // Rotate the character to face the direction of movement
            transform.rotation = Quaternion.LookRotation(movementDirection);

            // Apply movement directly using velocity
            rb.velocity = movementDirection * moveSpeed;

            // Play walking animation
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // Stop the character if there's no movement input
            rb.velocity = Vector3.zero;

            // Stop walking animation
            animator.SetBool("IsWalking", false);
            
            // Maintain the current rotation
            rb.angularVelocity = Vector3.zero;
        }
    }
}