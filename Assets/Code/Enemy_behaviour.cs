using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    public float patrolspeed = 1f;
    public float visionRadius = 5;
    public float attackRadius = 2;
    public float range = 1;
    private float leftspeed;
    private float rightspeed;
    public Transform groundetector;
    public Transform playerdetector;
    private Vector2 direction = new Vector2(1,-0.65f);
    private string facing = "Right";
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private bool onpatrol = true;
    private Rigidbody2D rb;
    Vector3 playerlocation;
    private float playerdistance;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //getproperties
        anim = GetComponent<Animator>();
        //dirspeedmultipliers
        leftspeed = -patrolspeed;
        rightspeed = patrolspeed;
        //getbody
        rb = GetComponent<Rigidbody2D>();
        //getscale
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        //player positio tracker
        playerlocation = playerdetector.transform.position;
        //patrol movemetn
        if (onpatrol)
        {
            transform.position += new Vector3(patrolspeed, 0, 0) * Time.deltaTime;
            anim.SetBool("Running", true);
        }
        else {
            anim.SetBool("Running", false);
        }
        //Raycast ground detection
        Debug.DrawRay(groundetector.position, direction * range);
        RaycastHit2D groundetect = Physics2D.Raycast(groundetector.position, direction, range);
        if (groundetect == true)
        {
            if (groundetect.collider.CompareTag("Wall"))
            {
                Debug.Log("WALL DETECTED!");
                if (facing.Equals("Right"))
                {
                    goLeft();
                }
                else if (facing.Equals("Left"))
                {
                    goRight();
                }
            }

        }
        else {
            Debug.Log("NOTHING DETECTED!");
            if (facing.Equals("Right"))
            {
                goLeft();
            }
            else if (facing.Equals("Left"))
            {
                goRight();
            }
        }
        //Raycast playerdetection
        RaycastHit2D playerdetect = Physics2D.Raycast(transform.position,playerdetector.transform.position - transform.position, visionRadius);
        if (playerdetect == true)
        {
            if (playerdetect.collider.CompareTag("Player"))
            {
            }
        }
        //playerdetection tracer
        Vector3 forwarder = transform.TransformDirection(playerdetector.transform.position - transform.position);
        Debug.DrawRay(transform.position, forwarder, Color.red);
        playerdistance = Vector3.Distance(playerlocation, transform.position);
        //playerdetection fire
        if (playerdistance < visionRadius)
        {   
            if(playerlocation.x<transform.position.x){
                if (facing.Equals("Left") && onpatrol)
                {
                    goLeft();
                    onpatrol = false;
                    anim.SetBool("Shooting", true);
                }
                else if (facing.Equals("Right")&&playerdistance<attackRadius) {
                    goLeft();
                    onpatrol = false;
                    anim.SetBool("Shooting", true);

                }
            }
            else if (playerlocation.x > transform.position.x) {
                goRight();
                if (facing.Equals("Right") && onpatrol)
                {
                    goRight();
                    onpatrol = false;
                    anim.SetBool("Shooting", true);
                }
                else if (facing.Equals("Left") && playerdistance < attackRadius)
                {
                    goRight();
                    onpatrol = false;
                    anim.SetBool("Shooting", true);

                }
            }
        }
        else {
            onpatrol = true;
            anim.SetBool("Shooting", false);
        }
    }
    private void goLeft() {
        patrolspeed = leftspeed;
        transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        direction = new Vector2(-1, -0.65f);
        facing = "Left";
        Debug.Log("GOING LEFT!");
    }
    private void goRight()
    {
        patrolspeed = rightspeed;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        direction = new Vector2(1, -0.65f);
        facing = "Right";
        Debug.Log("GOING RIGHT!");
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {

        //no collision needed at this point...
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
