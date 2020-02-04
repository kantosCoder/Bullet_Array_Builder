using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInterlacer : MonoBehaviour
{
    static bool jumpinput = false;
    static bool jumpispressed = false;
    static bool fireinput = false;
    static bool fireispressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //jump touch imput to interlacer
    public void jumpenable() {
        if (!jumpispressed)
        {
            jumpinput = true;
            jumpispressed = true;
        }
        
    }
    public void jumpdisable() {
        jumpinput = false;
        jumpispressed = false;
    }
    public bool getJumpStatus() {
        return jumpinput;
    }
    //fire touch imput to interlacer
    public void fire()
    {
        if (!fireispressed)
        {
            fireinput = true;
            fireispressed = true;
        }
        
    }
    public void halt()
    {
        fireinput = false;
        fireispressed = false;
    }
    public bool getFirestatus()
    {
        return fireinput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
