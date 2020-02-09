using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    private bool ispressed = false;
    public InputInterlacer touch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown()
    {
            touch.fire();
            ispressed = true;

    }
    public void OnPointerUp()
    {
        ispressed = false;
        touch.halt();
    }
}
