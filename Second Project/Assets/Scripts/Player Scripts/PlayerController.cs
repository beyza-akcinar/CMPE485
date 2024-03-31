using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pushForce = 5f; // Force to apply when pushing the key

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            // Calculate push direction
            Vector3 pushDirection = collision.transform.position - transform.position;
            

            // Apply force to the key
            collision.rigidbody.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        }
    }
}