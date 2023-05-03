using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWhooshRacket : MonoBehaviour
{
    Rigidbody rb;
    AudioSource whoosh;
    
    [SerializeField]
    float velocity_threshold;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        whoosh = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        if(rb.velocity.magnitude > velocity_threshold) {
            whoosh.Play();
            Debug.Log("uwu");
        }

    }
}
