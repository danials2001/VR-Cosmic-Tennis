using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
public Vector2 turn;
    public float sensitivity = 2f;
    public float speed = 10f;
    private float forwardForce;
    private float sideForce;

    Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform.Find("TrackingSpace/CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
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


    }
}
