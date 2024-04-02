using System.Collections;
using System.Collections.Generic;using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Transform mainCameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        // Input alma
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Kameraya göre yönü hesaplayın
        Vector3 cameraForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = cameraForward * verticalInput + mainCameraTransform.right * horizontalInput;

        // Hareket etme
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
