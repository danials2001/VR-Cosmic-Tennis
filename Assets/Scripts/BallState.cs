using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    //state = 0 -> player hit ball towards enemy
    //state = 1 -> enemy served
    //state = 2 -> player has ball
    private int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setState(int newState) {
        state = newState;
    }

    public int getState(){
        return state;
    }
}
