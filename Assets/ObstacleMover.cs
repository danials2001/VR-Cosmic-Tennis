using UnityEngine;
using System.Collections;

public class ObstacleMover : MonoBehaviour
{
    public Transform minX;
    public Transform maxX;
    public Transform minY;
    public Transform maxY;
    public float time = 4f;

    private float sphereRadius;

    private bool moveRight = true;
    private bool moveUp = true;

    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;
    public Vector3 offset;

    private IEnumerator MoveSphereHorizontally(float time)
    {

        // Move the sphere along the X-axis within the boundary
        Vector3 startingPos  = transform.position;
        Vector3 finalPos;
        if (moveRight)
            finalPos = new Vector3(maxX.position.x - sphereRadius, transform.position.y , transform.position.z);
        else
            finalPos = new Vector3(minX.position.x + sphereRadius, transform.position.y, transform.position.z);
        
        float elapsedTime = 0;
        
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moveRight = !moveRight;
        StartCoroutine(MoveSphereVertically(time));
    }

    private IEnumerator MoveSphereVertically(float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos;

        if (moveUp)
            finalPos = new Vector3(transform.position.x, maxY.position.y - sphereRadius  + offset.y, transform.position.z);
        else
            finalPos = new Vector3(transform.position.x, minY.position.y + sphereRadius + offset.y, transform.position.z);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        moveUp = !moveUp;
        StartCoroutine(MoveSphereHorizontally(time));
    }

    private void Start()
    {

        sphereRadius = transform.localScale.x / 2f;
        offset = new Vector3(offsetX, offsetY, offsetZ);
        StartCoroutine(MoveSphereHorizontally(time));
    }
}