using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControllerScript : MonoBehaviour {

    public float speed;     // The fastest the player can travel in the x axis.
    public float jumpForce = 800f;      // Amount of force added when the player jumps.
    [SerializeField] private LayerMask m_WhatIsGround;    // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool isGrounded;            // Whether or not the player is grounded.
    private Animator m_Anim;            
    private Rigidbody2D m_Rigidbody2D;
    public OnScreenCheckScript screenCheck;
    public CircleCollider2D circleCollider;
    public CapsuleCollider2D capsulCollider;
    public GUIScript guiHandler;

    private int coins;
    private float startX;
    private bool canJump;
    private bool hitJump;
    private bool canSlide;
    private bool slide;
    private int move;
    public int lives;
    public bool isInvulnerable;
    private int maxLives;
    private float nextSpeedDist;
    public float nextSpeedDistModifier;
    private bool downwardForce;     //constant downward force on player to counteract Unity quirk at high speeds

    private void Start()
    {
        maxLives = 3;
        if (lives > maxLives)
            lives = maxLives;
        startX = transform.position.x;
        coins = 0;
        move = 0;
        nextSpeedDist = 30;
        isInvulnerable = false;
        canJump = false;
        canSlide = false;
        nextSpeedDistModifier = 40;
        downwardForce = false;
        guiHandler.setPlayerStart(startX);
    }

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //Check for user input
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown("escape"))
        {
            guiHandler.pause();
        }

        if (canJump && !hitJump)
        {
            hitJump = Input.GetButtonDown("Jump");
        }
        if (canSlide && isGrounded)
        {
            slide = Input.GetButton("Slide");
            if (slide)
            {
                capsulCollider.enabled = false;
                circleCollider.enabled = true;
            }
            else
            {
                capsulCollider.enabled = true;
                circleCollider.enabled = false;
            }
            m_Anim.SetBool("Slide", slide);
        }
    }


    private void FixedUpdate()
    {
        Move(move, hitJump);
        hitJump = false;
        if(transform.position.x - startX >= nextSpeedDist)
        {
            nextSpeedDist += nextSpeedDistModifier;
            speed += 0.15f;
            if (speed > 15)
                speed = 15;
            screenCheck.addWait(0.03f);
        }
        guiHandler.setCountText(coins, transform.position.x);
        isGrounded = false;

        checkIfGrounded();

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        if (downwardForce)
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coin":
                coins++;
                Destroy(collision.gameObject);
                break;
            case "Hazard":
                if (collision.gameObject.GetComponent<HazardScript>().damage() && !isInvulnerable)
                {
                    isInvulnerable = true;
                    lives--;
                    guiHandler.changeHeartCount(lives);
                    if (lives > 0)
                    {
                        m_Anim.SetTrigger("Damage");
                    }
                    else
                    {
                        die();
                    }
                    StartCoroutine(becomeVulnerable(1f));
                }
                break;
        }
    }

    public void Move(float move, bool jump)
    {
        //Move the player ever forward
        if (isGrounded)
        {
            m_Anim.SetFloat("Speed", Mathf.Abs(move));
            m_Rigidbody2D.velocity = new Vector2(move * speed, m_Rigidbody2D.velocity.y);
        }
        // If the player should jump...
        if (isGrounded && jump)
        {
            isGrounded = false;
            m_Anim.SetBool("Ground", false);
            // Zero out and add a vertical force to the player.
            m_Rigidbody2D.velocity = new Vector2(move * speed, 0);
            m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x, jumpForce));
            downwardForce = false;
            stopDownwardForce(2);
        }
    }

    private void checkIfGrounded()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        m_Anim.SetBool("Ground", isGrounded);
    }

    private void die()
    {
        if (lives == 0)
        {
            m_Anim.SetBool("isDead", true);
            canJump = false;
            canSlide = false;
            stopPlayer();
            guiHandler.playerDeath();
        }
    }

    public void stopPlayer()
    {
        move = 0;
    }

    public void startPlayer()
    {
        canJump = true;
        canSlide = true;
        move = 1;
    }

    //temporarily remove downward force when jumping
    IEnumerator stopDownwardForce(float x)
    {
        yield return new WaitForSeconds(x);
        downwardForce = true;
    }

    //Period of invincibility time after getting hit
    IEnumerator becomeVulnerable(float x)
    {
        yield return new WaitForSeconds(x);
        isInvulnerable = false;
    }
}
