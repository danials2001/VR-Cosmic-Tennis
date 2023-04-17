using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight() 
    {
        if(toggle) 
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        toggle = !toggle;
    }
}
