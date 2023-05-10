using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float distance = 1f; // The distance in front of the target transform
    public float offsetHeight = 0f; // The offset height from the target transform

    public Transform Player;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Debug.Log("Ball Position Reset");
            Vector3 desiredPosition = Player.position + (Player.forward * distance);
            //desiredPosition.y = offsetHeight; // Apply the offset height

            // Set the position of the game object to the desired position
            transform.position = desiredPosition;
            
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
