using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float forceMultiplyer = 20.0f;

    [SerializeField]
    TextMeshProUGUI text;

    private AudioSource hitSound;
    private Rigidbody rb;

    [SerializeField]
    private float velocityThreshold = 2f;

    private float startTime = 3.0f;
    private float resetTime = 5.0f;
    private float shootForce = 1.0f;
    private float timer = 0.0f;

    private float randAngleX = 0.0f;
    private float randAngleY = 0.0f;
    private bool first = true;

    private int playerScore = 0, enemyScore = 0;

    private Vector3 shootDirection;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = enemy.GetComponent<Animator>();
        anim.Play("Base Layer.IdleNormal");
        transform.position = enemy.transform.position;
        hitSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        Invoke("ServeBall(false)", 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // timer += Time.deltaTime;
        // if(timer > startTime && first)
        // {
        //     ServeBall(true);
        //     first = false;
        //     timer = 0f;
        // }
        // if (OVRInput.Get(OVRInput.Button.Two))
        // {
        //     Vector3 desiredPosition = player.transform.position + (player.transform.forward * 0.5f);
        //     transform.position = desiredPosition;
        //     rb.velocity = Vector3.zero;
        // }
    }

    // side = false => player, side = true => enemy
    void ServeBall(bool side)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // shoot from enemy side
        if (side) 
        {
            transform.position = enemy.transform.position + new Vector3(0f,2f,0f);

            shootForce = forceMultiplyer;

            randAngleX = Random.Range(-0.2f,0.2f);
            randAngleY = Random.Range(-0.2f,0.2f);

            shootDirection = enemy.transform.forward + new Vector3(randAngleX,randAngleY,0f);
            rb.AddForce(shootDirection * shootForce);
            anim.Play("Base Layer.Attack01");
            //anim.Play("Base Layer.IdleBattle");
            Debug.Log("enemy serve");
            Debug.Log(shootDirection);
        }
        else
        {
            transform.position = player.transform.position + (player.transform.forward * 0.5f);// + new Vector3(0f,0f,2f);
            Debug.Log("player Serve");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "PlayerSide")
        {
            Debug.Log("Added 1 by collider: " + other.gameObject.name);
            GameManager.Instance.enemyScore += 1;
            ServeBall(false);
        }
        else if(other.gameObject.tag == "EnemySide") 
        {
            GameManager.Instance.playerScore += 1;
            anim.Play("Base Layer.GetHit");
            //anim.Play("Base Layer.IdleBattle");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Invoke("ServeBall(true)", 1);
        }
        else
        {
            if(rb.velocity.magnitude > velocityThreshold) 
            {
                hitSound.Play();
            }
        }
    }
}
