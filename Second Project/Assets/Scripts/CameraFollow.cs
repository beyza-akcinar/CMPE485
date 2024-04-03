using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    public Transform player; // Reference to the player GameObject

    // Adjust the follow speed of the Virtual Camera
    public float followSpeed = 5f;

    // Input threshold for arrow key presses
    public float inputThreshold = 0.1f;

    // Smoothness factor for camera movement
    public float smoothness = 0.1f;

    private Vector3 inputDirection; // Stores the input direction
    private Vector3 currentVelocity = Vector3.zero; // Current velocity for smoothing

    private void Start()
    {
        // Ensure that the virtual camera and player references are set
        if (virtualCamera == null || player == null)
        {
            Debug.LogError("Virtual Camera or Player Transform is not set in the CameraFollow script.");
            enabled = false; // Disable the script if references are not set
        }
    }

    private void Update()
    {
        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the input direction relative to the camera's orientation
        Vector3 cameraForward = virtualCamera.transform.forward;
        Vector3 cameraRight = virtualCamera.transform.right;

        // Calculate the input direction in camera's local space
        Vector3 localInputDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        // Check if the input exceeds the threshold
        if (localInputDirection.magnitude > inputThreshold)
        {
            // Convert the input direction back to world space
            inputDirection = transform.TransformDirection(localInputDirection);

            // Smoothly move the camera towards the target position
            Vector3 targetPosition = player.position + inputDirection * followSpeed * Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
        }
    }
}
