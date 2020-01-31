using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_input : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxVelocity = 4f;
    public float jump_accel = 5;
    public bool inputEnabled = true;
    private bool jump = false;
    private Animator anim;
    float horizont;
    float vertical;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (Input.GetKey(KeyCode.Space) && jump == false)
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            anim.SetBool("Jumping", true);
            jump = true;
            rb.AddForce(new Vector3(0, jump_accel, 0), ForceMode2D.Impulse);
        }
    }

    //FIXEDUPDATE FRAMEFREE
    void FixedUpdate()
    {
        horizont = Input.GetAxisRaw("Horizontal");
        //horizontal frame updater
        if (horizont != 0&&jump==false)
        {
            anim.SetBool("Running", true);
        }
        else {
            anim.SetBool("Running", false);
        }
        if (jump == true)
        {
            anim.SetBool("Jump_hang", true);
        }
        else {
            anim.SetBool("Jump_hang", false);
        }
        
        //horizontal movement updater
        if (Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(new Vector3(-maxVelocity, 0, 0), ForceMode2D.Impulse);
            transform.position += new Vector3(-maxVelocity, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(-3.531f, 3.531f, 3.531f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            transform.position += new Vector3(maxVelocity, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(3.531f, 3.531f, 3.531f);

        }
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Terrain"))
        {
            anim.SetBool("Jump_end",true);
                anim.SetBool("Jumping", false);
            jump = false;
        }
    }
}