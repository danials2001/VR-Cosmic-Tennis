using UnityEngine;
using System.Collections;

public class CircularMover : MonoBehaviour
{
    public Transform minX;
    public Transform maxX;
    public Transform minY;
    public Transform maxY;
    public float time = 4f;
    public float movementRadius = 1f;

    private float sphereRadius;

    private bool moveRight = true;
    private bool moveUp = true;

    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;
    public Vector3 offset;

    private IEnumerator MoveSphereCircularly(float time, Vector3 center, float movementRadius)
    {

        // Move the sphere along the X-axis within the boundary
        Vector3 startingPos = new Vector3(center.x + movementRadius, center.y + movementRadius, center.z);
        Vector3 finalPos = new Vector3(center.x - movementRadius, center.y - movementRadius, center.z);
        
        float elapsedTime = 0;
        
        while (elapsedTime < time)
        {
            transform.position = Vector3.Slerp(startingPos - center, finalPos - center, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    private void Start()
    {

        sphereRadius = transform.localScale.x / 2f;
        offset = new Vector3(offsetX, offsetY, offsetZ);
        Vector3 circleCenter = new Vector3(maxX.position.x - minX.position.x, maxY.position.y - minY.position.y, transform.position.z);
        StartCoroutine(MoveSphereCircularly(time, circleCenter, movementRadius));
    }
}