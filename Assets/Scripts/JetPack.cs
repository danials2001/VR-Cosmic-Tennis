using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float thrustMultiplier = 1;
    
    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private GameObject racket;

    Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform.Find("TrackingSpace/CenterEyeAnchor");
        rb = transform.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -0.95f, 0);
        Physics.IgnoreLayerCollision(3,7);
        Physics.IgnoreLayerCollision(0,3);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 moveVector = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        // turn.x += Input.GetAxis("Mouse X") * sensitivity;
        // turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        // Vector3 direction = new Vector3(playerTransform.rotation.x, 0, playerTransform.rotation.z);
        // Vector3 move = direction * Time.deltaTime * speed;

        Vector3 forwardMovement = playerTransform.forward * moveVector.y;
        Vector3 horizontalMovement = playerTransform.right * moveVector.x;
        //Vector3 movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);
        Vector3 movement = forwardMovement + horizontalMovement;

        movement.y = 0;
        
        transform.Translate(movement * Time.deltaTime * speed);
        //transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        */
        float thrust = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if(thrust > 0.1) { 
            var forceVector = new Vector3(hand.transform.up.x, hand.transform.up.y, 0f)  * thrust * thrustMultiplier;
            rb.AddForce(forceVector);
        }

        float backwardThrust = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);

        if(backwardThrust > 0.1) { 
            var forceVector = new Vector3(hand.transform.up.x, hand.transform.up.y, 0f)  * backwardThrust * thrustMultiplier * -1f;
            rb.AddForce(forceVector);
        }

    }
}
