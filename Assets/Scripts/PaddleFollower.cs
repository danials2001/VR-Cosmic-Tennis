using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleFollower : MonoBehaviour
{
    private PaddleCollider _paddleFollower;
	private Rigidbody _rigidbody;
	private Vector3 _velocity;

	[SerializeField]
	private float _sensitivity = 100f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Vector3 destination = _paddleFollower.transform.position;
		_rigidbody.transform.rotation = transform.rotation;

		_velocity = (destination - _rigidbody.transform.position) * _sensitivity;

		_rigidbody.velocity = _velocity;
		transform.rotation = _paddleFollower.transform.rotation;
	}

	public void SetFollowTarget(PaddleCollider paddleFollower)
	{
		_paddleFollower = paddleFollower;
	}

}
