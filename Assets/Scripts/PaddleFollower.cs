using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFollower : MonoBehaviour
{
    private PaddleCollider _paddleFollower;
	private Rigidbody _rigidbody;
	private Vector3 _velocity;
	private AudioSource whoosh;
	
	//public float paddleForce = 10f; // The force to apply to the ball
    public float maxPaddleAngle = 45f; // The maximum angle of deflection

	[SerializeField]
	private float _sensitivity = 100f;

	[SerializeField]
	private float velocity_threshold = 2f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		whoosh = GetComponent<AudioSource>();
	}

	private void FixedUpdate()
	{
		Vector3 destination = _paddleFollower.transform.position;
		_rigidbody.transform.rotation = transform.rotation;

		_velocity = (destination - _rigidbody.transform.position) * _sensitivity;

		_rigidbody.velocity = _velocity;
		transform.rotation = _paddleFollower.transform.rotation;

		if(_rigidbody.velocity.magnitude > velocity_threshold) {
			//Debug.Log(_rigidbody.velocity.magnitude);
			whoosh.Play();
		}
	}

	public void SetFollowTarget(PaddleCollider paddleFollower)
	{
		_paddleFollower = paddleFollower;
	}

	
    private void OnTriggerEnter(Collider other)
    {
		Debug.Log(other.name);
        if (other.CompareTag("Ball")) // Check if the collided object is the ball
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();

            if (ballRigidbody != null) // Check if the ball has a Rigidbody component
            {
                Vector3 paddleToBall = other.transform.position - transform.position;
                float normalizedPaddlePosition = paddleToBall.x / (transform.localScale.x / 2f);
                float paddleAngle = maxPaddleAngle * normalizedPaddlePosition;

                // Calculate the velocity to apply to the ball based on the paddle angle
                //Vector3 velocity = Quaternion.Euler(0f, paddleAngle, 0f) * transform.forward * paddleForce;
				Vector3 velocity = Quaternion.Euler(0f, paddleAngle, 0f) * transform.forward * _velocity.magnitude;

                // Apply the velocity to the ball
                ballRigidbody.velocity = velocity;
				Debug.Log("hit!!");
            }
        }
    }


}
