using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectScript : MonoBehaviour
{
    public Rigidbody rb;

    public float forceMagnitude = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody reference is not set. Please assign the Rigidbody component.");
            return;
        }

        rb.AddForce(Vector3.forward * forceMagnitude * Time.deltaTime);
    }
}
