using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollider : MonoBehaviour
{
    [SerializeField]
	private PaddleFollower _paddleFollowerPrefab;

	private void SpawnBatCapsuleFollower()
	{
		var follower = Instantiate(_paddleFollowerPrefab);
		follower.transform.position = transform.position;
		follower.SetFollowTarget(this);
	}

	private void Start()
	{
		SpawnBatCapsuleFollower();
	}
}
