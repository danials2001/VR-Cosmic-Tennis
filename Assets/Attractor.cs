using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField]
    float maxDistance = 10f;

    public float G = 6.674f;

    public static HashSet<Attractor> Attractors = new HashSet<Attractor>();
    public Rigidbody rb;

    void Attract(Attractor otherAttractor)
    {
        Rigidbody rbToAttract = otherAttractor.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0 || maxDistance > 10f)
        {
            return;
        }

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }

    public void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new HashSet<Attractor>();
        }

        Attractors.Add(this);
    }

    public void onDisable()
    {
        Attractors.Remove(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }
}
