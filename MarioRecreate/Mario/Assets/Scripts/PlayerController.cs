using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public float speed;
    public float jumpForce;

    //ground check
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    
    // dead
    public float DeadTime;
    private float DeadTimer;
    private bool isDead;

    private Animator anim;

    private Vector3 StartingLocation;

    // sound
    public AudioClip Jump;
    public AudioClip Dead;
    public AudioClip GoombaDead;
    public AudioClip LevelEnd;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartingLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (isDead)
        {
            if (DeadTime < DeadTimer)
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                DeadTimer += Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //down = happen once when you press it. GetKeyDown
        //!<--NOT 
        anim.SetBool("jump2", !isOnGround);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (isDead)
        {
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");

        //x and y movement
      //  Vector2 movement = new Vector2(moveHorizontal * speed, 0);

        //rb2d.AddForce(movement);


        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        //groundcheck
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        Debug.Log(isOnGround);

   
        //stuff I added to flip my character
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }



    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
        {
            return;
        }

        if (other.gameObject.CompareTag ("PickUp"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("EndLevel"))
        {
            audioSource.PlayOneShot(LevelEnd);
        }

        GoombaController GoombaController = other.gameObject.GetComponent<GoombaController>();

        if (GoombaController)
        {
           if (isOnGround && !GoombaController.IsDead)
            {
                anim.SetBool("isDeadzo", true);
                isDead = true;
                audioSource.PlayOneShot(Dead);

            }
            else 
            {
                audioSource.PlayOneShot(GoombaDead);
                GoombaController.Kill();
            }
        }

        CoinBoxController CoinBox = other.gameObject.GetComponent<CoinBoxController>();
        if (CoinBox != null)
        {
            CoinBox.use();

        }
    }



    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }

        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                //Forcemode = instant
                rb2d.velocity = Vector2.up * jumpForce;
                // rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

                audioSource.PlayOneShot(Jump);
               //just to see it
                Debug.Log("Jump");








            }

        }
    }
}