using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private Vector3 lastValidPosition; // Store the last valid position of the key object

    void Start()
    {
        lastValidPosition = transform.position; // Initialize the last valid position
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            // End the game
            GameManager.Instance.EndGame();
        }
    }

    void FixedUpdate()
    {
        // Calculate the intended movement direction
        Vector3 moveDirection = transform.position - lastValidPosition;

        // Cast a ray from the last valid position to the current position
        RaycastHit hit;
        if (Physics.Raycast(lastValidPosition, moveDirection.normalized, out hit, moveDirection.magnitude))
        {
            // If the ray hits a collider, move the key object to the point of intersection
            transform.position = hit.point;
        }
        else
        {
            // If the ray doesn't hit anything, update the last valid position
            lastValidPosition = transform.position;
        }
    }
}
