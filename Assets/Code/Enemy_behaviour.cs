using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    public float patrolspeed = 1f;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    // Start is called before the first frame update
    void Start()
    {
        //getscale
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(patrolspeed, 0, 0) * Time.deltaTime;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
