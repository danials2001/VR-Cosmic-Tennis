using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float waitTime = 5.0f;

    [SerializeField]
    float forceMultiplyer = 3.0f;

    private float shootForce = 1.0f;

    private float timer = 0.0f;

    private float randAngleX = 0.0f;
    private float randAngleY = 0.0f;
    private bool hasBall = true;
    private Vector3 shootDirection;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("IdleNormal");
        projectile.transform.position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer > waitTime && hasBall)
        {
            projectile.transform.position = transform.position;
            randAngleX = Random.Range(-0.5f,0.5f);
            randAngleY = Random.Range(-0f,4f);

            shootForce = Random.Range(0.2f,0.4f) * forceMultiplyer;

            shootDirection = transform.forward;
            shootDirection.x += randAngleX;
            shootDirection.y += randAngleY;

            projectile.transform.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce);
            transform.LookAt(shootDirection);
            anim.Play("Attack01");

            //hasBall = false;
            timer = 0.0f;
        }
    }
}
