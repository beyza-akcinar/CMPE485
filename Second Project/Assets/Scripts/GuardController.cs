using UnityEngine;
using System.Collections;

public class GuardController : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints defining the path for the guard
    public float moveSpeed = 1f; // Speed at which the guard moves
    public float rotationSpeed = 5f; // Speed at which the guard rotates
	private GameManager gameManager; // Reference to the GameManager
	public UIManager uiManager;

    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool isMoving = true; // Flag to control guard movement
    private bool movingForward = true; // Flag to indicate the direction of movement

    void Start()
    {
        // Start the guard coroutine
        StartCoroutine(MoveGuard());
		gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    IEnumerator MoveGuard()
    {
        while (isMoving)
        {
            // Calculate direction to the next waypoint
            Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

            // Rotate towards the next waypoint
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Move towards the current waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

            // Check if the guard has reached the current waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                // Move to the next or previous waypoint based on the direction of movement
                if (movingForward)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    currentWaypointIndex--;
                }

                // Check if the guard has reached the last or the first waypoint
                if (currentWaypointIndex >= waypoints.Length)
                {
                    movingForward = false; // Change the direction of movement
                    currentWaypointIndex = waypoints.Length - 1;
                }
                else if (currentWaypointIndex < 0)
                {
                    movingForward = true; // Change the direction of movement
                    currentWaypointIndex = 0;
                }
            }

            yield return null;
        }
    }

    // Check for collisions with the player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Game over scenario for collision with the player
            uiManager.ShowCanvas();
            // Implement your game over logic here, such as showing a game over screen or resetting the level
        }
    }
}
