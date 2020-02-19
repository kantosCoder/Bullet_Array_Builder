using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_camera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, -2);
    private Vector3 originaloffset;
    // Start is called before the first frame update
    void Start()
    {
        originaloffset = offset;
    }
    //control the right offset
    public void rightoffset() {
        offset = originaloffset;
    }
    //control the left offset
    public void leftoffset() {
        offset = new Vector3(0, 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
