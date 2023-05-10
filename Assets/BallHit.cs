using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{

    public float paddleForce = 2f; // The force to apply to the ball
    public float maxPaddleAngle = 45f; // The maximum angle of deflection
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Check if the collided object is the ball
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();

            if (ballRigidbody != null) // Check if the ball has a Rigidbody component
            {
                Vector3 paddleToBall = other.transform.position - transform.position;
                float normalizedPaddlePosition = paddleToBall.x / (transform.localScale.x / 2f);
                float paddleAngle = maxPaddleAngle * normalizedPaddlePosition;

                // Calculate the velocity to apply to the ball based on the paddle angle
                Vector3 velocity = Quaternion.Euler(0f, paddleAngle, 0f) * transform.forward * paddleForce;

                // Apply the velocity to the ball
                ballRigidbody.velocity = velocity;
                Debug.Log("Hit!");
            }
        }
    }

}

//     void OnCollisionEnter(Collision collision) {
//         var otherRigidbody = collision.GetContact(0).Rigidbody;
//         if (otherRigidbody != null) {
//             var force = otherRigidbody.velocity - rb.velocity;
//             rb.velocity = Vector3.Reflect(rb.velocity, force);
//         }
//     }
// }
