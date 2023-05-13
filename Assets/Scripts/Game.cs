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

    [SerializeField]
    private GameObject obstacles;

    private float startTime = 3.0f;
    private float resetTime = 5.0f;
    private float shootForce = 1.0f;
    private float timer = 0.0f;

    private float randAngleX = 0.0f;
    private float randAngleY = 0.0f;
    private bool first = true;
    private int currState;

    private int playerScore = 0, enemyScore = 0;

    private Vector3 shootDirection;
    private Animator anim;
    private BallState ballState;
    private AudioSource monsterGrowl;
    private Attractor ballAttractor;

    // Start is called before the first frame update
    void Start()
    {
        anim = enemy.GetComponent<Animator>();
        anim.Play("Base Layer.IdleNormal");
        transform.position = enemy.transform.position;
        hitSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        ballState = GetComponent<BallState>();
        currState = ballState.getState();
        monsterGrowl = enemy.GetComponent<AudioSource>();
        ballAttractor = GetComponent<Attractor>();
        ballState.setState(3);
        //Invoke("ServeBall(false)", 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
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
        if(ballState.getState() == 2) //player has ball
        {
            transform.position = player.transform.position + (player.transform.forward * 0.6f);
        }
        // if(ballState.getState() == 0) // ball going toward player
        // {
        //     //check which side of obstacle ball is on
        //     foreach(Transform child in obstacles.transform) 
        //     {
        //         Transform quad = child.GetChild(1);
        //         Vector3 diff = transform.position - quad.position;
        //         // if ball is infront of obstacle
        //         if(Vector3.Dot(diff, quad.forward) > 0)
        //         {
        //             child.gameObject.GetComponent<Attractor>().onDisable();
        //             //Debug.Log("ball passed obstacle going to player");
        //         }    
        //     }
        // }
        // if(ballState.getState() == 1) // ball going toward enemy
        // {
        //     //check which side of obstacle ball is on
        //     foreach(Transform child in obstacles.transform) 
        //     {
        //         Transform quad = child.GetChild(1);
        //         Vector3 diff = transform.position - quad.position;
        //         // if ball is infront of obstacle
        //         if(Vector3.Dot(diff, quad.forward) < 0)
        //         {
        //             child.gameObject.GetComponent<Attractor>().onDisable();
        //             //Debug.Log("ball passed obstacle going to enemy");
        //         }    
        //     }
        // }
        if(ballState.getState() == 3) { //game paused
            transform.position = player.transform.position - (player.transform.forward * 2f);
        }
        if(currState != ballState.getState()) //state changed
        {
            timer = 0f;
            //re enable all attractors
            foreach(Transform child in obstacles.transform) 
            {
            child.gameObject.GetComponent<Attractor>().OnEnable();
            }
            currState = ballState.getState();
        }
        if(timer > 15f && ballState.getState() != 3) {
            ballState.setState(2);
        }
    }

    // side = false => player, side = true => enemy
    public IEnumerator ServeBall(bool side)
    {
        yield return new WaitForSeconds(1);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // shoot from enemy side
        if (side) 
        {
            ballState.setState(1);
            transform.position = enemy.transform.position + new Vector3(0f,2f,0f);

            shootForce = forceMultiplyer;

            randAngleX = Random.Range(-0.2f,0.2f);
            randAngleY = Random.Range(-0.2f,0.2f);

            shootDirection = enemy.transform.forward + new Vector3(randAngleX,randAngleY,0f);
            rb.AddForce(shootDirection * shootForce);
            anim.Play("Base Layer.Attack01");
            monsterGrowl.Play();
            //anim.Play("Base Layer.IdleBattle");
            Debug.Log("enemy serve");
            Debug.Log(shootDirection);
        }
        else
        {
            ballState.setState(2);
            transform.position = player.transform.position + (player.transform.forward * 0.6f);// + new Vector3(0f,0f,2f);
            Debug.Log("player Serve");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "PlayerSide")
        {
            Debug.Log("Added 1 by collider: " + other.gameObject.name);
            // Add point to enemy (false)
            GameManager.Instance.PointScoredByPlayer(false);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
            if(GameManager.Instance.State == GameState.TennisGame){
                StartCoroutine(ServeBall(false));    
            }
       
        }
        else if(other.gameObject.tag == "EnemySide") 
        {
            Debug.Log("Added 1 by collider: " + other.gameObject.name);
            GameManager.Instance.PointScoredByPlayer(true);
            anim.Play("Base Layer.GetHit");
            //anim.Play("Base Layer.IdleBattle");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if(GameManager.Instance.State == GameState.TennisGame){
                StartCoroutine(ServeBall(true));
            }
        }
        else
        {
            if(rb.velocity.magnitude > velocityThreshold) 
            {
                hitSound.Play();
            }
            ballState.setState(0);
        }
    }
}
