using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // The angle by which the door should open
    public float openSpeed = 1.0f; // The speed at which the door opens
    private bool isOpen = false; // Flag to track if the door is open or closed
    private Quaternion initialRotation; // The initial rotation of the door
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        // Store the initial rotation of the door
        initialRotation = transform.rotation;
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            // Start the coroutine to gradually open the door
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        isOpen = true;
        float t = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0) * initialRotation;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // End the game after the door opens completely
        gameManager.EndGame();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            // Open the door when collided with the key
            OpenDoor();
        }
    }
}
