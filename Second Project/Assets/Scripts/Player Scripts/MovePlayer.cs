using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f; // Adjust this value to control rotation speed
    public Rigidbody rb;
    public Animator animator; // Reference to the Animator component

    void FixedUpdate()
    {
        // Get input from arrow keys or WASD for movement
        float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        float verticalInput = Input.GetAxis("Vertical") * moveSpeed;

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize the movement direction to ensure constant speed regardless of input magnitude
        if (movementDirection.magnitude > 1f)
        {
            movementDirection.Normalize();
        }

        // Rotate the character based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);

        if (movementDirection != Vector3.zero) // If there's any movement input
        {
            // Apply movement using Rigidbody's velocity
            rb.velocity = transform.forward * moveSpeed;

            // Play walking animation
            animator.SetBool("IsWalking", true);
            
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
        rb.drag = 2f;
        // Limit maximum angular velocity
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 2f);
    }
}