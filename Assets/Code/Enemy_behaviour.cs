using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    public float patrolspeed = 1f;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private bool leftlimit = false;
    private bool rightlimit = true;
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
        if (leftlimit) {
            transform.position += new Vector3(patrolspeed, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        if (rightlimit){
            transform.position += new Vector3(-patrolspeed, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.collider.tag.Equals("Terrain"))
        {
            if (leftlimit) { rightlimit = true; }
            if (rightlimit) { leftlimit = true; }
        }
        if (collision.collider.tag.Equals("Wall"))
        {
            if (leftlimit) { rightlimit = true; }
            if (rightlimit) { leftlimit = true; }
        }
    }
}
