using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControllerScript : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        rb.AddForce(movementDirection * moveSpeed);
    }
}
