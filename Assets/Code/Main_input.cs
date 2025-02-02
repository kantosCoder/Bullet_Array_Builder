﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_input : MonoBehaviour
{
    Rigidbody2D charbody;
    public bool inputEnabled = true;
    public float maxVelocity = 4f;
    public float jump_accel = 5;
    public GameObject currentcamera;
    public InputInterlacer touch;
    public Joystick joytarget;
    private Quaternion spread;
    public GameObject bullet_prefab_pointer; //drag prefab from ui... to instantiate
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    //private float cameraoffset;
    private float shoot_frames = 0.1f;//negation frames of bullet spawning
    private float nextFire = 0.0F;
    //private bool antibounce = false;
    private bool jump = false;
    private bool upwards = false;
    private bool touchjump = false;
    private bool touchshoot = false;
    private bool hastouched = false;
    private int randomizer = 0;
    private Animator anim;
    private string facing = "Right";
    float horizont;

    // Start is called before the first frame update
    void Start()
    {
        //getscale
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
        //instantiation of main_input variables
        charbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Debug.Log("Start" + this.gameObject.name);
    }
    void Awake()
    {
        //statusload
        Debug.Log("Awake" + this.gameObject.name);
    }

    //UPDATE//FRAME-TO-FRAME
    private void Update()
    {
        //jump frame updater
        if (Input.GetKeyDown(KeyCode.Space) && jump == false || (touchjump==true && jump == false))
        {
            if (touchjump == true) {
                touch.jumpdisable();
            }
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            anim.SetBool("Jumping", true);
            anim.SetBool("Jump_end", false);
            jump = true;
            charbody.AddForce(new Vector3(0, jump_accel, 0), ForceMode2D.Impulse);
            currentcamera.transform.position = new Vector3(0,-30,0);
        }

            //shoot frame updater
            if (Input.GetKey(KeyCode.LeftArrow) || (touchshoot == true))
          {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            if (!upwards) { anim.SetBool("Shooting", true); }
            if (touchshoot) {
                hastouched = true;
            }
            //GameObject bullet = Instantiate(prefabloader, transform.position, Quaternion.identity) as GameObject;
            if (Time.time > nextFire)
            {
                nextFire = Time.time + shoot_frames;
                if (facing.Equals("Right")&&!upwards)
                {
                    GameObject thisbullet = Instantiate(bullet_prefab_pointer, new Vector3(transform.position.x + 1.8f, transform.position.y, 0), Quaternion.identity);
                    thisbullet.SendMessage("enabler", facing);
                }
                if (facing.Equals("Left")&&!upwards)
                {
                    GameObject thisbullet = Instantiate(bullet_prefab_pointer, new Vector3(transform.position.x - 1.5f, transform.position.y, 0), Quaternion.identity);
                    thisbullet.SendMessage("enabler", facing);
                }
                if (upwards) {
                    anim.SetBool("Shooting", false);
                    anim.SetBool("Upwards_shooting", true);
                    //bullets fly upwards...
                    if (facing.Equals("Left"))
                    {
                        GameObject thisbullet = Instantiate(bullet_prefab_pointer, new Vector3(transform.position.x+0.2f, transform.position.y + 2.2f, 0), Quaternion.identity);
                        thisbullet.SendMessage("enabler", "Upwards");
                    }
                    else {
                        GameObject thisbullet = Instantiate(bullet_prefab_pointer, new Vector3(transform.position.x-0.2f, transform.position.y + 2.2f, 0), Quaternion.identity);
                        thisbullet.SendMessage("enabler", "Upwards");
                    }
                    
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || (touchshoot == false && hastouched))
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            anim.SetBool("Shooting", false);
            anim.SetBool("Upwards_shooting", false);
            if (hastouched) { hastouched = false; }
        }
    }

    //FIXEDUPDATE FRAMEFREE
    void FixedUpdate()
    {
        touchshoot = touch.getFirestatus();
        touchjump = touch.getJumpStatus();
        horizont = Input.GetAxisRaw("Horizontal");
        //horizontal frame updater
        if ((horizont != 0 && jump == false) || ((joytarget.Horizontal < -0.70f || joytarget.Horizontal >0.70f) && jump ==false))
        {
            anim.SetBool("Running", true);
            anim.SetBool("Jump_hang", false);
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        if (jump == true)
        {
            anim.SetBool("Jump_hang", true);
        }
        else
        {
            anim.SetBool("Jump_end", true);
            anim.SetBool("Jump_hang", false);
            anim.SetBool("Jumping", false);
        }

        //camera frame information getter
        //cameraoffset = currentcamera.transform.position.y;

        //horizontal movement updater
        if (Input.GetKey(KeyCode.A) || (joytarget.Horizontal < -0.70f))
        {
            //rb.AddForce(new Vector3(-maxVelocity, 0, 0), ForceMode2D.Impulse);
            transform.position += new Vector3(-maxVelocity, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
            facing = "Left";
        }
        if (Input.GetKey(KeyCode.D) || joytarget.Horizontal > 0.70f)
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            transform.position += new Vector3(maxVelocity, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            facing = "Right";
        }
        //vertical aim
        if (Input.GetKey(KeyCode.W) || (joytarget.Vertical > 0.32f))
        {
            if (facing.Equals("Left"))
            {
                transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
            }
            else {
                transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            }
            anim.SetBool("Upwards", true);
            upwards = true;
        }
        else {
            anim.SetBool("Upwards_shooting",false);
            anim.SetBool("Upwards", false);
            upwards = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Terrain"))
        {
            anim.SetBool("Jump_end", true);
            anim.SetBool("Jump_hang", false);
            anim.SetBool("Jumping", false);
            jump = false;
        }
    }
}