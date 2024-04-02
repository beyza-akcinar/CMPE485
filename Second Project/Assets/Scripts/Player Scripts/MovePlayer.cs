using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f; // Adjust this value to control rotation speed
    public Rigidbody rb;
    public Animator animator; // Reference to the Animator component
    public Transform cameraTransform; // Reference to the camera's transform

    void FixedUpdate()
    {
        // Get input from arrow keys or WASD
        float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        float verticalInput = Input.GetAxis("Vertical") * moveSpeed;

        // Calculate the movement direction relative to the camera's orientation
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0f; // Ensure movement stays on the horizontal plane
        cameraRight.y = 0f; // Ensure movement stays on the horizontal plane

        Vector3 movementDirection = cameraForward * verticalInput + cameraRight * horizontalInput;

        // Normalize the movement direction to ensure constant speed regardless of input magnitude
        if (movementDirection.magnitude > 1f)
        {
            movementDirection.Normalize();
        }

        if (movementDirection != Vector3.zero) // If there's any movement input
        {
            // Apply movement using Rigidbody's velocity
            rb.velocity = movementDirection * moveSpeed;

            // Rotate the character to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Play walking animation
            animator.SetBool("IsWalking", true);

            // Log the movement direction only when there's actual input
            Debug.Log("Movement Direction: " + movementDirection);
        }
        else
        {
            // Stop walking animation
            animator.SetBool("IsWalking", false);
        }

        // Apply additional adjustments for control and stability
        // Adjust Rigidbody constraints
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        // Apply drag to dampen movement
        rb.drag = 5f;
        // Limit maximum angular velocity
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 2f);
    }
}
