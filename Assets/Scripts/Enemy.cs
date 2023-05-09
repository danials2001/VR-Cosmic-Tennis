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
    float forceMultiplier = 500.0f;


    [SerializeField]
    float maxShootY = .8f;
    [SerializeField]
    float minShootY = -.1f;

    [SerializeField]
    float maxShootX = .4f;
    [SerializeField]
    float minShootX = -.4f;

    private float randomShootForce = 1.0f;

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
    // void FixedUpdate()
    // {
    //     timer += Time.deltaTime;
    //     if(timer > waitTime && hasBall)
    //     {
    //         projectile.transform.position = transform.position;
    //         randAngleX = Random.Range(minShootX,maxShootX);
    //         randAngleY = Random.Range(minShootY,maxShootY);

    //         randomShootForce = Random.Range(0.2f,0.4f) * forceMultiplier;

    //         shootDirection = transform.forward;
    //         shootDirection.x += randAngleX;
    //         shootDirection.y += randAngleY;

    //         projectile.transform.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * randomShootForce);
    //         transform.LookAt(shootDirection);
    //         anim.Play("Attack01");

    //         //hasBall = false;
    //         timer = 0.0f;
    //     }
    // }

    void FixedUpdate()
    {

    }

    public void shootBallAtPlayer()
    {
        projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile.transform.position = transform.position;
        transform.LookAt(player.transform);
        projectile.transform.GetComponent<Rigidbody>().AddForce(transform.forward * forceMultiplier);
        anim.Play("Attack01");
    }

    public void shootBallRandom() // TODO: Fix Random 
    {
        projectile.transform.position = transform.position;
        randAngleX = Random.Range(minShootX,maxShootX);
        randAngleY = Random.Range(minShootY,maxShootY);

        randomShootForce = Random.Range(0.2f,0.4f) * forceMultiplier;

        shootDirection = transform.forward;
        shootDirection.x += randAngleX;
        shootDirection.y += randAngleY;

        projectile.transform.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * randomShootForce);
        transform.LookAt(shootDirection);
        anim.Play("Attack01");
    }
}
