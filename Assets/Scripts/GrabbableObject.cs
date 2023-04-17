using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    bool isGrabbed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Grab(float triggerPress)
    {
        if(triggerPress != 0.0f)
        {
            isGrabbed = true;
        }
        else
        {
            isGrabbed = false;
        }
    }
}
