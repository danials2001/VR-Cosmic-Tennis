using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    GameObject ball;

    [SerializeField]
    GameObject enemyPlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float step = speed * Time.deltaTime;
        //Vector3 targetpos = new Vector3(ball.transform.x, ball.transform.y, 0.0f);
        //transform.position = Vector3.MoveTowards(transform.position, targetpos, step);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Ball")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
        }
    }
}
