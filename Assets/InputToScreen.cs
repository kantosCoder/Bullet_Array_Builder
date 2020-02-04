using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToScreen : MonoBehaviour
{
    public Joystick joytotest;
    public float horizontaljoy;
    public float verticaljoy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        horizontaljoy = joytotest.Horizontal;
        verticaljoy = joytotest.Vertical;
    }
}
