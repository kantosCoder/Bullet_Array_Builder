using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_input : MonoBehaviour
{
    Rigidbody2D charbody;
    public float maxVelocity = 4f;
    public float jump_accel = 5;
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private float shoot_frames = 0.015f;//negation frames of bullet spawning (0.025f)/0.015f
    private float shootableframes = 2f;
    private float unshootableframes = 1f;
    private Quaternion spread;
    private bool abletoshoot = false;
    public GameObject bullet_prefab_pointer; //drag prefab from ui... to instantiate
    public bool inputEnabled = true;
    private bool jump = false;
    private int randomizer = 0;
    private Animator anim;
    private string facing = "Right";
    float horizont;



    // Start is called before the first frame update
    void Start()
    {
        shootableframes = shoot_frames;
        unshootableframes = shoot_frames;
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
        //shoot frame controller
        if (!abletoshoot)
        {
            shootableframes -= Time.deltaTime;
            if (shootableframes <= 0.0f)
            {
                abletoshoot = true;
                unshootableframes = shoot_frames;

            }
        }
        if (abletoshoot)
        {
            unshootableframes -= Time.deltaTime;
            if (unshootableframes <= 0.0f)
            {
                abletoshoot = false;
                shootableframes = shoot_frames;
            }

        }

        //jump frame updater
        if (Input.GetKey(KeyCode.Space) && jump == false)
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            anim.SetBool("Jumping", true);
            anim.SetBool("Jump_end", false);
            jump = true;
            charbody.AddForce(new Vector3(0, jump_accel, 0), ForceMode2D.Impulse);
        }
        //shoot frame updater
        if (Input.GetKey(KeyCode.LeftArrow) && jump == false)
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            anim.SetBool("Shooting", true);
            //GameObject bullet = Instantiate(prefabloader, transform.position, Quaternion.identity) as GameObject;
            if (abletoshoot)
            {
                if (facing.Equals("Right"))
                {
                    GameObject thisbullet = Instantiate(bullet_prefab_pointer, new Vector3(transform.position.x + 1.8f, transform.position.y, 0), Quaternion.identity);
                    thisbullet.SendMessage("enabler", facing);
                }
                if (facing.Equals("Left"))
                {
                    GameObject thisbullet = Instantiate(bullet_prefab_pointer, new Vector3(transform.position.x - 1.5f, transform.position.y, 0), Quaternion.identity);
                    thisbullet.SendMessage("enabler", facing);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && jump == false)
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            anim.SetBool("Shooting", false);

        }
    }

    //FIXEDUPDATE FRAMEFREE
    void FixedUpdate()
    {
        horizont = Input.GetAxisRaw("Horizontal");
        //horizontal frame updater
        if (horizont != 0 && jump == false)
        {
            anim.SetBool("Jump_end", false);
            anim.SetBool("Running", true);
            anim.SetBool("Jump_end", true);
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
            anim.SetBool("Jump_end", false);
            anim.SetBool("Jump_end", true);
            anim.SetBool("Jump_hang", false);
            anim.SetBool("Jumping", false);
        }

        //horizontal movement updater
        if (Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(new Vector3(-maxVelocity, 0, 0), ForceMode2D.Impulse);
            transform.position += new Vector3(-maxVelocity, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(-scaleX, 3.531f, 3.531f);
            facing = "Left";
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddForce(new Vector3(maxVelocity, 0, 0), ForceMode2D.Impulse);
            transform.position += new Vector3(maxVelocity, 0, 0) * Time.deltaTime;
            transform.localScale = new Vector3(3.531f, 3.531f, 3.531f);
            facing = "Right";
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