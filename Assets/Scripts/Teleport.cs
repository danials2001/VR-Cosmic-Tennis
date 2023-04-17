using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Teleport : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public float circleRadius;

    public GameObject Arrow;
    Vector3 arrowPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the trigger button is pressed
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            // Get the position and rotation of the controller
            Vector3 controllerPosition = transform.position;
            Quaternion controllerRotation = transform.rotation;

            // Cast a ray in the direction the controller is pointing
            RaycastHit hit;
            if (Physics.Raycast(controllerPosition, controllerRotation * Vector3.forward, out hit))
            {
                // Set the Line Renderer positions to draw the line from the controller to the hit point
                lineRenderer.SetPosition(0, controllerPosition);
                lineRenderer.SetPosition(1, hit.point);

                DrawCircle(hit.point, circleRadius);

                // Enable Arrow GameObject
                Arrow.SetActive(true);
                arrowPosition = hit.point;
                arrowPosition.y = .5f;
                Arrow.transform.position = arrowPosition;
            }
        }
        if (Arrow.active) {
            Vector2 inputDirection = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            
            if(inputDirection.x != 0f && inputDirection.y != 0f)
                Arrow.transform.right = new Vector3(inputDirection.x, 0f, inputDirection.y);

            if(OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
                transform.parent.parent.position = arrowPosition;
                transform.parent.parent.forward = Arrow.transform.right*-1;
                Arrow.SetActive(false);
            }
        }
    }

    private void DrawCircle(Vector3 center, float radius)
    {  
        int segments = 64;
        float angle = 0f;
        lineRenderer.positionCount = segments + 2 + 1;

        for (int i = 0; i <= segments; i++)
        {
            angle = i * Mathf.PI * 2f / segments;
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;
            Vector3 pos = new Vector3(x, 0.03f, y) + center;
            lineRenderer.SetPosition(i + 2, pos);
        }
    }
}
