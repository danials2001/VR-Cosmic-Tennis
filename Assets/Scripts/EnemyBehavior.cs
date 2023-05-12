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

    private Vector3 planeNormal;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        planeNormal = enemyPlane.transform.forward * -1f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        Vector3 targetpos = Vector3.ProjectOnPlane(ball.transform.position, planeNormal);
        targetpos.z = enemyPlane.transform.position.z;
        targetpos.y -= 0.8f;
        if(targetpos.y > 3.8f) {
            targetpos.y = 3.8f;
        } //position of top quad
        if(targetpos.y < -0.9f) {
            targetpos.y = -0.9f;
        }//position of bottom quad
        if(targetpos.x > 3f) {
            targetpos.x = 3f;
        }//position of right quad
        if(targetpos.x < -3.2f) {
            targetpos.x = -3.2f;
        }//position of left quad
        transform.position = Vector3.MoveTowards(transform.position, targetpos, step);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Ball")
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(transform.forward * 150f);
            anim.Play("Base Layer.Attack02ST");
            other.gameObject.GetComponent<BallState>().setState(1);
            GetComponent<AudioSource>().Play();
            //anim.Play("Base Layer.IdleBattle");
        }
    }
}
