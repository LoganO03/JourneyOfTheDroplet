using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] Transform groundCapsule;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    float horizontal;
    float vertical;
    public float maxSpeed;
    public float acceltime;
    public float deceltime;
    public float airAccel;
    public float jumpForgiveness;
    public float climbSpeed = 2;
    private float normalGravity;
    public float scaleOfMaximumSpeediness;
    public float minMaximumSpeed;
    private float targetxVelocity;
    public bool isSwimming;
    private float oldScaleFactor;



    
    private bool m_Grounded;
    public bool enteredCave;
    public AudioSource landing;


    public UnityEvent OnLandEvent;

    // Tweening Variables
    private float oldHorizontal;
    float start;
    float diff;
    float progress;
    float rate;
    float time;

    // Holding that strange vertical velocity bug accountable
    float oldy;

    // collision momentum killing
    float oldx;
    float oldtime;
    float expectedAcceleration;
    private bool isclimbing;

    private bool onJumpPad = false;

    bool resetFlag;

    public void StartClimb(float xcoord)
    {
        isclimbing = true;
        GameManager.Instance.canMove = false;
        normalGravity = rb2D.gravityScale;
        rb2D.gravityScale = 0;
        transform.position = new Vector3(xcoord, transform.position.y, transform.position.z);
        m_Grounded = false;

    }
    public void StartSwim()
    {
        GameManager.Instance.canMove = false;
        isSwimming = true;
    }
    
    public void EndClimb()
    {
        isclimbing = false;
        GameManager.Instance.canMove = true;
        rb2D.gravityScale = normalGravity;
    }

    public void ToggleBigJump(bool b)
    {
        onJumpPad = b;
    }

    // keeps track of current SFX
    AudioSource currentWalkSound;
    public AudioSource grassWalkSound;
    public AudioSource CaveWalkSound;

    //Has the character touched the ground before?
    bool everGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        currentWalkSound = grassWalkSound;
        enteredCave = false;
        oldScaleFactor = scaleFactor();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (OnLandEvent == null) {
		    OnLandEvent = new UnityEvent();
        }
        OnLandEvent.AddListener(Landed);
        horizontal = 0;
        oldHorizontal = 0;
        oldx = transform.position.x;
        oldy = transform.position.y;
        oldtime = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("grounded"))
        {
            currentWalkSound.Stop();
        }
        if (GameManager.Instance.canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal != 0)
            {
                animator.SetBool("isRunning", true);
                if (animator.GetBool("grounded") && !currentWalkSound.isPlaying)
                {
                    currentWalkSound.Play();
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
                currentWalkSound.Stop();
            }
            if (horizontal < 0 && m_Grounded)
            {
                spriteRenderer.flipX = true;
                //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (horizontal > 0 && m_Grounded)
            {
                spriteRenderer.flipX = false;
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            animator.SetFloat("RawHorizontal", horizontal);
            if (Input.GetKeyDown("space") && animator.GetBool("grounded"))
            {
                float forceamount = 500;
                float maxvel = 0.1f;
                if (onJumpPad)
        
                {
                    //change these to change how much the jump pad affects the jump
                    forceamount = 900;
                    maxvel = 0.2f;
                }

                rb2D.AddForce(Vector2.up * forceamount * rb2D.mass);
                if (rb2D.linearVelocity.y > maxvel)
                {
                    rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, maxvel);
                }

                animator.SetBool("grounded", false);
                m_Grounded = false;

                // 🔊 Play jump sound
                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound, 0.25f);
                }
            }

            if (animator.GetBool("grounded") && Mathf.Abs(rb2D.linearVelocity.y) > jumpForgiveness)
            {
                animator.SetBool("grounded", false);
                m_Grounded = false;
            }
        }
        else if (isclimbing)
        {
            vertical = Input.GetAxisRaw("Vertical");
            rb2D.linearVelocity = new Vector2(0f, vertical * climbSpeed);
            if (vertical != 0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else if (isSwimming)
        {
            rb2D.linearVelocity = new Vector2(2, 0f);
        }
    }
    void FixedUpdate()
    {
        
        rb2D.mass = Mathf.Max(GameManager.Instance.playerWater, transform.localScale.x);
        
        
        var dtime = Time.fixedTime - oldtime;
        float oldvx;

        if(dtime != 0) oldvx = (oldx - transform.position.x) / dtime;
        else oldvx = rb2D.linearVelocity.x;

        var discrepancyx = oldvx - rb2D.linearVelocity.x;

        oldx = transform.position.x;
        oldtime = Time.fixedTime;

        if (GameManager.Instance.canMove)
        {
            if (oldHorizontal != Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal) || Mathf.Abs(discrepancyx) < 0.5 || resetFlag)
            {

                resetFlag = false;

                start = rb2D.linearVelocity.x * scaleFactor() / oldScaleFactor;
                diff = maxSpeed * horizontal - start;
                float speedPercent;
                if (horizontal == 0f) speedPercent = Mathf.Abs(rb2D.linearVelocity.x / maxSpeed);
                else speedPercent = Mathf.Abs((maxSpeed * horizontal - rb2D.linearVelocity.x) / maxSpeed * horizontal);
                if (horizontal == 0f) time = speedPercent * deceltime;
                else time = speedPercent * acceltime;
                if (time == 0f) rate = 0;
                else rate = 1 / time;
                progress = 0;
            }
            if (progress < 1f)
            {
                progress = progress + rate * Time.deltaTime * (m_Grounded ? 1 : airAccel);
                float x;
                if (progress >= 1f) x = 1;
                else if (progress * progress == 0f) x = 0;
                else x = 1 + (Mathf.Sqrt(1 - ((1 - progress) * (1 - progress))) - 1);
                if (float.IsNaN(x)) Debug.Log("");
                float newVelocity = start + x * diff;
                expectedAcceleration = (newVelocity - rb2D.linearVelocity.x);
                rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x * (scaleFactor() / oldScaleFactor) + expectedAcceleration * scaleFactor(), rb2D.linearVelocity.y);
                if (time == 0f) rate = 0;
                else rate = 1 / time;
            }
            else if (rb2D.linearVelocity.x != maxSpeed * horizontal * scaleFactor())
            {
                Debug.Log("resetting acceleration");
                resetFlag = true;
            }
        }
        else
        {
            rb2D.linearVelocity = new Vector2(0f, rb2D.linearVelocity.y);
        }


        bool wasGrounded = m_Grounded;

        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(groundCapsule.position, new Vector3(transform.localScale.x * groundCapsule.localPosition.x, transform.localScale.z * groundCapsule.localPosition.y, transform.localScale.z * groundCapsule.localPosition.z), CapsuleDirection2D.Horizontal, groundCapsule.rotation.eulerAngles.z);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    animator.SetBool("grounded", true);
                    OnLandEvent.Invoke();
            }
        }
        
        float oldvy;

        oldvy = (transform.position.y - oldy) / Time.deltaTime;

        animator.SetFloat("Horizontal", rb2D.linearVelocity.x);
        animator.SetFloat("Vertical", oldvy);

        oldy = transform.position.y;

        oldHorizontal = Mathf.Ceil(Mathf.Abs(horizontal)) * Mathf.Sign(horizontal);
        oldScaleFactor = scaleFactor();
        
    }

    public void ExtremeJump()
    {
        rb2D.AddForce(Vector2.up * 1000 * rb2D.mass);
                if (rb2D.linearVelocity.y > 0.2f)
                {
                    rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0.2f);
                }

                animator.SetBool("grounded", false);
                m_Grounded = false;

                // 🔊 Play jump sound
                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound, 0.25f);
                }
    }

    public void Landed() {
        if (enteredCave && everGrounded)
        {
            landing.Play();
            enteredCave = false;
            currentWalkSound = CaveWalkSound;
        }
        animator.SetBool("grounded", true);
        if (!everGrounded)
        {
            everGrounded = true;
        }
    }
    public float scaleFactor() {
        return (1 / Mathf.Exp(Mathf.Abs(transform.localScale.x - scaleOfMaximumSpeediness))) * (1 - minMaximumSpeed) + minMaximumSpeed;
    }
}