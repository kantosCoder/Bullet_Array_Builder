using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : MonoBehaviour
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
    void OnPointerDown() {
            touch.jumpenable();
            ispressed = true;
    }
    void OnPointerUp()
    {
        ispressed = false;
        touch.jumpdisable();
    }
}
