﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_projectile_behaviour : MonoBehaviour
{
    public Rigidbody2D rb;
    public float projectileSpeed_def10 = 0.25f;
    public float flight_time = 2;
    public bool inputEnabled = true;
    private bool object_touch = false;
    private bool latest_position_left = false;
    private bool latest_position_right = false;
    private bool abletoexplode = false;
    private bool explodeset = false;
    private bool hastorotate = false;
    private bool isenabled = false;
    private bool flyuptosky = false;
    private float timetoexplode = 0.15f;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private GameObject thisbullet;
    private Animator anim;
    private Renderer render;
    private Collider2D collider;

    public void enabler(string facing) {
        if (facing.Equals("Upwards")){
            hastorotate = true;
            flyuptosky = true;}
        if (facing.Equals("Right")) { latest_position_right = true; }
        if (facing.Equals("Left")) { latest_position_left = true; }
        if (!hastorotate) { isenabled = true; }
    }


    void Start()
    {
        thisbullet = this.gameObject;
        render = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Debug.Log("Start" + this.gameObject.name);
        //getscaler
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
        render.enabled = false;
    }
    void Awake()
    {
        //statusload
        Debug.Log("Awake" + this.gameObject.name);
    }

    //UPDATE//FRAME-TO-FRAME
    private void Update()
    {
        //create code to count flying frames without hitting to destroy gameobject.
        
        if (isenabled) { 
            //control destruction of gameobject when hit.
            if (abletoexplode)
            {
                timetoexplode -= Time.deltaTime;
                if (timetoexplode <= 0.0f)
                {
                    explodeset = true;

                }
            }
            if (explodeset)
            {
                Destroy(thisbullet);
            }

            //projectile flight time centineel
            flight_time -= Time.deltaTime;

            if (flight_time <= 0.0f)
            {
                Destroy(thisbullet);
            }
        }
        if (hastorotate)
        {
            isenabled = true;
        }
    }

    //FIXEDUPDATE FRAMEFREE
    void FixedUpdate()
    {
        if (isenabled)
        {
            if (!object_touch)
            {
                if (latest_position_left && !flyuptosky)
                {
                    render.enabled = true;
                    //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
                    //transform.position += new Vector3(-projectileSpeed_def10, 0, 0) * Time.deltaTime;
                    transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
                    float Maxspread = 1.20f;
                    float randomNumberX = Random.Range(-Maxspread, Maxspread);
                    float randomNumberY = Random.Range(-Maxspread, Maxspread);
                    rb.transform.Rotate(randomNumberX, randomNumberY, 0);
                    rb.AddForce(new Vector3(-projectileSpeed_def10, Random.Range(-Maxspread, Maxspread), 0), ForceMode2D.Impulse);
                }
                if (latest_position_right && !flyuptosky)
                {
                    render.enabled = true;
                    //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
                    //goodone transform.position += new Vector3(projectileSpeed_def10, 0, 0) * Time.deltaTime;
                    // projectile direction transform.localScale = new Vector3(3.531f, 3.531f, 3.531f);
                    float Maxspread = 1.20f;
                    float randomNumberX = Random.Range(-Maxspread, Maxspread);
                    float randomNumberY = Random.Range(-Maxspread, Maxspread);
                    rb.transform.Rotate(randomNumberX, randomNumberY, 0);
                    rb.AddForce(new Vector3(projectileSpeed_def10, Random.Range(-Maxspread, Maxspread), 0), ForceMode2D.Impulse);
                    //rb.AddForce(rb.transform.forward * 10000);
                }
                if (flyuptosky)
                {
                    float Maxspread = 1.20f;
                    float randomNumberX = Random.Range(-Maxspread, Maxspread);
                    float randomNumberY = Random.Range(-Maxspread, Maxspread);
                    rb.transform.rotation = Quaternion.Euler(Vector3.forward *90);
                    render.enabled = true;
                    rb.AddForce(new Vector3(randomNumberX, projectileSpeed_def10, 0), ForceMode2D.Impulse);
                }
            }
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag.Equals("Terrain") || collision.GetComponent<Collider2D>().tag.Equals("Wall")) //create sprite for terrain.... and yet control them separately
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            object_touch = true;
            anim.SetBool("wall_is_hit", true);
            abletoexplode = true;
        }
        if (collision.GetComponent<Collider2D>().tag.Equals("Enemy")) //create sprite for terrain.... and yet control them separately
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            object_touch = true;
            anim.SetBool("enemy_is_hit", true);
            abletoexplode = true;
        }
        if (collision.GetComponent<Collider2D>().tag.Equals("Player")) //create sprite for terrain.... and yet control them separately
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            object_touch = true;
            anim.SetBool("enemy_is_hit", true);
            abletoexplode = true;
        }
    }
}
